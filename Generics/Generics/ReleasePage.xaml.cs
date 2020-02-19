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
                //Следует удалить
                string[] strParse = strGenerics.Split("}{".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string newStr = "[{";
                for (int i = 0; i < strParse.Length; i++)
                {
                    if (i != strParse.Length - 1)
                        newStr += strParse[i] + "},{";
                    else
                        newStr += strParse[i];
                }
                newStr += "}]";
                //Следует удалить
                JArray o = JArray.Parse(newStr);
                Medicament[] medicaments = JsonConvert.DeserializeObject<Medicament[]>(o.ToString());
                List<Medicament> medList = new List<Medicament>();
                foreach (var m in medicaments)
                {
                    medList.Add(m);
                }

                GenericPage genericPage = new GenericPage();
                genericPage.BindingContext = medList;
                await Navigation.PushAsync(genericPage);
                formReleaseList.SelectedItem = null;
            }
        }
    }
}