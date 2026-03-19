using PropertyManagerApp.Models;
using PropertyManagerApp.Services;

namespace PropertyManagerApp.Views
{
    [QueryProperty(nameof(PropertyId), "propertyId")]
    public partial class EditPropertyPage : ContentPage
    {
        private readonly PropertyService _service;
        private Property? _currentProperty;

        public int PropertyId { get; set; }

        public EditPropertyPage(PropertyService service)
        {
            InitializeComponent();
            _service = service;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _currentProperty = await _service.GetPropertyAsync(PropertyId);
            if (_currentProperty != null)
            {
                NameEntry.Text = _currentProperty.PropertyName;
                AddressEntry.Text = _currentProperty.Address;
                UnitEntry.Text = _currentProperty.UnitNumber;
                RentEntry.Text = _currentProperty.MonthlyRent.ToString();
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (_currentProperty == null) return;

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

            // Preserve the original ID so the PUT request targets the right record
            _currentProperty.PropertyName = NameEntry.Text;
            _currentProperty.Address = AddressEntry.Text ?? "";
            _currentProperty.UnitNumber = UnitEntry.Text ?? "";
            _currentProperty.MonthlyRent = rent;

            bool success = await _service.UpdatePropertyAsync(_currentProperty);
            if (success)
            {
                await DisplayAlert("Success", "Property updated!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Error", "Failed to update property.", "OK");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}