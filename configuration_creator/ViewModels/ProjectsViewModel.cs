using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using configuration_creator.Contracts.Services;
using configuration_creator.Contracts.ViewModels;
using configuration_creator.Core.Models;
using configuration_creator.Core.Services;

namespace configuration_creator.ViewModels;

public class ProjectsViewModel : ObservableObject, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly ExcelService _excelService;
    private ICommand _navigateToDetailCommand;
    private ICommand _newProjectCommand;

    public ObservableCollection<ProjectModel> Projects { get; } = new();

    public ICommand NavigateToDetailCommand =>
        _navigateToDetailCommand ??= new RelayCommand<ProjectModel>(NavigateToDetail);

    public ICommand NewProjectCommand =>
        _newProjectCommand ??= new RelayCommand(OnNewProject);

    public ProjectsViewModel(ExcelService excelService, INavigationService navigationService)
    {
        _excelService = excelService;
        _navigationService = navigationService;
    }

    public void OnNavigatedTo(object parameter)
    {
        Projects.Clear();

        var path = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Data",
            "Configuration_Articles.xlsm");

        var projects = _excelService.ReadProjects(path);

        foreach (var project in projects)
        {
            Projects.Add(project);
        }
    }

    public void OnNavigatedFrom() { }

    private void NavigateToDetail(ProjectModel project)
    {
        if (project != null)
        {
            _navigationService.NavigateTo(
                typeof(ProjectsDetailViewModel).FullName,
                project.Client);
        }
    }

    private void OnNewProject()
    {
        // Új projekt létrehozási logika
        _navigationService.NavigateTo(typeof(ProjectsDetailViewModel).FullName, null);
    }
}