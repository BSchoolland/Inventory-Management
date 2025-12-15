namespace Inventory_Management.UI
{
    public static class InventoryUI
    {
        private static int lastQuantity = 1;
        public static void RenderCards<T>(
            Panel container,
            IList<T> items,
            int startIdx,
            int endIdx,
            Func<T, string> getName,
            Func<T, string> getDescription,
            Func<T, decimal> getPrice,
            Func<T, int> getQuantity,
            Func<T, string> getBarcode,
            Action<T, int>? onAddQuantity)
        {
            container.Controls.Clear();
            int y = 10;
            for (int i = startIdx; i < endIdx; i++)
            {
                var item = items[i];
                var cardPanel = new Panel
                {
                    Location = new Point(10, y),
                    Size = new Size(570, 80),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.WhiteSmoke,
                    Padding = new Padding(10)
                };
                var nameLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    Text = getName(item),
                    Location = new Point(10, 10)
                };
                var descLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Italic),
                    Text = getDescription(item),
                    Location = new Point(10, 35)
                };
                var priceLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    Text = $"Price: ${getPrice(item)}",
                    Location = new Point(400, 10)
                };
                var stockLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    Text = $"Stock: {getQuantity(item)}",
                    Location = new Point(400, 35)
                };
                var barcodeLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 8, FontStyle.Regular),
                    Text = $"Barcode: {getBarcode(item)}",
                    Location = new Point(10, 60)
                };

                cardPanel.Controls.Add(nameLabel);
                cardPanel.Controls.Add(descLabel);
                cardPanel.Controls.Add(priceLabel);
                cardPanel.Controls.Add(stockLabel);
                cardPanel.Controls.Add(barcodeLabel);

                if (onAddQuantity != null)
                {
                    var addLabel = new Label { AutoSize = true, Text = "+ Qty:", Location = new Point(260, 10) };

                    var addUpDown = new NumericUpDown { Minimum = 1, Maximum = 1000000, Value = lastQuantity, Location = new Point(310, 8), Size = new Size(70, 27) };
                    
                    var addBtn = new Button { Text = "Add", Location = new Point(230, 38), Size = new Size(70, 29) };

                    addBtn.Click += (s, e) => { lastQuantity = (int)addUpDown.Value; onAddQuantity(item, (int)addUpDown.Value); };

                    var removeBtn = new Button { Text = "Remove", Location = new Point(310, 38), Size = new Size(85, 29) };

                    removeBtn.Click += (s, e) => { lastQuantity = (int)addUpDown.Value; onAddQuantity(item, -(int)addUpDown.Value); };

                    cardPanel.Controls.Add(addLabel);
                    cardPanel.Controls.Add(addUpDown);
                    cardPanel.Controls.Add(addBtn);
                    cardPanel.Controls.Add(removeBtn);
                }

                container.Controls.Add(cardPanel);
                y += cardPanel.Height + 10;
            }
        }
    }
}

