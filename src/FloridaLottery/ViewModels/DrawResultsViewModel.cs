using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Domain;
using Xamarin.Forms;

namespace FloridaLottery.ViewModels
{
    public class DrawResultsViewModel : BaseViewModel
    {
        Gem _selectedGem;
        public ObservableCollection<Gem> Gems { get; }
        public Command LoadGemsCommand { get; }

        public DrawResultsViewModel()
        {
            Title = "Resultados";
            Gems = new ObservableCollection<Gem>();

            LoadGemsCommand = new Command(async () => await ExecuteLoadGemsCommand());
        }

        async Task ExecuteLoadGemsCommand()
        {
            IsBusy = true;

            try
            {
                Gems.Clear();
                var items = await GemStore.GetItemsAsync(true);
                foreach (var Gem in items)
                {
                    Gems.Add(Gem);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedGem = null;
        }

        public Gem SelectedGem
        {
            get => _selectedGem;
            set
            {
                SetProperty(ref _selectedGem, value);
            }
        }
    }
}
