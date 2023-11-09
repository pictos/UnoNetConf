namespace UnoNetConf.Pages;
sealed partial class ComicsPage : Page
{
	public ComicsPage()
	{
		this.DataContext(new ComicsViewModel(), (page, vm) =>
		{
			page
#if ANDROID
			.Padding(28)
#else
			.Padding(58)
#endif
			.Background(ThemeResource.Get<Brush>("ApplicationPageBackgroundThemeBrush"))
			.Content
			(
				new Grid()
					.RowDefinitions("*,Auto")
					.RowSpacing(16)
					.Children
					(
						new Image()
							.Source(() => vm.ImageUrl)
							.HorizontalAlignment(HorizontalAlignment.Center)
							.VerticalAlignment(VerticalAlignment.Center)
							.Stretch(Stretch.Uniform),
						new Button()
							.Content("NEXT")
							.HorizontalAlignment(HorizontalAlignment.Center)
							.VerticalAlignment(VerticalAlignment.Center)
							.Command(() => vm.ChangeExecuteCommand)
							.Grid(row: 1)
							.Width(600)
							.Margin(10)
					)
			);
		});
	}
}
