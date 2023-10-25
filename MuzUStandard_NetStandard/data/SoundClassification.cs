using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MuzUStandard.data
{
    public class SoundClassification
    {
        public SoundClassification() { }
        public int Hue { get; set; } = random.Next(256);
        public List<Property> PropertiesList { get; set; } = new List<Property>();

        private static readonly Random random = new Random();
    }

    public class Property
    {
        public Property() { }
        public Property(string name) 
        {
            Name = name;
        }

        public string Name { get; set; } = "NoName";
        public string Value { get; set; }
    }
}
