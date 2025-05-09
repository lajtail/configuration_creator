using System.Windows.Controls;

using configuration_creator.ViewModels;

namespace configuration_creator.Views;

public partial class ProjectsPage : Page
{
    public ProjectsPage(ProjectsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
