using System.Windows.Controls;

using configuration_creator.ViewModels;

namespace configuration_creator.Views;

public partial class PhrasesPage : Page
{
    public PhrasesPage(PhrasesViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
