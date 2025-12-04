#!/usr/bin/env python3
"""
Script to import entire CSV file into SQLite inventory database.
Reads WMT_Grocery_202209.csv and populates the inventory.db with all products.
"""

import csv
import sqlite3
import os
import sys
from pathlib import Path

def import_csv_to_db(csv_path, db_path):
    """Import CSV data into SQLite database."""
    
    # Verify CSV exists
    if not os.path.exists(csv_path):
        print(f"Error: CSV file '{csv_path}' not found.")
        return False
    
    # Verify database exists
    if not os.path.exists(db_path):
        print(f"Error: Database file '{db_path}' not found. Please run the application first to initialize the database.")
        return False
    
    try:
        conn = sqlite3.connect(db_path)
        cursor = conn.cursor()
        
        # Get current item count
        cursor.execute("SELECT COUNT(*) FROM Items")
        initial_count = cursor.fetchone()[0]
        print(f"Database has {initial_count} existing items")
        
        print(f"\nReading CSV from: {csv_path}")
        print(f"Database: {db_path}\n")
        
        inserted = 0
        skipped = 0
        
        with open(csv_path, 'r', encoding='utf-8') as csvfile:
            reader = csv.DictReader(csvfile)
            
            if not reader.fieldnames or 'PRODUCT_NAME' not in reader.fieldnames or 'PRICE_CURRENT' not in reader.fieldnames:
                print("Error: CSV must have PRODUCT_NAME and PRICE_CURRENT columns")
                return False
            
            for row_num, row in enumerate(reader, start=2):  # Start at 2 to account for header
                try:
                    product_name = row.get('PRODUCT_NAME', '').strip()
                    price_str = row.get('PRICE_CURRENT', '').strip()
                    
                    # Validate data
                    if not product_name:
                        skipped += 1
                        continue
                    
                    # Try to parse price
                    try:
                        price = float(price_str) if price_str else 0.0
                    except ValueError:
                        price = 0.0
                    
                    # Build description from BREADCRUMBS + BRAND + PRODUCT_SIZE
                    description_parts = []
                    breadcrumbs = row.get('BREADCRUMBS', '').strip()
                    brand = row.get('BRAND', '').strip()
                    product_size = row.get('PRODUCT_SIZE', '').strip()
                    
                    if breadcrumbs:
                        description_parts.append(breadcrumbs)
                    if brand:
                        description_parts.append(brand)
                    if product_size:
                        description_parts.append(product_size)
                    
                    description = " | ".join(description_parts)
                    
                    # Truncate name to 256 chars (database constraint)
                    if len(product_name) > 256:
                        product_name = product_name[:256]
                    
                    # Truncate description to 1024 chars (database constraint)
                    if len(description) > 1024:
                        description = description[:1024]
                    
                    # Generate random stock quantity (between 10 and 100)
                    import random
                    stock_qty = random.randint(10, 100)
                    
                    # Insert into database
                    cursor.execute(
                        "INSERT INTO Items (Name, Description, CurrentPrice, StockQuantity, Barcode) VALUES (?, ?, ?, ?, ?)",
                        (product_name, description, price, stock_qty, "")
                    )
                    
                    inserted += 1
                    
                    # Progress indicator every 10000 rows
                    if inserted % 10000 == 0:
                        print(f"  Inserted {inserted} items...")
                
                except Exception as e:
                    skipped += 1
                    if row_num <= 10:  # Only print first few errors
                        print(f"  Warning: Skipped row {row_num}: {e}")
        
        conn.commit()
        
        # Get final count
        cursor.execute("SELECT COUNT(*) FROM Items")
        final_count = cursor.fetchone()[0]
        
        print(f"\n[COMPLETE] Import finished!")
        print(f"  Inserted: {inserted} items")
        print(f"  Skipped: {skipped} rows")
        print(f"  Total items in database: {final_count}")
        
        conn.close()
        return True
    
    except Exception as e:
        print(f"Error importing CSV: {e}")
        return False

if __name__ == "__main__":
    # Get paths
    script_dir = Path(__file__).parent
    csv_file = script_dir / "WMT_Grocery_202209.csv"
    db_file = script_dir / "bin" / "Debug" / "net8.0-windows" / "data" / "inventory.db"
    
    print("CSV to Database Importer")
    print("=" * 50)
    
    if not os.path.exists(db_file):
        print(f"Database not found at: {db_file}")
        print("Trying alternate location...")
        # Try alternate path
        db_file = script_dir / "data" / "inventory.db"
    
    success = import_csv_to_db(str(csv_file), str(db_file))
    sys.exit(0 if success else 1)

