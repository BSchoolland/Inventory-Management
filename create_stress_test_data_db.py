#!/usr/bin/env python3

"""
Stress test data generation script (database version)

This script skips creating large .txt files and writes items directly into the
SQLite database used by the Inventory Management application.

It mirrors the logic of `create_stress_test_data.ps1` but targets the `Items`
table in `inventory.db` and can generate very large datasets (e.g. 100M rows).

Usage (from the repository root):

    # Generate 100 million items into the debug database
    py create_stress_test_data_db.py --count 100000000

    # Or specify a particular database file
    py create_stress_test_data_db.py --db-path bin\\Debug\\net8.0-windows\\data\\inventory.db --count 100000000

Notes about SQLite / Python:
- The standard library `sqlite3` module is used; no extra pip install is required.
- If you prefer to vendor a SQLite driver via pip into this repo, you can do:
      py -m pip install --target .\\.python_libs pysqlite3-binary
  and then add that folder to `sys.path` before importing. This script will work
  fine without that, using the built-in `sqlite3`.
"""

import argparse
import os
import random
import sqlite3
import sys
import time
from typing import Iterable, List, Tuple


# Sample item templates for variety (mirrors create_stress_test_data.ps1)
ITEM_TEMPLATES: List[dict] = [
    {"name": "Great Value Whole Milk", "base_price": 3.28, "base_qty": 120},
    {"name": "Wonder Classic White Bread", "base_price": 2.48, "base_qty": 80},
    {"name": "Lay's Classic Potato Chips", "base_price": 4.29, "base_qty": 60},
    {"name": "Great Value Large Eggs", "base_price": 2.29, "base_qty": 150},
    {"name": "Coca-Cola 12 Pack", "base_price": 6.98, "base_qty": 90},
    {"name": "Bounty Paper Towels", "base_price": 12.97, "base_qty": 45},
    {"name": "Charmin Ultra Soft Toilet Paper", "base_price": 14.97, "base_qty": 50},
    {"name": "Hanes Mens T-Shirt 5-Pack", "base_price": 17.84, "base_qty": 35},
    {"name": "George Mens Jeans", "base_price": 19.97, "base_qty": 40},
    {"name": "Mainstays Bath Towel", "base_price": 4.88, "base_qty": 70},
    {"name": "Ozark Trail Stainless Steel Tumbler", "base_price": 9.94, "base_qty": 55},
    {"name": "Samsung 32-inch Smart TV", "base_price": 158.00, "base_qty": 12},
    {"name": "Duracell AA Batteries", "base_price": 14.24, "base_qty": 65},
    {"name": "Crayola Crayons 24 Count", "base_price": 1.47, "base_qty": 200},
    {"name": "LEGO Classic Medium Brick Box", "base_price": 34.76, "base_qty": 25},
]


DEFAULT_DB_CANDIDATES = [
    os.path.join("bin", "Debug", "net8.0-windows", "data", "inventory.db"),
    os.path.join("bin", "Release", "net8.0-windows", "data", "inventory.db"),
    os.path.join("data", "inventory.db"),
]


def resolve_db_path(explicit_path: str | None) -> str:
    """
    Resolve the SQLite database path.

    If an explicit path is provided, it is used as-is. Otherwise, a few common
    locations are checked (matching how the WinForms app builds to bin/Debug/...).
    """
    if explicit_path:
        return explicit_path

    for candidate in DEFAULT_DB_CANDIDATES:
        if os.path.exists(candidate):
            return candidate

    msg_lines = [
        "Could not locate inventory.db.",
        "Make sure you've run the Inventory Management app at least once so EF Core",
        "can create the database, or pass --db-path explicitly.",
        "",
        "Tried the following locations (relative to current working directory):",
    ]
    msg_lines.extend(f"  - {p}" for p in DEFAULT_DB_CANDIDATES)
    sys.exit("\n".join(msg_lines))


def configure_connection(conn: sqlite3.Connection) -> None:
    """
    Configure SQLite pragmas for faster bulk inserts.

    These settings trade durability for speed, which is acceptable for generating
    disposable stress-test data.
    """
    cursor = conn.cursor()
    cursor.execute("PRAGMA journal_mode = OFF;")
    cursor.execute("PRAGMA synchronous = OFF;")
    cursor.execute("PRAGMA temp_store = MEMORY;")
    cursor.execute("PRAGMA cache_size = -100000;")  # ~100MB cache
    cursor.close()


def clear_items_table(conn: sqlite3.Connection) -> None:
    """
    Remove all existing items from the Items table.

    This keeps the stress-test run deterministic and avoids unbounded growth if
    you re-run the script.
    """
    print("Clearing existing items from Items table...")
    cursor = conn.cursor()
    cursor.execute("DELETE FROM Items;")
    conn.commit()
    cursor.close()
    print("Existing items cleared.")


