using MuzUStandard.data.util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MuzUStandard.data
{
    public class Tempo : XmlBase
    {
        internal Tempo() {
            MicrosecondsPerQuarterNote = 500_000;
            TimeSignature = new TimeSignature();
        }
        internal Tempo(XElement xElement) : base(xElement) { }

        public long MicrosecondsPerQuarterNote;
        public TimeSignature TimeSignature;
        public double BPM {
            get => Utils.GetBPM(MicrosecondsPerQuarterNote, TimeSignature);
            set => MicrosecondsPerQuarterNote = Utils.GetMicrosecondsPerQuarterNote(value, TimeSignature);
        }

        internal override XElement ToXElement()
        {
            var xElement = base.ToXElement();
            xElement.Add(new XElement(nameof(MicrosecondsPerQuarterNote), MicrosecondsPerQuarterNote),
                            TimeSignature.ToXElement());
            return xElement;
        }

        internal override XElement LoadFromXElement(XElement xElement)
        {
            var thisElement = base.LoadFromXElement(xElement);
            MicrosecondsPerQuarterNote = long.Parse(thisElement.Element(nameof(MicrosecondsPerQuarterNote))?.Value ?? "0");
            TimeSignature = new TimeSignature(thisElement);
            return thisElement;
        }
    }

    public class TimeSignature : XmlBase
    {
        internal TimeSignature() { }
        internal TimeSignature(XElement xElement) : base(xElement) { }

        public int Numerator = 4;
        public int Denominator = 4;

        internal override XElement ToXElement()
        {
            var xElement = base.ToXElement();
            xElement.Add(new XElement(nameof(Numerator), Numerator),
                         new XElement(nameof(Denominator), Denominator));
            return xElement;
        }

        internal override XElement LoadFromXElement(XElement xElement)
        {
            var thisElement = base.LoadFromXElement(xElement);
            Numerator = int.Parse(thisElement.Element(nameof(Numerator))?.Value ?? "0");
            Denominator = int.Parse(thisElement.Element(nameof(Denominator))?.Value ?? "0");
            return thisElement;
        }
    }
}
