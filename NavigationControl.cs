using System.Media;
using System.Drawing;

namespace Inventory_Management
{
    public partial class NavigationControl : UserControl
    {
        public enum NavigationPage
        {
            Overview,
            ViewInventory,
            AddStock,
            ManageItems,
            Projections,
            Checkout
        }

        public event EventHandler? OverviewClicked;
        public event EventHandler? ViewInventoryClicked;
        public event EventHandler? AddStockClicked;
        public event EventHandler? ManageItemsClicked;
        public event EventHandler? ProjectionsClicked;
        public event EventHandler? CheckoutClicked;

        private Color _overviewDefaultColor;
        private Color _viewInventoryDefaultColor;
        private Color _addStockDefaultColor;
        private Color _manageItemsDefaultColor;
        private Color _projectionsDefaultColor;
        private Color _checkoutDefaultColor;

        private NavigationPage _currentPage;

        public NavigationControl()
        {
            InitializeComponent();

            // Capture the initial designer-set colors so we can restore them when not selected
            _overviewDefaultColor = overviewButton.BackColor;
            _viewInventoryDefaultColor = viewInventoryButton.BackColor;
            _addStockDefaultColor = addStockButton.BackColor;
            _manageItemsDefaultColor = manageItemsButton.BackColor;
            _projectionsDefaultColor = projectionsButton.BackColor;
            _checkoutDefaultColor = checkoutButton.BackColor;

            // Ensure Overview's unselected color isn't blue by default
            _overviewDefaultColor = SystemColors.Control;

            // Do not highlight any button unless the parent form sets the current page
            UpdateButtonStyles();
        }

        public NavigationControl(NavigationPage initialPage) : this()
        {
            SetCurrentPage(initialPage);
        }

        public void SetCurrentPage(NavigationPage page)
        {
            _currentPage = page;
            UpdateButtonStyles();
        }

        private void UpdateButtonStyles()
        {
            // Reset all to defaults
            overviewButton.BackColor = _overviewDefaultColor;
            viewInventoryButton.BackColor = _viewInventoryDefaultColor;
            addStockButton.BackColor = _addStockDefaultColor;
            manageItemsButton.BackColor = _manageItemsDefaultColor;
            projectionsButton.BackColor = _projectionsDefaultColor;
            checkoutButton.BackColor = _checkoutDefaultColor;

            // Highlight the selected one
            switch (_currentPage)
            {
                case NavigationPage.Overview:
                    overviewButton.BackColor = SystemColors.ActiveCaption;
                    break;
                case NavigationPage.ViewInventory:
                    viewInventoryButton.BackColor = SystemColors.ActiveCaption;
                    break;
                case NavigationPage.AddStock:
                    addStockButton.BackColor = SystemColors.ActiveCaption;
                    break;
                case NavigationPage.ManageItems:
                    manageItemsButton.BackColor = SystemColors.ActiveCaption;
                    break;
                case NavigationPage.Projections:
                    projectionsButton.BackColor = SystemColors.ActiveCaption;
                    break;
                case NavigationPage.Checkout:
                    checkoutButton.BackColor = SystemColors.ActiveCaption;
                    break;
            }
        }

        private void overviewButton_Click(object sender, EventArgs e) => OverviewClicked?.Invoke(this, EventArgs.Empty);
        private void viewInventoryButton_Click(object sender, EventArgs e) => ViewInventoryClicked?.Invoke(this, EventArgs.Empty);
        private void addStockButton_Click(object sender, EventArgs e) => AddStockClicked?.Invoke(this, EventArgs.Empty);
        private void manageItemsButton_Click(object sender, EventArgs e) => ManageItemsClicked?.Invoke(this, EventArgs.Empty);
        private void projectionsButton_Click(object sender, EventArgs e) => ProjectionsClicked?.Invoke(this, EventArgs.Empty);
        private void checkoutButton_Click(object sender, EventArgs e) => CheckoutClicked?.Invoke(this, EventArgs.Empty);
    }
}


