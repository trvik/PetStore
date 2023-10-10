using System;
using System.Net.Http;
using Modals;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Reflection;
using System.Collections;
using System.Linq;

namespace Context
{
    public class APIContext
    {
        private static readonly HttpClient client = new HttpClient();

        public static void CreatePetViaAPIAsync(Pet pet)
        {
            #region Data

            var url = $"{ DataForTests.BaseUrl }{ DataForTests.PetUrl }";

            #endregion

            var response = client.PostAsJsonAsync<Pet>(url, pet).Result;
            response.EnsureSuccessStatusCode();

            GeneralContext.Pet = response.Content.ReadFromJsonAsync<Pet>().Result;
            GeneralContext.ResponseStatus = response.StatusCode.ToString();
        }

        public static void FindPetsByStatus(string status)
        {
            #region Data

            var url = $"{ DataForTests.BaseUrl }{ DataForTests.PetUrl }/findByStatus?status={ status }";

            #endregion

            var response = client.GetAsync(new Uri(url)).Result;
            response.EnsureSuccessStatusCode();

            var responseData = response.Content.ReadAsStringAsync().Result;

            var foundPetsResponse = JsonConvert.DeserializeObject<List<Pet>>(responseData);

            GeneralContext.ResponseStatus = response.StatusCode.ToString();
            GeneralContext.Pets = foundPetsResponse;
        }

        public static void FindPetsById(long id)
        {
            #region Data

            var url = $"{ DataForTests.BaseUrl }{ DataForTests.PetUrl }/{ id }";

            #endregion

            var response = client.GetAsync(new Uri(url)).Result;
            response.EnsureSuccessStatusCode();

            var responseData = response.Content.ReadAsStringAsync().Result;

            var foundPetsResponse = JsonConvert.DeserializeObject<Pet>(responseData);

            GeneralContext.ResponseStatus = response.StatusCode.ToString();
            GeneralContext.Pet = foundPetsResponse;
        }

        public static bool VerifyResponseStatus(string expectedStatus) => GeneralContext.ResponseStatus == expectedStatus;

        public static string GetPetStatus(Pet pet) => pet.Status;

        public static void UpdatePetFieldToValue(Pet pet, List<(string Object, string Field, string CurrentValue, string ValueToUpdate)> petFieldsToUpdate)
        {
            #region Data

            var petId = pet.Id;
            var url = $"{ DataForTests.BaseUrl }{ DataForTests.PetUrl }";

            #endregion

            foreach (var petFieldToUpdate in petFieldsToUpdate)
            {
                object targetObject = petFieldToUpdate.Object != "Pet" ? (pet.GetType().GetProperty(petFieldToUpdate.Object)?.GetValue(pet)) : pet;
                switch (petFieldToUpdate.Object)
                {
                    case "Pet":
                        (targetObject as Pet).SetPropertyValue(petFieldToUpdate.Field, petFieldToUpdate.ValueToUpdate);
                        break;
                    case "Category":
                        (targetObject as PetCategory).SetPropertyValue(petFieldToUpdate.Field, petFieldToUpdate.ValueToUpdate);
                        break;
                    case "Tags":
                        (targetObject as List<Tag>).First(tag => tag.Name == petFieldToUpdate.CurrentValue || tag.Id.ToString() == petFieldToUpdate.CurrentValue)
                            .SetPropertyValue(petFieldToUpdate.Field, petFieldToUpdate.ValueToUpdate);
                        break;
                }
            }

            var response = client.PutAsJsonAsync<Pet>(url, pet).Result;
            response.EnsureSuccessStatusCode();

            GeneralContext.Pet = response.Content.ReadFromJsonAsync<Pet>().Result;
            GeneralContext.ResponseStatus = response.StatusCode.ToString();
        }

        public static string GetActualFieldValue(Pet pet, (string Object, string Field, string Value) petFieldValue)
        {
            string actualValue = null;
            var targetObject = pet.GetType().GetProperty(petFieldValue.Object)?.GetValue(pet);
            switch (petFieldValue.Object)
            {
                case "Pet":
                    actualValue = pet.GetPropertyValue(petFieldValue.Field);
                    break;
                case "Category":
                    actualValue = (targetObject as PetCategory).GetPropertyValue(petFieldValue.Field);
                    break;
                case "Tags":
                    actualValue = (targetObject as List<Tag>).First(tag => tag.Name == petFieldValue.Value || tag.Id.ToString() == petFieldValue.Value)
                   .GetPropertyValue(petFieldValue.Field);
                    break;
            }
            return actualValue;
        }
    }
}
