using System;

namespace MuzUStandard.data.util
{
    internal class Utils
    {
        internal static int GetTimeSignatureNumerator(string str)
        {
            if (!int.TryParse(str.Split('/')[0], out int r)) return -1;
            return r;
        }

        internal static int GetTimeSignatureDenominator(string str)
        {
            if (!int.TryParse(str.Split('/')[1], out int r)) return -1;
            return r;
        }

        internal static double GetBPM(long microsecondsPerQuarterNote, TimeSignature timeSignature)
        {
            double microsecondsPerBeat = microsecondsPerQuarterNote * 4.0 / timeSignature.Denominator;
            double minutesePerBeat = microsecondsPerBeat / 60000000;
            return 1.0 / minutesePerBeat;
        }

        internal static long GetMicrosecondsPerQuarterNote(double bPM, TimeSignature timeSignature)
        {
            double minutesPerBeat = 1 / bPM;
            double microsecondsPerBeat = minutesPerBeat * 60000000;
            return (long)(microsecondsPerBeat * timeSignature.Denominator / 4.0);
        }
    }
}
