using MuzUStandard.data.util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MuzUStandard.data
{
    public class Tempo
    {
        public Tempo() {
            MicrosecondsPerQuarterNote = 500_000;
            TimeSignature = new TimeSignature();
        }

        public long MicrosecondsPerQuarterNote;
        public TimeSignature TimeSignature;
        public double BPM {
            get => Utils.GetBPM(MicrosecondsPerQuarterNote, TimeSignature);
            set => MicrosecondsPerQuarterNote = Utils.GetMicrosecondsPerQuarterNote(value, TimeSignature);
        }
    }

    public class TimeSignature
    {
        internal TimeSignature() { }

        public int Numerator = 4;
        public int Denominator = 4;
    }
}
