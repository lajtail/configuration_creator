using System.Windows.Controls;

using configuration_creator.ViewModels;

namespace configuration_creator.Views;

public partial class ConfPage : Page
{
    public ConfPage(ConfViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
