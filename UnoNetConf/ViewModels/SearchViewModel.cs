using System.Collections.ObjectModel;
using UnoNetConf.Services;

namespace UnoNetConf.ViewModels;

partial class SearchViewModel : ObservableObject
{
	[ObservableProperty]
	string query = string.Empty;

	public ObservableCollection<SearchResult> Results { get; } = new();

	static AllServices MediaService => AllServices.Current;

	[RelayCommand]
	async Task SearchExecute()
	{
		if (string.IsNullOrWhiteSpace(Query))
		{
			return;
		}

		Results.Clear();

		await foreach (var item in MediaService.SearchMediaAsync(Query))
		{
			Results.Add(item);
		}
	}
}
