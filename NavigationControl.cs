using System.Media;

namespace Inventory_Management
{
    public partial class NavigationControl : UserControl
    {
        public event EventHandler? OverviewClicked;
        public event EventHandler? ViewInventoryClicked;
        public event EventHandler? AddStockClicked;
        public event EventHandler? ManageItemsClicked;
        public event EventHandler? ProjectionsClicked;
        public event EventHandler? CheckoutClicked;

        public NavigationControl()
        {
            InitializeComponent();
        }

        private void overviewButton_Click(object sender, EventArgs e) => OverviewClicked?.Invoke(this, EventArgs.Empty);
        private void viewInventoryButton_Click(object sender, EventArgs e) => ViewInventoryClicked?.Invoke(this, EventArgs.Empty);
        private void addStockButton_Click(object sender, EventArgs e) => AddStockClicked?.Invoke(this, EventArgs.Empty);
        private void manageItemsButton_Click(object sender, EventArgs e) => ManageItemsClicked?.Invoke(this, EventArgs.Empty);
        private void projectionsButton_Click(object sender, EventArgs e) => ProjectionsClicked?.Invoke(this, EventArgs.Empty);
        private void checkoutButton_Click(object sender, EventArgs e) => CheckoutClicked?.Invoke(this, EventArgs.Empty);
    }
}


