using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MuzUStandard.data
{
    public abstract class XmlInfo: XmlBase
    {
        internal XmlInfo() { }

        internal XmlInfo(XElement xElement) : base(xElement) { }

        internal readonly Dictionary<string, string> Infos = new Dictionary<string, string>();
        
        internal override XElement ToXElement()
        {
            var xElement = base.ToXElement();
            xElement.Add(Infos.Select(x => new XElement(x.Key, x.Value)));
            return xElement;
        }

        internal override XElement LoadFromXElement(XElement xElement)
        {
            var thisElement = base.LoadFromXElement(xElement);
            foreach (var x in thisElement.Elements()) { Infos.Add(x.Name.LocalName, x.Value); }
            return thisElement;
        }
    }
}
