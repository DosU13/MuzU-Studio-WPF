using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MuzUStandard.data
{
    public class SequenceTemplate 
    {
        public SequenceTemplate() { }

        public bool LengthEnabled { get; set; } = false;
        public bool NoteEnabled { get; set; } = false;
        public bool LyricsEnabled { get; set; } = false;
        public List<Property> PropertiesList { get; set; } = new List<Property>();
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
