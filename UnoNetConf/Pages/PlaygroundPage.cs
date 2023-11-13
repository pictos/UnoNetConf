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
			.Background(ThemeResource.Get<Brush>("ApplicationPageBackgroundThemeBrush"))
			.Content
			(
				new Grid()
				.RowDefinitions("auto, auto")
				.Children
				(
					new TextBox()
					.Text(x => x.Bind(() => vm.Count).Mode(BindingMode.TwoWay))
					.FontSize(30),
					new Button().Content
					(
						x => x.Bind(() => vm.Count)
						.Convert(count => new PathIcon()
							.Data(count % 2 is 0 ? playIcon : stopIcon))
					)
					.Grid(row: 1)
					.Command(() => vm.IncreaseCountCommand)
				)
				.VerticalAlignment(VerticalAlignment.Center)
				.HorizontalAlignment(HorizontalAlignment.Center)
			);
		});
	}
}
