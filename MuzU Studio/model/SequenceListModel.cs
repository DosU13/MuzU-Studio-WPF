using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.util;
using MuzU_Studio.viewmodel;
using MuzUStandard.data;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MuzU_Studio.model;

public class SequenceListModel: BindableBase
{
    private readonly ProjectRepository _projectRepository;

    private readonly ObservableCollection<SequenceViewModel> sequences = new();
    private readonly ObservableCollection<NoteViewModel> notes = new();

    public SequenceListModel(ProjectRepository projectRepository) {
        this._projectRepository = projectRepository;
        sequences.CollectionChanged += Sequences_CollectionChanged;
        notes.CollectionChanged += Notes_CollectionChanged;
        ReinitCollections();
    }

    private bool isCollectionChangedEventEnabled = true;
    private void Sequences_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (!isCollectionChangedEventEnabled) return;
        var data = _projectRepository.ProjectModel.MuzUProject.MuzUData.SequenceList;
        if (e.NewItems != null)
        {
            foreach (SequenceViewModel sequence in e.NewItems)
            {
                data.Add(sequence.Data);
            }
        }
        if(e.OldItems != null)
        {
            foreach (SequenceViewModel sequence in e.OldItems)
            {
                data.Remove(sequence.Data);
            }
        }
    }

    private void Notes_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (!isCollectionChangedEventEnabled) return;
        if (e.NewItems != null)
        {
            foreach (NoteViewModel note in e.NewItems)
            {
                ((SequenceViewModel)note.Parent).AddNote(note);
            }
        }
        if (e.OldItems != null)
        {
            foreach (NoteViewModel note in e.OldItems)
            {
                ((SequenceViewModel)note.Parent).RemoveNote(note);
            }
        }
    }

    public void ReinitCollections()
    {
        App.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
            isCollectionChangedEventEnabled = false;
            sequences.Clear();
            notes.Clear();
            if (!_projectRepository.ProjectExists) return;
            MuzUData muzUData = _projectRepository.ProjectModel.MuzUProject.MuzUData;
            foreach (var sequenceData in muzUData.SequenceList)
            {
                SequenceViewModel sequenceViewModel = new(sequenceData);
                sequences.Add(sequenceViewModel);
                foreach (NoteViewModel note in sequenceViewModel.Notes)
                    notes.Add(note);
            }
            var panAndZoomModel = App.Current.Services.GetRequiredService<PanAndZoomModel>();
            panAndZoomModel.ResetWidth();
            SelectedSequence = Sequences.FirstOrDefault();
            isCollectionChangedEventEnabled = true;
        }));
    }

    internal void AddNewSequence()
    {
        var newSequence = new Sequence() { Name = "New Sequence" };
        Sequences.Add(new SequenceViewModel(newSequence));
    }

    internal void AddNewSequenceEveryBeat()
    {
        var newSequence = new Sequence() { Name = "Every Beat" };
        var pianoRollModel = App.Current.Services.GetRequiredService<PianoRollModel>();
        var panAndZoomModel = App.Current.Services.GetRequiredService<PanAndZoomModel>();
        var beatLengthPixels = pianoRollModel.BeatLength;
        var beatLengthMicrosecs = PanAndZoomModel.ToMicroseconds(beatLengthPixels);
        for (int i = 0; i < panAndZoomModel.ContentViewportWidth / beatLengthPixels; i++)
        {
            newSequence.NodeList.Add(new Node()
            {
                Time = PanAndZoomModel.ToMicroseconds(i * beatLengthPixels),
                Length = beatLengthMicrosecs,
                Note = 64
            });
        }
        Sequences.Add(new SequenceViewModel(newSequence));
        ReinitCollections();
    }

    /// <summary>
    /// The list of rectangles that is displayed both in the main window and in the overview window.
    /// </summary>
    public ObservableCollection<SequenceViewModel> Sequences => sequences;

    public static string Nameof_SelectedSequence => nameof(SelectedSequence);

    private SequenceViewModel? selectedSequence;
    public SequenceViewModel? SelectedSequence
    {
        get => selectedSequence ??= Sequences.FirstOrDefault();
        set => SetProperty(ref selectedSequence, value);
    }
    public ObservableCollection<NoteViewModel> Notes => notes;
}
