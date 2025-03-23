using System.Windows;
using CMM.Test.GUI.Models;
using CMM.Test.GUI.ViewModels;

namespace CMM.Test.GUI.Views
{
    /// <summary>
    /// Interaction logic for SelectFolderView.xaml
    /// </summary>
    public partial class SelectFolderView : Window
    {
        public SelectFolderView(string basePath, SelectedFolderModel model)
        {
            InitializeComponent();

            this.DataContext = new  SelectFolderViewModel(basePath, model);
        }
    }
}
