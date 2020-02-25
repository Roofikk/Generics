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
    public partial class GenericPage : ContentPage
    {
        Dictionary<string, List<Medicament>> medDict = new Dictionary<string, List<Medicament>>();
        public GenericPage(Dictionary<string, List<Medicament>> md)
        {
            InitializeComponent();

            List<Medicament> medList = (List<Medicament>)genericList.ItemsSource;
            medDict = md;
        }
    }
}