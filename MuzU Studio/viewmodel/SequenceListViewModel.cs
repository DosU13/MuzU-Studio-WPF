using MuzU_Studio.model;

namespace MuzU_Studio.viewmodel;

internal class SequenceListViewModel
{
    private SequenceListModel sequenceModel;

    public SequenceListViewModel(SequenceListModel sequenceModel)
    {
        this.sequenceModel = sequenceModel;
    }

    public SequenceListModel SequenceModel
    {
        get { return sequenceModel; }
        set { sequenceModel = value; }
    }

}
