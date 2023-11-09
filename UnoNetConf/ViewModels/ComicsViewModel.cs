using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoNetConf.ViewModels;
sealed partial class ComicsViewModel : ObservableObject
{
	const string comicUrl = "https://imgs.xkcd.com/comics/standards_2x.png";

	[ObservableProperty]
	string imageUrl = comicUrl;

	int index = 0;

	[RelayCommand]
	public void ChangeExecute()
	{
		ImageUrl = ++index switch
		{
			_ => Reset()
		};
	}

	string Reset()
	{
		index = 0;
		return comicUrl;
	}
}
