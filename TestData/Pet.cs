using System.Collections.Generic;

namespace Modals
{
    public class Pet : Modal
    {
        public PetCategory Category { get; set; }
        public List<string> PhotoUrls { get; set; }
        public List<Tag> Tags { get; set; }
        public string Status { get; set; }
    }
}
