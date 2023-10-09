using NUnit.Framework;
using TechTalk.SpecFlow;
using Context;
using System.Linq;
using System.Collections.Generic;
using Modals;

namespace StepDefinitions
{
    [Binding]
    public static class PetsSteps
    {
        [Given("I create a '(.*)' pet with '(.*)' name")]
        public static void CreatePet(string petCategory, string petName)
        {
            #region Data

            var pet = Storage.Storage.GetDataForCreatingPet(petCategory, petName);

            #endregion

            APIContext.CreatePetViaAPIAsync(pet);
        }

        [StepDefinition("I find the pets by '(.*)' status")]
        public static void FindPetsByStatus(string status) =>
            APIContext.FindPetsByStatus(status);

        [StepDefinition("I get response with '(.*)' status")]
        public static void VerifyResponseStatus(string expectedStatus) =>
            Assert.IsTrue(APIContext.VerifyResponseStatus(expectedStatus), $"Response status IS NOT {expectedStatus}");

        [StepDefinition("I update pet fields to value")]
        public static void UpdatePetFieldTo(Table valueToUpdate)
        {
            #region Data

            var petFieldToUpdate = valueToUpdate.Rows.Select(row =>
                        (
                            Object: row["Object"],
                            Field: row["Field"],
                            CurrentValue: row["CurrentValue"],
                            ValueToUpdate: row["ValueToUpdate"]
                        )).ToList();

            var pet = GeneralContext.Pet;

            #endregion

            APIContext.UpdatePetFieldToValue(pet, petFieldToUpdate);
        }

        [Then("All pets have '(.*)' status")]
        public static void VerifyAllPetsStatus(string expectedPetStatus)
        {
            Assert.Multiple(() =>
            {
                foreach (var pet in GeneralContext.Pets)
                    Assert.That(APIContext.GetPetStatus(pet).Equals(expectedPetStatus));
            });
        }

        [Then("pet is created with correct values")]
        [Then("pet has correct updated value")]
        public static void VerifyIsPetVaruesAreCorrect(Table expectedPetValues)
        {
            #region Data

            var petFieldsValues = expectedPetValues.Rows.Select(row =>
                        (
                            Object: row["Object"],
                            Field: row["Field"],
                            Value: row["Value"]
                        )).ToList();

            var pet = GeneralContext.Pet;

            #endregion

            Assert.Multiple(() =>
            {
                foreach (var petFieldValue in petFieldsValues)
                {
                    string expectedValue = petFieldValue.Value;                 
                    Assert.That(APIContext.GetActualFieldValue(pet, petFieldValue), Is.EqualTo(expectedValue));
                }
            });
        }
    }
}
