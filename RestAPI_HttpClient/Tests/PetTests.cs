using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestAPI_HttpClient.DataModels;
using RestAPI_HttpClient.Helpers;
using System.Net;
using System.Text;

namespace RestAPI_HttpClient.Tests
{
    [TestClass]
    public class PetTests : PetHelper
    {
        [TestInitialize]
        public void TestInitialize()
        {
            httpClient = new HttpClient();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var httpResponse = httpClient.DeleteAsync(GetURL($"{PetsEndpoint}/{data.Id}"));
            }
        }

        [TestMethod]
        public void PutMethod()
        {

            #region arrange
            PetModel petData = new PetModel();
            List<Category> categories = new List<Category>();
            
            // Create Json Object
            categories.Add(new Category()
            {
                Id = 11111,
                Name = "Pets"
            });

            petData = new PetModel()
            {
                Id = 9223372036854256000,
                Category = new Category()
                {
                    Id = 0,
                    Name = "Domestic"
                },
                Name = "abc",
                PhotoUrls = new List<string>() { "someUrl" },
                Tags = categories,
                Status = "available"
            };

            #endregion

            #region act
            //POST
            var statusCode = PostMethod(petData);

            // GET by Id
            var listPetData = GetPetById(petData.Id);


            // PUT
            petData = new PetModel()
            {
                Id = listPetData.Id,
                Category = listPetData.Category,
                Name = listPetData.Name,
                PhotoUrls = listPetData.PhotoUrls,
                Tags = listPetData.Tags,
                Status = "sold"
            };

            statusCode = PutMethod(petData);


            // GET by Id
            listPetData = GetPetById(petData.Id);


            // Clean Up Data
            cleanUpList.Add(listPetData);

            #endregion

            #region assert

            // Assertion
            Assert.AreEqual(HttpStatusCode.OK, statusCode.StatusCode, "Status code is not equal to 201");
            Assert.AreEqual(petData.Id, listPetData.Id, "Id not matching");
            Assert.AreEqual(petData.Category.Id, listPetData.Category.Id, "Category Id not matching");
            Assert.AreEqual(petData.Category.Name, listPetData.Category.Name, "Category Name not matching");
            Assert.AreEqual(petData.Name, listPetData.Name, "Name not matching");
            Assert.AreEqual(petData.PhotoUrls[0], listPetData.PhotoUrls[0], "PhotoUrls not matching");
            Assert.AreEqual(petData.Tags[0].Id, listPetData.Tags[0].Id, "Tags Id not matching");
            Assert.AreEqual(petData.Tags[0].Name, listPetData.Tags[0].Name, "Tags Name not matching");
            Assert.AreEqual(petData.Status, listPetData.Status, "Status not matching");

            #endregion

        }


        //[TestMethod]
        //public async Task PutMethod2()
        //{


        //    // Create Json Object
        //    List<Category> categories = new List<Category>();
        //    categories.Add(new Category()
        //    {
        //        Id = 11111,
        //        Name = "Pets"
        //    });


        //    #region create data and send post request
        //    // Create Json Object
        //    PetModel petData = new PetModel()
        //    {
        //        Id = 9223372036854256000,
        //        Category = new Category()
        //        {
        //            Id = 0,
        //            Name = "Domestic"
        //        },
        //        Name = "abc",
        //        PhotoUrls = new List<string>() { "someUrl" },
        //        Tags = categories,
        //        Status = "available"
        //    };

        //    // Serialize Content
        //    var request = JsonConvert.SerializeObject(petData);
        //    var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

        //    // Send Post Request
        //    await httpClient.PostAsync(GetURL(PetsEndpoint), postRequest);

        //    #endregion

        //    #region get Id of the created data

        //    // Get Request
        //    var getResponse = await httpClient.GetAsync(GetURI($"{PetsEndpoint}/{petData.Id}"));

        //    // Deserialize Content
        //    var listPetData = JsonConvert.DeserializeObject<PetModel>(getResponse.Content.ReadAsStringAsync().Result);

        //    // filter created data
        //    var createdPetData = listPetData.Id;

        //    #endregion

        //    #region send put request to update data

        //    // Update value of petData
        //    petData = new PetModel()
        //    {
        //        Id = 9223372036854256000,
        //        Category = listPetData.Category,
        //        Name = listPetData.Name,
        //        PhotoUrls = listPetData.PhotoUrls,
        //        Tags = listPetData.Tags,
        //        Status = "sold"
        //    };


        //    // Serialize Content
        //    request = JsonConvert.SerializeObject(petData);
        //    postRequest = new StringContent(request, Encoding.UTF8, "application/json");

        //    // Send Put Request
        //    var httpResponse = await httpClient.PutAsync(GetURL($"{PetsEndpoint}"), postRequest);

        //    // Get Status Code
        //    var statusCode = httpResponse.StatusCode;

        //    #endregion

        //    #region get updated data

        //    // Get Request
        //    getResponse = await httpClient.GetAsync(GetURI($"{PetsEndpoint}/{petData.Id}"));

        //    // Deserialize Content
        //    listPetData = JsonConvert.DeserializeObject<PetModel>(getResponse.Content.ReadAsStringAsync().Result);

        //    // filter created data
        //    //var createdPetData = listPetData;

        //    #endregion

        //    #region cleanup data

        //    // Add data to cleanup list
        //    cleanUpList.Add(listPetData);

        //    #endregion

        //    #region assertion

        //    // Assertion
        //    Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status code is not equal to 201");
        //    Assert.AreEqual(petData.Id, listPetData.Id, "Id not matching");
        //    Assert.AreEqual(petData.Category.Id, listPetData.Category.Id, "Category Id not matching");
        //    Assert.AreEqual(petData.Category.Name, listPetData.Category.Name, "Category Name not matching");
        //    Assert.AreEqual(petData.Name, listPetData.Name, "Name not matching");
        //    Assert.AreEqual(petData.PhotoUrls[0], listPetData.PhotoUrls[0], "PhotoUrls not matching");
        //    Assert.AreEqual(petData.Tags[0].Id, listPetData.Tags[0].Id, "Tags Id not matching");
        //    Assert.AreEqual(petData.Tags[0].Name, listPetData.Tags[0].Name, "Tags Name not matching");
        //    Assert.AreEqual(petData.Status, listPetData.Status, "Status not matching");

        //    #endregion

        //}
    }
}