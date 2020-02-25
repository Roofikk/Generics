using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Generics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReleasePage : ContentPage
    {
        public ReleasePage()
        {
            InitializeComponent();
        }

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (formReleaseList.SelectedItem != null)
            {
                Medicament med = (Medicament)e.SelectedItem;
                string strGenerics = await App.MedRequest.GetGeneric(med);
                JArray o = JArray.Parse(strGenerics);
                Medicament[] medicaments = JsonConvert.DeserializeObject<Medicament[]>(o.ToString());
                Dictionary<string, List<Medicament>> medDict = new Dictionary<string, List<Medicament>>();
                List<Medicament> medList = new List<Medicament>();
                List<Medicament> medListView = new List<Medicament>();
                foreach (var m in medicaments)
                {
                    medList.Add(m);
                    if (!medDict.ContainsKey(m.TradeName))
                    {
                        medDict.Add(m.TradeName, new List<Medicament> { m });
                        medListView.Add(m);
                    }
                    else
                    {
                        medDict[m.TradeName].Add(m);
                    }
                }


                GenericPage genericPage = new GenericPage(medDict) { Title = "Generics of " + this.Title + ":",
                                                            BindingContext = medListView};
                await Navigation.PushAsync(genericPage);
                formReleaseList.SelectedItem = null;
            }
        }
    }
}