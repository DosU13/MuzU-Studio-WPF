using System.Xml.Linq;

namespace MuzUStandard.data
{
    public abstract class XmlBase
    {
        internal XmlBase() { }
        internal XmlBase(XElement xElement) { LoadFromXElement(xElement); }

        internal virtual XElement ToXElement() { 
            return new XElement(GetType().Name);
        }
        internal virtual XElement LoadFromXElement(XElement xElement) {
            return xElement.Element(GetType().Name);
        }
    }
}
