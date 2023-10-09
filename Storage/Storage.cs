using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Modals;

namespace Storage
{
    public class Storage
    {
        public static Pet GetDataForCreatingPet(string petCategory, string petName)
        {
            string petsString = File.ReadAllText($"../../../../Storage/Data/{ petCategory}.json");
            List<Pet> pets = JsonSerializer.Deserialize<List<Pet>>(petsString);
            return pets.Find(pet => pet.Name == petName);
        }
    }
}
