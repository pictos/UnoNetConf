using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnoNetConf.ViewModels;

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
					.Grid(row: 0, column: 0)
					.Foreground(Colors.Black)
					.Margin(5)
					.Text(x => x.Bind(() => vm.Query).Mode(BindingMode.TwoWay))
					.Assign(out var txtBlock),
				new ListView()
					.ItemsSource(() => vm.Results)
					.Grid(row: 1, column: 0)
					.Visibility(builder => SetVisibility(builder, vm))
					.ItemTemplate<SearchResult>
					(result =>

					new Grid().Children
						(
							new Image().Source(() => result.AlbumUrl).Grid(row: 0, rowSpan: 2)
								.Stretch(Stretch.Uniform),
							new StackPanel()
								.Padding(5)
								.Background("#99000000")
								.Children
								(
									new TextBlock()
										.Text(() => result.Title)
										.Foreground(Colors.White)
										.VerticalAlignment(VerticalAlignment.Center)
										.TextWrapping(TextWrapping.WrapWholeWords),
									new TextBlock().Foreground(Colors.White).Text(() => result.Duration)
								).Grid(1, 0)
						).RowDefinitions("* , auto")
						.RowSpacing(16)
					),
				new Button()
					.Content("Search")
					.Grid(row: 2, column: 0)
					.MinWidth(100)
					.HorizontalAlignment(HorizontalAlignment.Center)
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
}
