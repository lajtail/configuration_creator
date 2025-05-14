using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using configuration_creator.ViewModels;
using configuration_creator.Core.Models;

namespace configuration_creator.Views
{
    public partial class ProjectsPage : Page
    {
        public ProjectsPage()
        {
            InitializeComponent();
        }

        private void ProjectTile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ProjectsViewModel vm &&
                ((FrameworkElement)sender).DataContext is ProjectModel project)
            {
                vm.NavigateToDetailCommand.Execute(project);
            }
        }
    }
}
