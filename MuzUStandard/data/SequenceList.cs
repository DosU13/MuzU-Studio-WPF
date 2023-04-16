using System.Xml.Linq;

namespace MuzUStandard.data
{
    public class SequenceList : XmlList<Sequence>
    {
        internal SequenceList() { }
        internal SequenceList(XElement xElement) : base(xElement) { }

        internal override XElement ToXElement()
        {
            return base.ToXElement();
        }

        internal override XElement LoadFromXElement(XElement xElement) => base.LoadFromXElement(xElement);
    }
}
