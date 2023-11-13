namespace UnoNetConf.ViewModels;
partial class PlaygroundViewModel : ObservableObject
{
	[ObservableProperty]
	int count;


	[RelayCommand]
	void IncreaseCount()
	{
		Count++;
	}
}
