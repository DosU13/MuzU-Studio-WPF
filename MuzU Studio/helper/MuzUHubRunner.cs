using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MuzU_Studio.helper
{
    internal class MuzUHubRunner
    {
        internal static void Run()
        {
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string searchPattern = "MuzU Studio WPF\\";
            int index = appPath.LastIndexOf(searchPattern);
            if (index < 0)
            {
                MessageBox.Show("Path is not find");
                return;
            }
            appPath = appPath[..(index + searchPattern.Length)];
            appPath += "MuzUHub\\bin\\Debug\\net7.0-windows\\MuzUHub.exe";
            Process.Start(appPath);
            //string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //appPath = appPath[..(appPath.LastIndexOf('\\') + 1)] + "MuzUHub.exe";
            //Process.Start(appPath);
        }
    }
}
