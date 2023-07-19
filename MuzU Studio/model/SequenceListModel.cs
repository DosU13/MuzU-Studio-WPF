﻿using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.util;
using MuzU_Studio.viewmodel;
using MuzUStandard.data;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MuzU_Studio.model;

public class SequenceListModel: BindableBase
{
    private readonly ProjectRepository _projectRepository;
    private readonly PanAndZoomModel _panAndZoomModel;

    private readonly ObservableCollection<SequenceViewModel> sequences = new();
    private readonly ObservableCollection<NoteViewModel> notes = new();

    public SequenceListModel(ProjectRepository projectRepository, PanAndZoomModel panAndZoomModel) {
        this._projectRepository = projectRepository;
        _panAndZoomModel = panAndZoomModel;
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
            _panAndZoomModel.ResetWidth();
            SelectedSequence = Sequences.FirstOrDefault();
            isCollectionChangedEventEnabled = true;
        }));
    }

    internal void AddNewSequence()
    {
        var newSequence = new Sequence() { Name = "New Sequence" };
        Sequences.Add(new SequenceViewModel(newSequence));
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
