using System.ComponentModel;
using Xamarin.Forms;
using FloridaLottery.ViewModels;

namespace FloridaLottery.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}