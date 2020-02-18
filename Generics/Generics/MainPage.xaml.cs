using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Generics
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            Title = "Generic drugs";
            SearchBarRenderer

            InitializeComponent();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (medicamentList.SelectedItem != null)
            {
                string formReleases = await App.MedRequest.GetSelectedMedicament(e.SelectedItem);
                Medicament selectedMed = (Medicament)e.SelectedItem;
                string[] formRelease = formReleases.Split('\n');
                List<Medicament> list = new List<Medicament>();
                foreach (var i in formRelease)
                {
                    Medicament med = new Medicament { FormRelease = i, TradeName = selectedMed.TradeName };
                    list.Add(med);
                }

                list.RemoveAt(list.Count - 1);

                ReleasePage medicamentPage = new ReleasePage(selectedMed.TradeName)
                { 
                    Title = "Select form release"
                };
                medicamentPage.BindingContext = list;
                await Navigation.PushAsync(medicamentPage);
                medicamentList.SelectedItem = null;
            }
        }
        private async void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            HttpClient client = new HttpClient();
            if (e.NewTextValue.Length > 2)
            {
                string tradeNames = await App.MedRequest.GetTradeName(e.NewTextValue);
                string[] tradeName = tradeNames.Split('\n');
                List<Medicament> list = new List<Medicament>();
                foreach (var i in tradeName)
                {
                    Medicament med = new Medicament { TradeName = i };
                    list.Add(med);
                }
                list.RemoveAt(list.Count - 1);
                medicamentList.ItemsSource = list;
            }
            else
            {
                medicamentList.ItemsSource = null;
            }
        }
    }
}
