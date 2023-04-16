using System.Collections.Generic;
using System.Xml.Linq;

namespace MuzUStandard.data
{
    public class Node : XmlBase
    {
        public Node() { }
        internal Node(XElement xElement):base(xElement) { }

        /// <summary>
        /// Time as micro seconds
        /// </summary>
        public long Time { get; set; } = 0;
        /// <summary>
        /// Time as micro seconds
        /// </summary>
        public long? Length { get; set; }
        public int? Note{ get; set; }
        public string Lyrics { get; set; }

        internal override XElement ToXElement()
        {
            var xElement = base.ToXElement();
            xElement.Add(new XAttribute(nameof(Time), Time));
            if (Length != null) xElement.Add(new XAttribute(nameof(Length), Length));
            if (Note != null) xElement.Add(new XElement(nameof(Note), Note));
            if (Lyrics != null) xElement.Add(new XElement(nameof(Lyrics), Lyrics));
            return xElement;
        }

        internal override XElement LoadFromXElement(XElement xElement)
        {
            var thisElement = base.LoadFromXElement(xElement);
            Time = long.TryParse(thisElement.Attribute(nameof(Time))?.Value, out long tm) ? tm : 0L;
            Length = long.TryParse(thisElement.Attribute(nameof(Length))?.Value, out long lm) ? lm as long? : null;
            Note = int.TryParse(thisElement.Element(nameof(Note))?.Value, out int _note) ? _note as int? : null;
            Lyrics = thisElement.Element(nameof(Lyrics))?.Value;
            return thisElement;
        }
    }
}
