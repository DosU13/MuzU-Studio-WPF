using MuzU_Studio.viewmodel;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MuzU_Studio.view
{
    /// <summary>
    /// Interaction logic for LyricsMapper.xaml
    /// </summary>
    public partial class LyricsMapper : Window
    {
        public LyricsMapper()
        {
            InitializeComponent();
            DataObject.AddPastingHandler(LyricsBox, OnPaste);
        }

        private LyricsMapperViewModel LyricsMapperViewModel => (LyricsMapperViewModel)DataContext;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LyricsMapperViewModel.LoadLyrics();
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string? pastedText = e.DataObject.GetData(DataFormats.Text) as string;

                if (string.IsNullOrEmpty(pastedText)) return;
                pastedText = LyricsMapperViewModel.SplitByWords(pastedText);

                int selectionStart = LyricsBox.SelectionStart;
                int selectionLength = LyricsBox.SelectionLength;
                string currentText = LyricsBox.Text;

                string modifiedText = 
                    string.Concat(currentText.AsSpan(0, selectionStart), 
                                  pastedText, 
                                  currentText.AsSpan(selectionStart + selectionLength));

                LyricsBox.Text = modifiedText;

                LyricsBox.SelectionStart = selectionStart + pastedText.Length;
                LyricsBox.SelectionLength = 0;

                e.CancelCommand();
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            LyricsMapperViewModel.Map();
            Close();
        }
    }
}
