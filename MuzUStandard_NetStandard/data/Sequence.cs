using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MuzUStandard.data
{
    public class Sequence
    {
        public Sequence(){}

        public string Name { get; set; } = "";
        public SequenceTemplate SequenceTemplate { get; set; } = new SequenceTemplate();
        public SoundClassification SoundClassification { get; set; } = new SoundClassification();
        public List<Node> NodeList { get; set; } = new List<Node>();
    }
}
