using configuration_creator.Core.Models;

namespace configuration_creator.Core.Contracts.Services;

public interface ISampleDataService
{
    Task<IEnumerable<SampleOrder>> GetContentGridDataAsync();

    Task<IEnumerable<SampleOrder>> GetListDetailsDataAsync();

    Task<IEnumerable<SampleOrder>> GetGridDataAsync();
}
