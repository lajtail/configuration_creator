using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using configuration_creator.Contracts.Services;
using configuration_creator.Contracts.ViewModels;
using configuration_creator.Core.Contracts.Services;
using configuration_creator.Core.Models;

namespace configuration_creator.ViewModels;

// Simple model for the tiles
public class ProjectTileModel : ObservableObject
{

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public string Client { get; set; }
    public string CC { get; set; }
    public string ProjectInfo { get; set; }
    // Store a reference to the original Excel row for details
    public Dictionary<string, string> RowCells { get; set; }
}

public class ProjectsViewModel : ObservableObject, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly ISampleDataService _sampleDataService;
    private ICommand _navigateToDetailCommand;

    public ICommand NavigateToDetailCommand => _navigateToDetailCommand ?? (_navigateToDetailCommand = new RelayCommand<ProjectTileModel>(NavigateToDetail));

    public ObservableCollection<ProjectTileModel> Source { get; } = new ObservableCollection<ProjectTileModel>();

    // List of column names for the details panel
    private List<string> _columns = new List<string>();
    public List<string> Columns
    {
        get => _columns;
        set => SetProperty(ref _columns, value);
    }

    // The selected tile (project)
    private ProjectTileModel _selectedTile;
    private static ProjectTileModel _lastSelectedTile;

    public ProjectTileModel SelectedTile
    {
        get => _selectedTile;
        set
        {
            if (SetProperty(ref _selectedTile, value))
            {
                foreach (var tile in Source)
                {
                    tile.IsSelected = tile == value;
                }
                SelectedRowCells = value?.RowCells;
                _lastSelectedTile = value;
                if (value != null)
                {
                    NavigateToDetailCommand.Execute(value);
                }
            }
        }
    }

    // The dictionary of cell values for the selected row
    private Dictionary<string, string> _selectedRowCells;
    public Dictionary<string, string> SelectedRowCells
    {
        get => _selectedRowCells;
        set => SetProperty(ref _selectedRowCells, value);
    }

    public ProjectsViewModel(ISampleDataService sampleDataService, INavigationService navigationService)
    {
        _sampleDataService = sampleDataService;
        _navigationService = navigationService;
    }

    public void OnNavigatedTo(object parameter)
    {
        Source.Clear();
        Columns = new List<string>();

        // LOADING Excel Data
        if (App.ExcelData != null)
        {
            // Get all unique column names from the first row (or all rows if needed)
            var allColumns = new HashSet<string>();
            foreach (var row in App.ExcelData)
            {
                foreach (var col in row.Cells.Keys)
                    allColumns.Add(col);
            }
            Columns = allColumns.ToList();

            foreach (var row in App.ExcelData)
            {
                row.Cells.TryGetValue("Client", out string client);
                row.Cells.TryGetValue("CC", out string cc);
                row.Cells.TryGetValue("OBU\nArtNum1", out string obuartnum);

                if (!string.IsNullOrWhiteSpace(client))
                {
                    Source.Add(new ProjectTileModel
                    {
                        Client = client,
                        CC = cc,
                        ProjectInfo = "8633 " + obuartnum,
                        RowCells = row.Cells // Store the full row for details
                    });
                }
            }

            if (_lastSelectedTile != null)
            {
                var match = Source.FirstOrDefault(t => t.Client == _lastSelectedTile.Client && t.CC == _lastSelectedTile.CC);
                if (match != null)
                {
                    SelectedTile = match;
                }
            }
        }
    }

    public void OnNavigatedFrom() { }

    private void NavigateToDetail(ProjectTileModel tile)
    {
        // You can use this for navigation if needed
    }
}
