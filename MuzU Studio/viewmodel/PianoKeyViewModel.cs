using MuzU_Studio.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MuzU_Studio.viewmodel
{
    public class PianoKeyViewModel
    {
        public static readonly double PianoKeysWidth = 30;
        public string KeyName { get; set; }
        public SolidColorBrush FillColor { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Y { get; set; }

        public PianoKeyViewModel(int midiKeyNumber)
        {
            KeyName = GetKeyName(midiKeyNumber);
            FillColor = GetFillColor(midiKeyNumber);
            Width = PianoKeysWidth;
            Height = PanAndZoomModel.VER_SCALE;
            Y = midiKeyNumber * PanAndZoomModel.VER_SCALE;
        }

        // Helper methods to calculate key properties based on MIDI key number
        private static string GetKeyName(int midiKeyNumber)
        {
            int octave = (midiKeyNumber / 12) - 1;
            int note = midiKeyNumber % 12;
            string noteName = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" }[note];
            return $"{noteName}{octave}";
        }

        private static SolidColorBrush GetFillColor(int midiKeyNumber)
        {
            bool isBlack = new[] { 1, 3, 6, 8, 10 }.Contains(midiKeyNumber % 12);
            return new SolidColorBrush(isBlack ? Colors.Black : Colors.White);
        }

        private static double GetWidth(int midiKeyNumber)
        {
            bool isBlack = new[] { 1, 3, 6, 8, 10 }.Contains(midiKeyNumber % 12);
            return isBlack ? PianoKeysWidth / 2 : PianoKeysWidth;
        }
    }
}
