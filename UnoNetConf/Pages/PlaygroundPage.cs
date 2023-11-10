using System.Diagnostics;

namespace UnoNetConf.Pages;
partial class PlaygroundPage : Page
{
	static readonly Resource<Geometry> playIcon = 
		StaticResource.Create<Geometry>(nameof(playIcon), Constants.playPath);

	static readonly Resource<Geometry> stopIcon =
		StaticResource.Create<Geometry>(nameof(stopIcon), Constants.stopPath);
	public PlaygroundPage()
	{
		this.DataContext(new PlaygroundViewModel(), (page, vm) =>
		{
			page
			.Padding(58)
			.Resources(r => r.Add(playIcon).Add(stopIcon))
			.Background(Colors.White)
			.Content
			(
				new StackPanel().Children
				(
					new TextBox()
						.Text(x => x.Bind(() => vm.Count).Mode(BindingMode.TwoWay))
						.Foreground(Colors.Black),
					new Button()
					.Content(x =>
						x.Bind(() => vm.Count)
						.Convert(count =>
						{
							if (count % 2 == 0)
							{
								return new PathIcon()
								.Data(StaticResource.Get<Geometry>(nameof(playIcon)));
							}
							return new PathIcon()
								.Data(StaticResource.Get<Geometry>(nameof(stopIcon)));
						})
					)
					.Foreground(Colors.Black)
					.Command(() => vm.IncreaseCountCommand)

				)
				.Background(Colors.AliceBlue)
				.VerticalAlignment(VerticalAlignment.Center)
				.HorizontalAlignment(HorizontalAlignment.Center)
			);
		});
	}
}
