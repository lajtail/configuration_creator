using System.Windows.Controls;

using configuration_creator.ViewModels;

namespace configuration_creator.Views;

public partial class PersPage : Page
{
    public PersPage(PersViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
