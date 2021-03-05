using System;
using System.Collections.Generic;
using FloridaLottery.ViewModels;
using Xamarin.Forms;

namespace FloridaLottery.Views
{
    public partial class DrawResults : ContentPage
    {
        DrawResultsViewModel _viewModel;

        public DrawResults()
        {
            InitializeComponent();

            BindingContext = _viewModel = new DrawResultsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
