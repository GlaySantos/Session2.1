using Newtonsoft.Json;
using RestAPI_HttpClient.DataModels;
using RestAPI_HttpClient.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI_HttpClient.Helpers
{
    public class PetHelper: Endpoints
    {
        public readonly List<PetModel> cleanUpList = new List<PetModel>();

        public PetModel GetPetById(long petId)
        {
            var httpResponse = httpClient.GetAsync(GetURL($"{PetsEndpoint}/{petId}"));
            PetModel petData = JsonConvert.DeserializeObject<PetModel>(httpResponse.Result.Content.ReadAsStringAsync().Result);

            return petData;
        }

        public HttpResponseMessage PostMethod(PetModel petModel)
        {
            var request = JsonConvert.SerializeObject(petModel);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Request
            var httpResponse = httpClient.PostAsync(GetURL(PetsEndpoint), postRequest);
            var httpResponseMessage = httpResponse.Result;

            return httpResponseMessage;
        }

        public HttpResponseMessage PutMethod(PetModel petModel)
        {

            // Serialize Content
            var request = JsonConvert.SerializeObject(petModel);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Put Request
            var httpResponse = httpClient.PutAsync(GetURL($"{PetsEndpoint}"), postRequest);
            var httpResponseMessage = httpResponse.Result;

            return httpResponseMessage;
        }

    }
}
