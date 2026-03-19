using PropertyManagerApp.Views;

namespace PropertyManagerApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for pages that are navigated to programmatically
            Routing.RegisterRoute(nameof(PropertyDetailPage), typeof(PropertyDetailPage));
            Routing.RegisterRoute(nameof(AddPropertyPage), typeof(AddPropertyPage));
            Routing.RegisterRoute(nameof(EditPropertyPage), typeof(EditPropertyPage));
        }
    }
}