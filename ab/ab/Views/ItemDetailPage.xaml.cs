using System.ComponentModel;
using Xamarin.Forms;
using ab.ViewModels;

namespace ab.Views
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
