using System.Diagnostics;

namespace UnoNetConf.ViewModels;
partial class PlayerViewModel : ObservableObject
{
	public string Source { get; set; } = "https://avatars.githubusercontent.com/u/20712372?v=4";

	[ObservableProperty]
	float progress;

	CancellationTokenSource cts = new();

	Task progressTask;

	[ObservableProperty]
	bool isPlaying;

	public PlayerViewModel()
	{
		progressTask = PositionChanged(cts.Token);
		isPlaying = true;
	}

	async Task PositionChanged(CancellationToken token)
	{
		while (true)
		{
			if (token.IsCancellationRequested)
			{
				Progress = 0;
				break;
			}

			await Task.Delay(500);
			Progress += 0.001f;
		}
	}

	[RelayCommand]
	void PlayExecute()
	{
		if (progressTask.IsFaulted)
		{
			var exception = progressTask.Exception;
			Debugger.Break();
		}

		if (IsPlaying)
		{
			cts.Cancel();
			cts = new();
		}
		else
		{
			progressTask = PositionChanged(cts.Token);
		}

		IsPlaying = !IsPlaying;
	}

}
