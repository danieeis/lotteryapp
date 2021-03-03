using System;
using System.Collections.Generic;
using FloridaLottery.ViewModels;
using FloridaLottery.Views;
using Xamarin.Forms;

namespace FloridaLottery
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