def generate_item_rows(total_items: int) -> Iterable[Tuple[str, str, float, int, str]]:
    """
    Lazy generator that yields rows to insert into the Items table.

    Schema (per InventoryItem model):
        Name (TEXT, required, max 256)
        Description (TEXT, max 1024)
        CurrentPrice (NUMERIC with precision(18,2))
        StockQuantity (INTEGER)
        Barcode (TEXT, max 128, unique when non-empty)

    For stress testing we leave Barcode empty to avoid the unique index overhead.
    """
    template_count = len(ITEM_TEMPLATES)
    for i in range(1, total_items + 1):
        template = ITEM_TEMPLATES[(i - 1) % template_count]
        name = f"{template['name']} #{i}"

        # Match the PowerShell logic approximately:
        # price = basePrice * (0.8 + rand(0..199) / 100)
        multiplier = 0.8 + random.randint(0, 199) / 100.0
        price = round(template["base_price"] * multiplier, 2)

        qty = template["base_qty"] + random.randint(-20, 50)
        if qty < 0:
            qty = 0

        description = ""
        barcode = ""  # keep empty to avoid unique index collisions

        yield (name, description, price, qty, barcode)


def bulk_insert_items(
    conn: sqlite3.Connection,
    total_items: int,
    batch_size: int = 10_000,
    commit_every: int = 1_000_000,
) -> None:
    """
    Insert many items into the Items table efficiently using batched executemany.
    """
    cursor = conn.cursor()
    sql = (
        "INSERT INTO Items (Name, Description, CurrentPrice, StockQuantity, Barcode) "
        "VALUES (?, ?, ?, ?, ?);"
    )

    start_time = time.time()
    last_report_time = start_time
    inserted = 0
    batch: List[Tuple[str, str, float, int, str]] = []

    print(
        f"Beginning bulk insert: total_items={total_items}, "
        f"batch_size={batch_size}, commit_every={commit_every}"
    )

    for row_num, row in enumerate(generate_item_rows(total_items), start=1):
        batch.append(row)

        if len(batch) >= batch_size:
            cursor.executemany(sql, batch)
            batch.clear()

        if row_num % commit_every == 0:
            conn.commit()
            inserted = row_num
            elapsed = time.time() - start_time
            rate = inserted / elapsed if elapsed > 0 else 0.0
            now = time.time()
            if now - last_report_time >= 5:
                print(
                    f"  Inserted {inserted:,} items "
                    f"in {elapsed:0.1f}s ({rate:0.0f} rows/s)"
                )
                last_report_time = now

    # Flush any remaining rows
    if batch:
        cursor.executemany(sql, batch)

    conn.commit()
    total_elapsed = time.time() - start_time
    rate = total_items / total_elapsed if total_elapsed > 0 else 0.0
    print(
        f"[DONE] Inserted {total_items:,} items "
        f"in {total_elapsed:0.1f}s ({rate:0.0f} rows/s)"
    )

    cursor.close()


def main(argv: List[str] | None = None) -> int:
    parser = argparse.ArgumentParser(
        description=(
            "Generate large volumes of inventory items directly into the "
            "SQLite database for stress testing."
        )
    )
    parser.add_argument(
        "--db-path",
        type=str,
        default=None,
        help=(
            "Path to inventory.db. If omitted, common build output locations are "
            "searched (bin/Debug/net8.0-windows/data/inventory.db, etc.)."
        ),
    )
    parser.add_argument(
        "--count",
        type=int,
        default=100_000_000,
        help="Number of items to generate (default: 100000000).",
    )
    parser.add_argument(
        "--batch-size",
        type=int,
        default=10_000,
        help="Number of rows per executemany batch (default: 10000).",
    )
    parser.add_argument(
        "--commit-every",
        type=int,
        default=1_000_000,
        help="Commit every N inserted rows (default: 1000000).",
    )
    parser.add_argument(
        "--skip-clear",
        action="store_true",
        help="Do not clear the Items table before inserting.",
    )

    args = parser.parse_args(argv)

    db_path = resolve_db_path(args.db_path)
    print(f"Using database: {db_path}")

    # Ensure containing directory exists (it should if the app has run once)
    db_dir = os.path.dirname(db_path)
    if db_dir and not os.path.isdir(db_dir):
        os.makedirs(db_dir, exist_ok=True)

    conn = sqlite3.connect(db_path)
    try:
        configure_connection(conn)

        if not args.skip_clear:
            clear_items_table(conn)

        bulk_insert_items(
            conn,
            total_items=args.count,
            batch_size=args.batch_size,
            commit_every=args.commit_every,
        )
    finally:
        conn.close()

    return 0


if __name__ == "__main__":
    raise SystemExit(main())



