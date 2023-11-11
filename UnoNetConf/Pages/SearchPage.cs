namespace UnoNetConf.Pages;
partial class SearchPage : Page
{
	public SearchPage()
	{
		this.DataContext(new SearchViewModel(), (page, vm) =>
		{
			page.Background(ThemeResource.Get<Brush>("ApplicationPageBackgroundThemeBrush"))
				.Padding(0, 58, 0, 20)
				.Content(MainContent(vm));
		});
	}

	Grid MainContent(SearchViewModel vm)
	{
		new Grid()
			.Assign(out var grid)
			.RowDefinitions("auto, *, auto")
			.Children
			(
				new ProgressRing()
					.Width(100)
					.Height(100)
					.HorizontalAlignment(HorizontalAlignment.Center)
					.VerticalAlignment(VerticalAlignment.Center)
					.IsActive(() => vm.SearchExecuteCommand.IsRunning)
					.Grid(rowSpan: 3),
				new TextBox()
					.PlaceholderText("Search...")
					.Grid(row: PageRows.Search, column: 0)
					.Foreground(Colors.White)
					.Margin(5)
					.Text(x => x.Bind(() => vm.Query).Mode(BindingMode.TwoWay))
					.Assign(out var txtBlock),
				new ListView()
					.ItemsSource(() => vm.Results)
					.Grid(row: PageRows.ListView, column: 0)
					.MyCustomVisibility(() => vm.SearchExecuteCommand.IsRunning)
					.Visibility(builder => SetVisibility(builder, vm))
					.ItemTemplate<SearchResult>(result => new MediaCell()),
				new Button()
					.Content("Search")
					.Grid(row: PageRows.Button, column: 0)
					.MinWidth(100)
					.HorizontalAlignment(HorizontalAlignment.Stretch)
					.Assign(out var btn)
					.Command(() => vm.SearchExecuteCommand)
					.Margin(10)
			);

		return grid;
	}


	static void SetVisibility(IDependencyPropertyBuilder<Visibility> builder, SearchViewModel vm)
	{
		builder
		.Bind(() => vm.SearchExecuteCommand.IsRunning)
		.Convert(x => x ? Visibility.Collapsed : Visibility.Visible);
	}

	enum PageRows
	{
		Search = 0,
		ListView = 1,
		Button = 2
	}

}
