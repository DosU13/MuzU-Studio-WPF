using System.Xml.Linq;

namespace MuzUStandard.data
{
    public class NodeList : XmlList<Node>
    {
        internal NodeList() { }
        internal NodeList(XElement xElement) : base(xElement) { }

        internal override XElement ToXElement()
        {
            return base.ToXElement();
        }

        internal override XElement LoadFromXElement(XElement xElement)
        {
            return base.LoadFromXElement(xElement);
        }
    }
}
