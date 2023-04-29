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

        private void myListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem selectedItem = (ListBoxItem)myListBox.ItemContainerGenerator.ContainerFromItem(myListBox.SelectedItem);

            if (selectedItem != null)
            {
                double targetHorizontalOffset = selectedItem.TranslatePoint(new Point(0, 0), myListBox).X - myListBox.ActualWidth / 2 + selectedItem.ActualWidth / 2;
                double maxHorizontalOffset = myListBox.Items.Count * ((FrameworkElement)myListBox.ItemContainerGenerator.ContainerFromItem(myListBox.Items[0])).ActualWidth - myListBox.ActualWidth;
                double newHorizontalOffset = targetHorizontalOffset < 0 ? 0 : targetHorizontalOffset > maxHorizontalOffset ? maxHorizontalOffset : targetHorizontalOffset;
                myListBox.ScrollIntoView(myListBox.SelectedItem);
                ((ScrollViewer)VisualTreeHelper.GetChild(myListBox, 0)).ScrollToHorizontalOffset(newHorizontalOffset);
            }
        }
    }
}
