using System.Windows.Controls;

using configuration_creator.ViewModels;

namespace configuration_creator.Views;

public partial class GeofencePage : Page
{
    public GeofencePage(GeofenceViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
