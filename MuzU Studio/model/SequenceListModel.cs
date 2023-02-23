using MuzU_Studio.viewmodel;
using MuzUStandard.data;
using System.Collections.ObjectModel;

namespace MuzU_Studio.model;

internal class SequenceListModel
{
    /// <summary>
    /// The list of rectangles that is displayed both in the main window and in the overview window.
    /// </summary>
    private ObservableCollection<SequenceViewModel> sequences = new ObservableCollection<SequenceViewModel>();

    public SequenceListModel(ProjectRepository projectModel) {
        Update(projectModel);
    }

    public void Update(ProjectRepository projectModel)
    {
        App.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
            sequences.Clear();
            MuzUData muzUData = projectModel.MuzUProject?.MuzUData;
            if (muzUData == null) return;
            foreach (var sequenceData in muzUData.SequenceList.List)
            {
                sequences.Add(new SequenceViewModel(sequenceData));
            }
        }));
    }

    /// <summary>
    /// The list of rectangles that is displayed both in the main window and in the overview window.
    /// </summary>
    public ObservableCollection<SequenceViewModel> Sequences => sequences;
}
