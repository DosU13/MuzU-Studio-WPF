using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.util;

internal class ViewModelLocator
{
    static ViewModelLocator() { }

    public static MediaPlayerViewModel MediaPlayerViewModel => App.Current.Services.GetService<MediaPlayerViewModel>()!;
    public static PianoRollViewModel PianoRollViewModel => App.Current.Services.GetService<PianoRollViewModel>()!;
    public static ProjectPropertiesVM ProjectPropertiesVM => App.Current.Services.GetService<ProjectPropertiesVM>()!;
    public static ProjectViewModel ProjectViewModel => App.Current.Services.GetService<ProjectViewModel>()!;
    public static SequenceViewModel SequenceViewModel => App.Current.Services.GetService<SequenceViewModel>()!;
    public static SequenceListViewModel SequenceListViewModel => App.Current.Services.GetService<SequenceListViewModel>()!;
    public static MediaControlsViewModel MediaControlsViewModel => App.Current.Services.GetService<MediaControlsViewModel>()!;
    public static ToolboxViewModel ToolboxViewModel => App.Current.Services.GetService<ToolboxViewModel>()!;
    public static LyricsViewModel LyricsViewModel => App.Current.Services.GetService<LyricsViewModel>()!;
}
