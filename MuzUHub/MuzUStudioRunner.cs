﻿using System;
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
            appPath = appPath[..(appPath.LastIndexOf('\\') + 1)] + "MuzU Studio.exe";
            Process.Start(appPath, new string[] {argType.ToString(), url});
        }
    }

    public enum MuzUStudio_ArgType
    {
        MuzU_FILE,
        MIDI_FILE,
        NEW_PROJECT
    }
}
