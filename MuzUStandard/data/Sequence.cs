using System.Xml.Linq;

namespace MuzUStandard.data
{
    public class Sequence : XmlBase
    {
        public Sequence(){}
        internal Sequence(XElement xElement) : base(xElement) { }

        public string Name { get; set; } = "";
        public SequenceTemplate SequenceTemplate { get; set; } = new SequenceTemplate();
        public NodeList NodeList { get; set; } = new NodeList();

        internal override XElement ToXElement()
        {
            var xElement = base.ToXElement();
            xElement.Add(new XElement(nameof(Name), Name),
                         SequenceTemplate.ToXElement(),
                         NodeList.ToXElement());
            return xElement;
        }

        internal override XElement LoadFromXElement(XElement xElement)
        {
            var thisElement = base.LoadFromXElement(xElement);
            Name = thisElement.Element(nameof(Name)).Value;
            SequenceTemplate = new SequenceTemplate(thisElement);
            NodeList = new NodeList(thisElement);
            return thisElement;
        }
    }
}
