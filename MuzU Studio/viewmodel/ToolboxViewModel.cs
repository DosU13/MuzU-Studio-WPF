using Microsoft.Extensions.DependencyInjection;
using MuzU_Studio.model;
using MuzUHub;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MuzU_Studio.viewmodel
{
    internal class ToolboxViewModel : BindableBase
    {
        private SequenceListModel sequenceListModel;

        public ToolboxViewModel(SequenceListModel sequenceListModel)
        {
            this.sequenceListModel = sequenceListModel;
        }

        private double snapAllInterval = 0.0;
        public double SnapAllInterval
        {
            get => snapAllInterval;
            set => snapAllInterval = value;
        }

        private ICommand? snapAllCommand;
        public ICommand SnapAllCommand
        {
            get
            {
                snapAllCommand ??= new RelayCommand(param => SnapAll());
                return snapAllCommand;
            }
        }

        public void SnapAll() {
            int count = 0;
            foreach (var note in sequenceListModel.Notes)
            {
                if (note.Parent.Visible)
                {
                    var newX = App.Current.Services.GetService<PianoRollModel>()!
                                          .SnapToGrid(note.X, SnapAllInterval);
                    var oldTime = note.Node.Time;
                    note.ForceSetX(newX);
                    var newTime = note.Node.Time;
                    if (oldTime != newTime) count++;
                }
            }
            MessageBox.Show($"{count} notes snapped");
        }

        private MelodizeType choosenType;
        public MelodizeType ChoosenType
        {
            get => choosenType; 
            set => SetProperty(ref choosenType, value);
        }

        public List<string> MelodizeTypes = new() {
            "Highest",
            "Lowest",
            "Loudest",
            "Loudest then highest",
            "Loudest then lowest",
        };
         
        public string[] MelodizeTypes = Enum.GetNames<MelodizeType>();
        public enum MelodizeType
        {
            [Description("Highest")]
            HIGHEST,
            ["Lowest"]
            LOWEST,
            LOUDEST,
            LOUDEST_THEN_HIGHEST,
            LOUDEST_THEN_LOWEST,
        }
    }
}
