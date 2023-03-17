using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.viewmodel;
using MuzUStandard.data;
using System.Collections.ObjectModel;

namespace MuzU_Studio.model;

internal class SequenceListModel
{
    /// <summary>
    /// The list of rectangles that is displayed both in the main window and in the overview window.
    /// </summary>
    private readonly ObservableCollection<SequenceViewModel> sequences = new();
    private readonly ObservableCollection<NoteViewModel> notes = new();

    public SequenceListModel(ProjectRepository projectRepository) {
        Update(projectRepository);
    }

    public void Update(ProjectRepository projectRepository)
    {
        App.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
            sequences.Clear();
            notes.Clear();
            if (!projectRepository.ProjectExists) return;
            MuzUData muzUData = projectRepository.ProjectModel.MuzUProject.MuzUData;
            foreach (var sequenceData in muzUData.SequenceList.List)
            {
                SequenceViewModel sequenceViewModel = new(sequenceData);
                sequences.Add(sequenceViewModel);
                foreach (var note in sequenceData.NodeList.List)
                    notes.Add(new NoteViewModel(note, sequenceViewModel));
            }

            double max = 0;
            foreach (var n in notes) if (max < n.Width + n.X) max = n.Width + n.X;
            App.Current.Services.GetService<PanAndZoomModel>()!.ContentWidth = max;
        }));
    }

    /// <summary>
    /// The list of rectangles that is displayed both in the main window and in the overview window.
    /// </summary>
    public ObservableCollection<SequenceViewModel> Sequences => sequences;

    public ObservableCollection<NoteViewModel> Notes => notes;
}
