using PropertyManagerApp.Services;
using PropertyManagerApp.Models;

namespace PropertyManagerApp.Views
{
    [QueryProperty(nameof(PropertyId), "propertyId")]
    public partial class PropertyDetailPage : ContentPage
    {
        private readonly PropertyService _service;

        public int PropertyId { get; set; }

        public PropertyDetailPage(PropertyService service)
        {
            InitializeComponent();
            _service = service;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var property = await _service.GetPropertyAsync(PropertyId);
            if (property != null)
            {
                NameLabel.Text = property.PropertyName;
                AddressLabel.Text = property.Address;
                UnitLabel.Text = property.UnitNumber;
                RentLabel.Text = property.MonthlyRent.ToString("C");
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}