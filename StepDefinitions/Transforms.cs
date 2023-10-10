using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace StepDefinitions
{
    [Binding]
    class Transforms
    {
        [StepArgumentTransformation]
        public Dictionary<string, string> TwoColumnsTableToDictionary(Table values)
        {
            var dictionary = new Dictionary<string, string>();
            foreach(var row in values.Rows)
                dictionary.Add(row.Values.ElementAt(0), row.Values.ElementAt(1));
            return dictionary;
        }

        [StepArgumentTransformation]
        public List<(string, string, string)> ThreeColumnsTableToTurple(Table values)
        {
            var tupleList = new List<(string, string, string)>();
            foreach (var row in values.Rows)
                tupleList.Add((row.Values.ElementAt(0), row.Values.ElementAt(1), row.Values.ElementAt(2)));
            return tupleList;
        }
    }
}
