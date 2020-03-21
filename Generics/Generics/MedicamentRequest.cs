using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Localhost;

namespace Generics
{
    public class MedicamentRequest
    {
        HttpClient client = new HttpClient();
        readonly string ip = CrossLocalhost.Current.Ip;
        string domen = "http://192.168.0.158:3000/?type_request=";
        public async Task<string> GetTradeName(string text)
        {
            string strReq = domen + "get_names&words=" + text;
            HttpResponseMessage response = await client.GetAsync(strReq);
            HttpContent responseContent = response.Content;
            return await responseContent.ReadAsStringAsync();
        }

        public async Task<string> GetSelectedMedicament(object e)
        {
            Medicament med = (Medicament)e;
            string strReq = domen + "get_condition&name=" + med.TradeName;

            HttpResponseMessage response = await client.GetAsync(strReq);
            HttpContent responseContent = response.Content;
            return await responseContent.ReadAsStringAsync();
        }
        public async Task<string> GetGeneric(Medicament med)
        {
            string strReq = domen + "get_generics_with_condition&chemistryName=";
            string strReqSec = "&condition=";
            strReq += med.ChemistryName + strReqSec + med.FormRelease;

            HttpResponseMessage response = await client.GetAsync(strReq);
            HttpContent responseContent = response.Content;
            return await responseContent.ReadAsStringAsync();
        }

        public async Task<string> GetChemistryName(Medicament med)
        {
            string strReq = domen + "get_chemistryName&name=";
            strReq += med.TradeName;

             HttpResponseMessage response = await client.GetAsync(strReq);
            HttpContent responseContent = response.Content;
            return await responseContent.ReadAsStringAsync();
        }
    }
}
