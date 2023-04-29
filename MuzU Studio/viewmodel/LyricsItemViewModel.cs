using MuzU_Studio.model;
using MuzUHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.viewmodel
{
    public class LyricsItemViewModel: BindableBase
    {
        public LyricsItemViewModel(string lyrics)
        {
            this.lyrics = lyrics;
        }

        private string lyrics;
        public string Lyrics { get => lyrics; set => SetProperty(ref lyrics, value); }

        private bool isSelected = false;
        public bool IsSelected { get => isSelected;
            set {
                if (SetProperty(ref isSelected, value)) OnPropertyChanged(nameof(FontSize));
            } }

        public double FontSize => IsSelected ? 25 : 18;
    }
}
