using MuzU_Studio.model;
using MuzU_Studio.util;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MuzU_Studio.viewmodel;

public class SequenceListViewModel
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

    private ICommand? addNewSequenceCommand;
    public ICommand AddNewSequenceCommand =>
        addNewSequenceCommand ??= new RelayCommand(param => sequenceModel.AddNewSequence());
        
    public void AddSequenceFromMidi(string fileName)
    {
        MessageBox.Show("Not implemented yet");
    }
}
