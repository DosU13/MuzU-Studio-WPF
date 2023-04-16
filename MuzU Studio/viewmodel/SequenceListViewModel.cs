using MuzU_Studio.model;
using System.Collections.ObjectModel;

namespace MuzU_Studio.viewmodel;

internal class SequenceListViewModel
{
    private readonly SequenceListModel sequenceModel;

    public SequenceListViewModel(SequenceListModel sequenceModel)
    {
        this.sequenceModel = sequenceModel;
    }

    public ObservableCollection<SequenceViewModel> Sequences => sequenceModel.Sequences;
    public SequenceViewModel? SelectedSequence
    {
        get => sequenceModel.SelectedSequence;
        set => sequenceModel.SelectedSequence = value;
    }
}
