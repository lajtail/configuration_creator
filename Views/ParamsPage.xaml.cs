using System.Windows.Controls;

using configuration_creator.ViewModels;

namespace configuration_creator.Views;

public partial class ParamsPage : Page
{
    public ParamsPage(ParamsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
