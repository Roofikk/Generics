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
        public async Task<string> GetTradeName(string text)
        {
            string strReq = "http://" + ip + ":3000/?type_request=get_names&words=";
            HttpResponseMessage response = await client.GetAsync(strReq += text);
            HttpContent responseContent = response.Content;
            return await responseContent.ReadAsStringAsync();
        }

        public async Task<string> GetSelectedMedicament(object e)
        {
            Medicament med = (Medicament)e;
            string strReq = "http://" + ip + ":3000/?type_request=get_condition&name=";

            HttpResponseMessage response = await client.GetAsync(strReq += med.TradeName);
            HttpContent responseContent = response.Content;
            return await responseContent.ReadAsStringAsync();
        }
        public async Task<string> GetGeneric(Medicament med)
        {
            string strReq = "http://" + ip + ":3000/?type_request=get_drug&name=";
            string strReqSec = "&condition=";
            strReq += med.TradeName + strReqSec + med.FormRelease;

            HttpResponseMessage response = await client.GetAsync(strReq);
            HttpContent responseContent = response.Content;
            return await responseContent.ReadAsStringAsync();
        }

        public async Task<string> GetChemistryName(Medicament med)
        {
            string strReq = "http://" + ip + ":3000/?type_request=get_chemistryName&name=";
            strReq += med.TradeName;

             HttpResponseMessage response = await client.GetAsync(strReq);
            HttpContent responseContent = response.Content;
            return await responseContent.ReadAsStringAsync();
        }
    }
}
