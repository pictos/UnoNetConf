using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
