using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Generics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReleasePage : ContentPage
    {
        string TradeName;
        public ReleasePage(string tradeName)
        {
            InitializeComponent();
            TradeName = tradeName;
        }

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Medicament med = (Medicament)e.SelectedItem;
            string strGenerics = await App.MedRequest.GetGeneric(med);
        }
    }
}