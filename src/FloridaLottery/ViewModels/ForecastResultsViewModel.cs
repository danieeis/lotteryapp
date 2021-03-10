using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain;

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

