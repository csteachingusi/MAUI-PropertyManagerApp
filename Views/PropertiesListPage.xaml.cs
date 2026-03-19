using PropertyManagerApp.Models;
using PropertyManagerApp.Services;

namespace PropertyManagerApp.Views
{
    public partial class PropertiesListPage : ContentPage
    {
        private readonly PropertyService _service;

        public PropertiesListPage(PropertyService service)
        {
            InitializeComponent();
            _service = service;
        }

        // Refresh list every time we navigate to this page
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var properties = await _service.GetAllPropertiesAsync();
            PropertiesCollection.ItemsSource = properties;
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddPropertyPage));
        }

        private async void OnDetailsClicked(object sender, EventArgs e)
        {
            if ((Button)sender is { CommandParameter: Property property })
            {
                await Shell.Current.GoToAsync(
                    $"{nameof(PropertyDetailPage)}?propertyId={property.PropertyID}");
            }
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            if ((Button)sender is { CommandParameter: Property property })
            {
                await Shell.Current.GoToAsync(
                    $"{nameof(EditPropertyPage)}?propertyId={property.PropertyID}");
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if ((Button)sender is { CommandParameter: Property property })
            {
                bool confirm = await DisplayAlert(
                    "Delete",
                    $"Delete '{property.PropertyName}'?",
                    "Yes", "Cancel");

                if (confirm)
                {
                    bool success = await _service.DeletePropertyAsync(property.PropertyID);
                    if (success)
                    {
                        await DisplayAlert("Success", "Property deleted.", "OK");
                        var updated = await _service.GetAllPropertiesAsync();
                        PropertiesCollection.ItemsSource = updated;
                    }
                    else
                    {
                        await DisplayAlert("Error", "Could not delete property.", "OK");
                    }
                }
            }
        }
    }
}