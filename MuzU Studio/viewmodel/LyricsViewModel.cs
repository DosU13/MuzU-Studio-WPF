using MuzU_Studio.model;
using MuzUHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.viewmodel
{
    public class LyricsViewModel: BindableBase
    {
        private readonly SequenceListModel sequenceListModel;

        public LyricsViewModel(SequenceListModel sequenceListModel)
        {
            this.sequenceListModel = sequenceListModel;
            sequenceListModel.PropertyChanged += SequenceListModel_PropertyChanged;
        }

        private void SequenceListModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == SequenceListModel.Nameof_SelectedSequence) 
                OnPropertyChanged(nameof(Lyrics));
        }

        public SortedSet<NoteViewModel> Lyrics => sequenceListModel?.SelectedSequence?.Notes??new();
    }
}
