using System.Windows.Controls;

using configuration_creator.ViewModels;

namespace configuration_creator.Views;

public partial class MidPage : Page
{
    public MidPage(MidViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
