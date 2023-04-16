using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MuzUStandard.data
{
    public abstract class XmlList<T> : XmlBase where T : XmlBase, new()
    {
        internal XmlList() { List = new List<T>(); }
        
        internal XmlList(XElement xElement) : base(xElement) { }

        public List<T> List;

        internal override XElement ToXElement()
        {
            var xElement = base.ToXElement();
            xElement.Add(new XAttribute("count", List.Count));
            for (int i = 0; i < List.Count; i++)
            {
                xElement.Add(new XElement("item-" + i, List[i]?.ToXElement()));
            }
            return xElement;
        }

        internal override XElement LoadFromXElement(XElement xElement)
        {
            var thisElement = base.LoadFromXElement(xElement);
            int count = int.Parse(thisElement.Attribute("count").Value);
            List = new List<T>(count);
            for (int i = 0; i < count; i++)
            {
                XElement item = thisElement.Element("item-" + i);
                T t = new T();
                t.LoadFromXElement(item);
                List.Insert(i, t);
            }
            return thisElement;
        }
    }
}
