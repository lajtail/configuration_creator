using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using configuration_creator.Contracts.Services;
using configuration_creator.Contracts.ViewModels;
using configuration_creator.Core.Contracts.Services;
using configuration_creator.Core.Models;

namespace configuration_creator.ViewModels;

// ÚJ: Egy egyszerű modell a csempékhez (ha nincs már ilyen a projektben)
public class ProjectTileModel
{
    public string Client { get; set; }
    public string CC { get; set; }
    public string ProjectInfo { get; set; }
}


public class ProjectsViewModel : ObservableObject, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly ISampleDataService _sampleDataService;
    private ICommand _navigateToDetailCommand;

    public ICommand NavigateToDetailCommand => _navigateToDetailCommand ?? (_navigateToDetailCommand = new RelayCommand<ProjectTileModel>(NavigateToDetail));
    // CHANGE: Using new ProjectTieModel for the tiles
    public ObservableCollection<ProjectTileModel> Source { get; } = new ObservableCollection<ProjectTileModel>();

    // Selected tile property
    private ProjectTileModel _selectedTile;
    public ProjectTileModel SelectedTile
    {
        get => _selectedTile;
        set
        {
            if (SetProperty(ref _selectedTile, value))
            {
                if (value != null)
                {
                    NavigateToDetailCommand.Execute(value);
                }
            }
        }
    }

    public ProjectsViewModel(ISampleDataService sampleDataService, INavigationService navigationService)
    {
        _sampleDataService = sampleDataService;
        _navigationService = navigationService;
    }

    public void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        // LOADING Excel Data
        if (App.ExcelData != null)
        {
            foreach (var row in App.ExcelData)
            {
                // Check if Client, CC, Charon, Charon Revision, OBU ArtNum exists in the row
                row.Cells.TryGetValue("Client", out string client);
                row.Cells.TryGetValue("CC", out string cc);
                row.Cells.TryGetValue("Charon", out string charon);
                row.Cells.TryGetValue("Charon\nRevision", out string revision);
                row.Cells.TryGetValue("OBU\nArtNum1", out string obuartnum);

                // Adding it if Client is not empty
                if (!string.IsNullOrWhiteSpace(client))
                {
                    Source.Add(new ProjectTileModel
                    {
                        Client = client,
                        CC = cc,
                        ProjectInfo = "8633 " + obuartnum + '\n' + "CHR ver: " + charon + " (rev. " + revision + ")"
                    });
                }
            }
        }
    }

    public void OnNavigatedFrom()
    {
    }

    // Modified command, to expect ProjectTileModel
    private void NavigateToDetail(ProjectTileModel tile)
    {
        // Itt átadhatod a szükséges adatokat a részletező oldalnak
        // Például: _navigationService.NavigateTo(typeof(MidDetailViewModel).FullName, tile.Client);
    }
}
