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

    public SequenceListModel(ProjectRepository projectRepository) {
        Update(projectRepository);
    }

    public void Update(ProjectRepository projectRepository)
    {
        App.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
            sequences.Clear();
            if (!projectRepository.ProjectExists) return;
            MuzUData muzUData = projectRepository.ProjectModel.MuzUProject.MuzUData;
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

    public ObservableCollection<NoteViewModel> FirstSequence => Sequences.FirstOrDefault()?.Notes;
}
