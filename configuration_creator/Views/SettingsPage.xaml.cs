﻿using System.Windows.Controls;

using configuration_creator.ViewModels;

namespace configuration_creator.Views;

public partial class SettingsPage : Page
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
