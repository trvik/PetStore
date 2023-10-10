using System;
using System.Collections.Generic;
using System.Text;
using Modals;

namespace Context
{
    public class GeneralContext
    {
        public static string ResponseStatus { get; set; }

        public static Pet Pet {get; set;}

        public static List<Pet> Pets {get; set;}
    }
}
