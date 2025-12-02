using Inventory_Management.Forms;
using Inventory_Management.Services;

namespace Inventory_Management
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Initialize SQLite database
            try
            {
                InventoryStorageSqlite.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize database: {ex.Message}", "Database Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new OverviewForm());
        }
    }
}