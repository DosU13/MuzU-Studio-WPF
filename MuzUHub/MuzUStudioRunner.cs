using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MuzUHub
{
    internal class MuzUStudioRunner
    {
        public static void Run(MuzUStudio_ArgType argType, string url = "")
        {
            Application.Current.Shutdown();
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string searchPattern = "MuzU Studio WPF\\";
            int index = appPath.LastIndexOf(searchPattern);
            if (index < 0)
            {
                MessageBox.Show("Path is not find");
                return;
            }
            appPath = appPath[..(index + searchPattern.Length)];
            appPath += "MuzU Studio\\bin\\Debug\\net7.0-windows\\MuzU Studio.exe";
            var args = new string[] { argType.ToString(), url };
            Process.Start(appPath, args);
        }
    }

    public enum MuzUStudio_ArgType
    {
        MuzU_FILE,
        MIDI_FILE,
        NEW_PROJECT
    }
}
