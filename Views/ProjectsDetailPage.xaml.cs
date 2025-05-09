using System.Windows.Controls;

using configuration_creator.ViewModels;

namespace configuration_creator.Views;

public partial class ProjectsDetailPage : Page
{
    public ProjectsDetailPage(ProjectsDetailViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
