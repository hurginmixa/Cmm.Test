using System.Windows;
using System.Windows.Controls;

namespace CMM.Test.GUI.Views
{
    /// <summary>
    /// Interaction logic for CreatingMapControl.xaml
    /// </summary>
    public partial class CreatingMapControl : UserControl
    {
        public CreatingMapControl()
        {
            InitializeComponent();
        }

        private void ConverterNameList_OnLoaded(object sender, RoutedEventArgs e)
        {
            var listBox = sender as ListBox;
            listBox?.ScrollIntoView(listBox.SelectedItem);
        }
    }
}
