using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain;
using FloridaLottery.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using FloridaLottery.Services.GemsBuilder;

namespace FloridaLottery.ViewModels
{
    public class PickLastDrawingsViewModel : BaseViewModel
    {
#if DEBUG
        public int NumberSequence = 1234;
#else
        public int NumberSequence = -1;
#endif

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

        private Task SelectDrawNumbers()
        {
            while (LastDrawings.Count != DrawingNumbers)
            {
                if (LastDrawings.Count > DrawingNumbers)
                    LastDrawings.RemoveAt(-1);
                else if (LastDrawings.Count < DrawingNumbers)
                    LastDrawings.Add(new Draw(NumberSequence == -1 ? string.Empty : NumberSequence++.ToString()));
            }

            return Task.CompletedTask;
        }
        private Task CreateForecast()
        {
            var validationFailed = LastDrawings.Any(x => string.IsNullOrWhiteSpace(x.Number));
            if (validationFailed)
            {
                Console.WriteLine("Complete todos los sorteos seleccionados");
                return Task.FromException(new ArgumentNullException());
            }
            if (DrawingNumbers < 30)
            {
                DrawingNumbers = 30;
                SelectDrawNumbers();
            }
            Gem diamante = new Gem("Diamante");
            diamante.Draws = LastDrawings.GenerateDiamont().ToList();
            
            List<Gem> Gems = new List<Gem>() {
                    diamante
                };

            var forecast = new ForecastResultsViewModel(Gems);
            Xamarin.Forms.DependencyService.Get<NavigationService>().NavigatoTo("", forecast);
            return Task.CompletedTask;
        }
    }
}

