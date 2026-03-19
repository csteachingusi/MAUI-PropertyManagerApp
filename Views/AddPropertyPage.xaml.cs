using PropertyManagerApp.Models;
using PropertyManagerApp.Services;

namespace PropertyManagerApp.Views
{
    public partial class AddPropertyPage : ContentPage
    {
        private readonly PropertyService _service;

        public AddPropertyPage(PropertyService service)
        {
            InitializeComponent();
            _service = service;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("Validation", "Property Name is required.", "OK");
                return;
            }

            if (!decimal.TryParse(RentEntry.Text, out decimal rent))
            {
                await DisplayAlert("Validation", "Please enter a valid rent amount.", "OK");
                return;
            }

            var newProperty = new Property
            {
                PropertyName = NameEntry.Text,
                Address = AddressEntry.Text ?? "",
                UnitNumber = UnitEntry.Text ?? "",
                MonthlyRent = rent
            };

            bool success = await _service.CreatePropertyAsync(newProperty);
            if (success)
            {
                await DisplayAlert("Success", "Property added!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Error", "Failed to create property.", "OK");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}