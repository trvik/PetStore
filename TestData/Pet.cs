using System.Collections.Generic;

namespace Modals
{
    public class Pet
    {
        public long Id { get; set; }
        public PetCategory Category { get; set; }
        public string Name { get; set; }
        public List<string> PhotoUrls { get; set; }
        public List<Tag> Tags { get; set; }
        public string Status { get; set; }

        public string GetPropertyValue(string propName) => this.GetType().GetProperty(propName).GetValue(this, null).ToString();

        public void SetPropertyValue(string propName, string propValue) => this.GetType().GetProperty(propName).SetValue(this, propValue);
    }
}
