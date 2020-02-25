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
        List<Medicament> medList = new List<Medicament>();
        
        public MainPage()
        {
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            Title = "Generic drugs";

            InitializeComponent();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (medicamentList.SelectedItem != null)
            {
                string formReleases = await App.MedRequest.GetSelectedMedicament(e.SelectedItem);
                string chemistryName = await App.MedRequest.GetChemistryName((Medicament)e.SelectedItem);
                Medicament selectedMed = (Medicament)e.SelectedItem;
                string[] formRelease = formReleases.Split('\n');
                List<Medicament> list = new List<Medicament>();
                foreach (var i in formRelease)
                {
                    Medicament med = new Medicament { FormRelease = i, TradeName = selectedMed.TradeName, ChemistryName = chemistryName};
                    list.Add(med);
                }

                list.RemoveAt(list.Count - 1);

                ReleasePage medicamentPage = new ReleasePage()
                { 
                    Title = selectedMed.TradeName
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
                if (e.NewTextValue.Length == 3 && medList.Count == 0)
                {
                    string tradeNames = await App.MedRequest.GetTradeName(e.NewTextValue);
                    string[] tradeName = tradeNames.Split('\n');
                    foreach (var i in tradeName)
                    {
                        Medicament med = new Medicament { TradeName = i };
                        medList.Add(med);
                    }

                    medicamentList.ItemsSource = medList;
                }
                else
                {
                    int i = 0;
                    List<Medicament> newListMed = new List<Medicament>();
                    
                    foreach (var m in medList)
                    {
                        if (m.TradeName.ToLower().StartsWith(e.NewTextValue.ToLower()))
                        {
                            newListMed.Add(m);
                            i++;
                        }
                    }

                    medicamentList.ItemsSource = newListMed;
                }
            }
            else
            {
                medicamentList.ItemsSource = null;
                medList.Clear();
            }
        }
    }
}
