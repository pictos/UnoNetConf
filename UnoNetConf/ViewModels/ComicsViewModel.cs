namespace UnoNetConf.ViewModels;
sealed partial class ComicsViewModel : ObservableObject
{
	const string comicUrl = "https://imgs.xkcd.com/comics/standards_2x.png";

	const string twitterImg = "ms-appx:///UnoNetConf/Assets/Images/twitter_img.jpg";

	[ObservableProperty]
	string imageUrl = comicUrl; 

	int index = 0;

	[RelayCommand]
	public void ChangeExecute()
	{
		ImageUrl = ++index switch
		{
			1 => twitterImg,
			_ => Reset()
		};
	}

	string Reset()
	{
		index = 0;
		return comicUrl;
	}
}
