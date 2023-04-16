using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MuzUStandard.data
{
    public enum TimeUnite { Microsecond, Musical, Both}

    static class Extensions
    {
        internal static TimeUnite ParseToTimeUnit(string str)
        {
            if (str == TimeUnite.Microsecond.ToString()) return TimeUnite.Microsecond;
            else if (str == TimeUnite.Musical.ToString()) return TimeUnite.Musical;
            else return TimeUnite.Both;
        }
    }

    public class SequenceTemplate : XmlBase
    {
        public SequenceTemplate() { }
        internal SequenceTemplate(XElement xElement) : base(xElement) { }

        public bool LengthEnabled { get; set; } = false;
        public bool NoteEnabled { get; set; } = false;
        public bool LyricsEnabled { get; set; } = false;
        public TimeUnite TimeUnit { get; set; } = TimeUnite.Both;
        public PropertiesList PropertiesList { get; set; } = new PropertiesList();

        internal override XElement ToXElement()
        {
            var xElement = base.ToXElement();
            xElement.Add(new XElement(nameof(LengthEnabled), LengthEnabled),
                         new XElement(nameof(NoteEnabled), NoteEnabled),
                         new XElement(nameof(LyricsEnabled), LyricsEnabled),
                         new XElement(nameof(TimeUnit), TimeUnit.ToString()),
                         PropertiesList.ToXElement());
            return xElement;
        }

        internal override XElement LoadFromXElement(XElement xElement)
        {
            var thisElement = base.LoadFromXElement(xElement);
            LengthEnabled = bool.Parse(thisElement.Element(nameof(LengthEnabled)).Value);
            NoteEnabled = bool.Parse(thisElement.Element(nameof(NoteEnabled)).Value);
            LyricsEnabled = bool.Parse(thisElement.Element(nameof(LyricsEnabled)).Value);
            TimeUnit = Extensions.ParseToTimeUnit(thisElement.Element(nameof(TimeUnit)).Value);
            PropertiesList = new PropertiesList(thisElement);
            return thisElement;
        }
    }

    public class PropertiesList : XmlList<Property>
    {
        internal PropertiesList() { }
        internal PropertiesList(XElement xElement) : base(xElement) { }

        internal override XElement ToXElement()
        {
            return base.ToXElement();
        }

        internal override XElement LoadFromXElement(XElement xElement)
        {
            return base.LoadFromXElement(xElement);
        }
    }

    public class Property : XmlBase
    {
        public Property() { }
        public Property(string name) 
        {
            Name = name;
        }

        public Property(XElement xElement) : base(xElement) { }

        public string Name { get; set; } = "NoName";
        public string Value { get; set; }

        internal override XElement ToXElement()
        {
            var xElement = base.ToXElement();
            xElement.Add(new XElement(nameof(Name), Name));
            xElement.Add(new XElement(nameof(Value), Value));
            return xElement;
        }

        internal override XElement LoadFromXElement(XElement xElement)
        {
            var thisElement = base.LoadFromXElement(xElement);
            Name = thisElement.Element(nameof(Name)).Value;
            Value = thisElement.Element(nameof(Value)).Value;
            return thisElement;
        }
    }

    //internal static class Extensions
    //{
    //    internal static string _ToString(this ValueType type)
    //    {
    //        if (type == ValueType.Integer) return "Integer";
    //        else if (type == ValueType.Decimal) return "Decimal";
    //        else return "NoWay this is impossible";
    //    }

    //    internal static ValueType ParseToValueType(this String type)
    //    {
    //        if (type == "Integer") return ValueType.Integer;
    //        else return ValueType.Decimal;
    //    }
    //}

    //internal enum ValueType { Integer, Decimal } //TinyText}
}
