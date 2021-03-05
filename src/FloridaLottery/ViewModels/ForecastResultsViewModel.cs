using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace FloridaLottery.ViewModels
{
    public class ForecastResultsViewModel : BaseViewModel
    {
        ObservableCollection<Gem> _forecasts;
        public ObservableCollection<Gem> Forecasts
        {
            get => _forecasts;
            set => SetProperty(ref _forecasts, value);
        }

        public ForecastResultsViewModel(IList<Gem> gems)
        {
            Title = "Pronósticos";
            Forecasts = new ObservableCollection<Gem>(gems);
        }
    }
}

