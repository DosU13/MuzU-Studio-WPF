using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MuzU_Studio.view
{
    /// <summary>
    /// Interaction logic for LyricsControl.xaml
    /// </summary>
    public partial class LyricsControl : UserControl
    {
        public LyricsControl()
        {
            InitializeComponent();
        }

        private LyricsMapper? lyricsMapper = null;
        private void LyricsMapper_Click(object sender, RoutedEventArgs e)
        {
            if (lyricsMapper == null)
            {
                lyricsMapper = new LyricsMapper();
                lyricsMapper.Show();
                lyricsMapper.Closed += (_, _) =>
                {
                    lyricsMapper = null;
                };
            }
            else lyricsMapper.Focus();
        }
    }
}
