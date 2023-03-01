using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzU_Studio.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.util
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
            // Update any existing singleton instances with the new service provider
            foreach (var singleton in _singletons)
            {
                if (singleton.Value is AudioService usesServiceProvider)
                {
                    //usesServiceProvider.ServiceProvider = serviceProvider;
                    _services.AddSingleton(usesServiceProvider);
                }
            }
            _services.AddSingleton(projectRepository);
            // Models
            _services.AddSingleton<PianoRollModel>();
            _services.AddSingleton<SequenceListModel>();
            // ViewModels
            _services.AddTransient<MediaPlayerViewModel>();
            _services.AddTransient<PianoRollViewModel>();
            _services.AddTransient<ProjectPropertiesVM>();
            _services.AddTransient<ProjectViewModel>();
            _services.AddTransient<SequenceViewModel>();
            _services.AddTransient<SequenceListViewModel>();
            _services.AddTransient<AudioPlayerViewModel>();

            // Build the service provider
            Services = _services.BuildServiceProvider();

            // Notify subscribed views to update their viewmodels with the new service provider
            ServiceUpdated?.Invoke(Services);
        }

        public void RegisterSingleton<T>(T instance)
        {
            _singletons[typeof(T)] = instance;
        }

        public T GetSingleton<T>()
        {
            return (T)_singletons[typeof(T)];
        }

        public event Action<IServiceProvider>? ServiceUpdated;
    }
}
