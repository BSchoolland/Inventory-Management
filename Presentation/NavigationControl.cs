using System.Media;
using System.Drawing;

namespace Inventory_Management.Forms
{
    public partial class NavigationControl : UserControl
    {
        public enum NavigationPage
        {
            Overview,
            ManageInventory,
            ManageItems
        }

        public event EventHandler? OverviewClicked;
        public event EventHandler? ManageInventoryClicked;
        public event EventHandler? ManageItemsClicked;

        private Color _overviewDefaultColor;
        private Color _manageInventoryDefaultColor;
        private Color _manageItemsDefaultColor;

        private NavigationPage _currentPage;

        public NavigationControl()
        {
            InitializeComponent();

            // Capture the initial designer-set colors so we can restore them when not selected
            _overviewDefaultColor = overviewButton.BackColor;
            _manageInventoryDefaultColor = manageInventoryButton.BackColor;
            _manageItemsDefaultColor = manageItemsButton.BackColor;

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
            manageInventoryButton.BackColor = _manageInventoryDefaultColor;
            manageItemsButton.BackColor = _manageItemsDefaultColor;
            
            Color activeColor = Color.FromArgb(0, 122, 204);

            // Highlight the selected one
            switch (_currentPage)
            {
                case NavigationPage.Overview:
                    overviewButton.BackColor = activeColor;
                    break;
                case NavigationPage.ManageInventory:
                    manageInventoryButton.BackColor = activeColor;
                    break;
                case NavigationPage.ManageItems:
                    manageItemsButton.BackColor = activeColor;
                    break;
            }
        }

        private void overviewButton_Click(object sender, EventArgs e) => OverviewClicked?.Invoke(this, EventArgs.Empty);
        private void manageInventoryButton_Click(object sender, EventArgs e) => ManageInventoryClicked?.Invoke(this, EventArgs.Empty);
        private void manageItemsButton_Click(object sender, EventArgs e) => ManageItemsClicked?.Invoke(this, EventArgs.Empty);
    }
}
