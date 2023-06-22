using MuzU_Studio.model;
using MuzU_Studio.util;

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
                OnPropertyChanged(nameof(LyricsItems));
        }

        public SortedSet<NoteViewModel> LyricsItems => sequenceListModel?.SelectedSequence?.Notes??new();
    }
}
