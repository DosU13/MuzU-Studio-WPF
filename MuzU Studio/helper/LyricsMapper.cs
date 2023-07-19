using MuzU_Studio.viewmodel;
using MuzUStandard.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.helper
{
    internal class LyricsMapper
    {
        public static string Map(SequenceViewModel sequenceVM)
        {
            return string.Join('$', sequenceVM.Notes.Select(n => n.Lyrics));
        }

        public static void MapInto(string from, SequenceViewModel into)
        {
            var lyrics = from.Split('$');
            for(int i = 0; i < lyrics.Length && i < into.Notes.Count; i++)
            {
                into.Notes.ElementAt(i).Lyrics = lyrics[i];
            }
        }

        public static string DivideByWords(string text)
        {
            StringBuilder result = new StringBuilder();
            for(int i = 0; i < text.Length-1;i++)
            {
                result.Append(text[i]);
                if (text[i] != '$' && !char.IsLetter(text[i]) && char.IsLetter(text[i+1]))
                {
                    result.Append('$');
                }
            }
            return result.ToString();
        }
    }
}
