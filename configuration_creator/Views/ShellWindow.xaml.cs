using System.Windows.Controls;

using configuration_creator.Contracts.Views;
using configuration_creator.ViewModels;

using MahApps.Metro.Controls;

namespace configuration_creator.Views;

public partial class ShellWindow : MetroWindow, IShellWindow
{
    public ShellWindow(ShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public Frame GetNavigationFrame()
        => shellFrame;

    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();
}
