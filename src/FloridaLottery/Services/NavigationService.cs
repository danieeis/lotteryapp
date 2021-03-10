using System;
using FloridaLottery.Views;
using Xamarin.Forms;

namespace FloridaLottery.Services
{
    public class NavigationService
    {
        public void NavigatoTo(string name, object bindingContext)
        {
            Shell.Current.Navigation.PushAsync(new ForecastResultsView() { BindingContext = bindingContext});
        }
    }
}
