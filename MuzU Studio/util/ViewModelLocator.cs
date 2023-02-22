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

    public MediaPlayerViewModel MediaPlayerViewModel => App.Current.Services.GetService<MediaPlayerViewModel>();
    public PianoRollViewModel PianoRollViewModel => App.Current.Services.GetService<PianoRollViewModel>();
    public ProjectPropertiesVM ProjectPropertiesVM => App.Current.Services.GetService<ProjectPropertiesVM>();
    public ProjectViewModel ProjectViewModel => App.Current.Services.GetService<ProjectViewModel>();
    public SequenceViewModel SequenceViewModel => App.Current.Services.GetService<SequenceViewModel>();
    public SequenceListViewModel SequenceListViewModel => App.Current.Services.GetService<SequenceListViewModel>();
    public AudioPlayerViewModel AudioPlayerViewModel => App.Current.Services.GetService<AudioPlayerViewModel>();
}
