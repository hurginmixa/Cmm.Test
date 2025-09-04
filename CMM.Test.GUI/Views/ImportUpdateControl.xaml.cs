using System.Windows;
using System.Windows.Controls;
using CMM.Test.GUI.ViewModels;

namespace CMM.Test.GUI.Views
{
    /// <summary>
    /// Interaction logic for ImportUpdateControl.xaml
    /// </summary>
    public partial class ImportUpdateControl : UserControl
    {
        public ImportUpdateControl()
        {
            InitializeComponent();
        }

        private void ConverterList_OnLoaded(object sender, RoutedEventArgs e)
        {
            var listBox = sender as ListBox;
            listBox?.ScrollIntoView(listBox.SelectedItem);
        }
    }
}
