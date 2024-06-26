﻿using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.service;
using MuzU_Studio.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.helper
{
    public class ServiceManager
    {
        private readonly Dictionary<Type, object> _singletons = new Dictionary<Type, object>();

        public ServiceManager()
        {
            RegisterSingleton(new AudioService());
            ConfigureServices(ProjectRepository.InitEmpty());
        }

        public IServiceProvider Services { get; set; }

        public void ConfigureServices(ProjectRepository projectRepository)
        {
            IServiceCollection _services = new ServiceCollection();
            foreach (var singleton in _singletons)
            {
                if (singleton.Value is AudioService serv)
                {
                    _services.AddSingleton(serv);
                    serv.Update(projectRepository);
                }
            }
            _services.AddSingleton(projectRepository);
            // Models
            _services.AddSingleton<PianoRollModel>();
            _services.AddSingleton<SequenceListModel>();
            _services.AddSingleton<PanAndZoomModel>();
            // ViewModels
            _services.AddTransient<MediaPlayerViewModel>();
            _services.AddTransient<PianoRollViewModel>();
            _services.AddTransient<ProjectPropertiesVM>();
            _services.AddTransient<ProjectViewModel>();
            _services.AddTransient<SequenceViewModel>();
            _services.AddTransient<SequenceListViewModel>();
            _services.AddTransient<MediaControlsViewModel>();
            _services.AddTransient<ToolboxViewModel>();
            _services.AddTransient<LyricsViewModel>();
            _services.AddTransient<LyricsMapperViewModel>();

            Services = _services.BuildServiceProvider();

            ServiceUpdated?.Invoke(Services);
        }

        public void RegisterSingleton<T>(T instance)
        {
            if (instance == null) throw new NullReferenceException();
            _singletons[typeof(T)] = instance;
        }

        public T GetSingleton<T>()
        {
            return (T)_singletons[typeof(T)];
        }

        public event Action<IServiceProvider>? ServiceUpdated;
    }
}
