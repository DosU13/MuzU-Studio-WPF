using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MuzUStandard.data
{
    public class SequenceTemplate 
    {
        public SequenceTemplate() { }

        public bool LengthEnabled { get; set; } = false;
        public bool NoteEnabled { get; set; } = false;
        public bool LyricsEnabled { get; set; } = false;
    }
}
