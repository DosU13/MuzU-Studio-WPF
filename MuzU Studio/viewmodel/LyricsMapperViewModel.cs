using MuzU_Studio.model;
using MuzU_Studio.util;
using System.Windows.Input;

namespace MuzU_Studio.viewmodel;

public class LyricsMapperViewModel: BindableBase
{
    private SequenceListModel sequenceListModel;

    public LyricsMapperViewModel(SequenceListModel sequenceListViewModel)
    {
        this.sequenceListModel = sequenceListViewModel;
        sequenceListViewModel.PropertyChanged += SequenceListViewModel_PropertyChanged;
    }

    private void SequenceListViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if(e.PropertyName == SequenceListModel.Nameof_SelectedSequence)
        {
            LoadLyrics();
        }
    }

    private SortedSet<NoteViewModel>? Notes => sequenceListModel.SelectedSequence?.Notes;

    public void LoadLyrics()
    {
        Lyrics = Notes != null ? string.Join("$", Notes.Select(x => x.Lyrics )) : "";
        oldLyrics = Lyrics;
    }

    private string oldLyrics;
    private string lyrics = "";
    public string Lyrics
    {
        get => lyrics;
        set => SetProperty(ref lyrics, value);
    }

    private ICommand mapCommand;
    public ICommand MapCommand => mapCommand ??= new RelayCommand(x=>Map());

    public void Map()
    {
        if (Notes == null || oldLyrics == Lyrics) return;
        var newLyrics = Lyrics.Split("$");
        for (int i = 0; i < Notes.Count && i < newLyrics.Length; i++)
        {
            Notes.ElementAt(i).Lyrics = newLyrics[i];
        }
        for (int i = newLyrics.Length; i < Notes.Count; i++)
        {
            Notes.ElementAt(i).Lyrics = "";
        }
    }

    public string SplitByWords(string text)
    {
        for (int i = 0; i < text.Length - 1; i++)
        {
            if (char.IsWhiteSpace(text[i]) && !char.IsWhiteSpace(text[i + 1]))
            {
                text = text.Insert(i + 1, "$");
            }
        }
        return text;
    }
}
