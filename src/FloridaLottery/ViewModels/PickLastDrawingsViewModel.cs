using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain;
using FloridaLottery.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace FloridaLottery.ViewModels
{
    public class PickLastDrawingsViewModel : BaseViewModel
    {
        int _drawingNumbers;
        public int DrawingNumbers
        {
            get => _drawingNumbers;
            set => SetProperty(ref _drawingNumbers, value);
        }

        ObservableCollection<Draw> _lastDrawings;
        public ObservableCollection<Draw> LastDrawings
        {
            get => _lastDrawings;
            set => SetProperty(ref _lastDrawings, value);
        }

        public PickLastDrawingsViewModel()
        {
            Title = "Lottery App";
            DrawingNumbers = 10;
            LastDrawings = new ObservableCollection<Draw>();
            Task.Run(SelectDrawNumbers);
        }

        public ICommand SelectDrawNumbersCommand
        {
            get
            {
                return new AsyncCommand(SelectDrawNumbers);
            }
        }

        public ICommand CreateForecastCommand
        {
            get
            {
                return new AsyncCommand(CreateForecast);
            }
        }

        private Task CreateForecast()
        {
            var validationFailed = LastDrawings.Any(x => x.Number == 0);
            if (validationFailed)
            {
                Console.WriteLine("Complete todos los sorteos seleccionados");
            }

            Gem diamante = new Gem("Diamante");
            diamante.Draws = LastDrawings;
            Gem zafiro = new Gem("Zafiro");
            zafiro.Draws = LastDrawings;
            List<Gem> Gems = new List<Gem>() {
                    diamante, zafiro
                };

            var forecast = new ForecastResultsViewModel(Gems);
            Shell.Current.Navigation.PushAsync(new ForecastResultsView() { BindingContext = forecast });
            return Task.CompletedTask;
        }

        private Task SelectDrawNumbers()
        {
            while (LastDrawings.Count != DrawingNumbers)
            {
                if (LastDrawings.Count > DrawingNumbers)
                    LastDrawings.RemoveAt(-1);
                else if (LastDrawings.Count < DrawingNumbers)
                    LastDrawings.Add(new Draw(4321));
            }

            return Task.CompletedTask;
        }
    }
}

