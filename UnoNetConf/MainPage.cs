using System.Collections.ObjectModel;
using UnoNetConf.Pages;
using static UnoNetConf.AppPages;

namespace UnoNetConf;

public sealed partial class MainPage : Page
{
	static readonly ReadOnlyDictionary<AppPages, Page> pages = new Dictionary<AppPages, Page>()
	{
		{ Playground, new PlaygroundPage() },
		{ Player, new PlayerPage() },
		{ XamlPage, new SearchPageXAML() }

	}.AsReadOnly();
	readonly Frame contentFrame;
	public MainPage()
	{
		new NavigationView()
			.Assign(out var navigationView)
			.IsSettingsVisible(false)
			.IsBackButtonVisible(NavigationViewBackButtonVisible.Collapsed)
			.PaneDisplayMode(NavigationViewPaneDisplayMode.LeftMinimal)
			.MenuItems
			(
			                   // These values came from AppPages enum
				new NavigationViewItem().Content(Playground),
				new NavigationViewItem().Content(XamlPage),
				new NavigationViewItem().Content(Player)
			)
			.Content
			(
				new Grid().Children
				(
					new Frame().Assign(out contentFrame).Grid(row: 1)
				).RowDefinitions("40,*")
			)
			.SelectionChanged += OnShellSelectionChanged;

		navigationView.SelectedItem(navigationView.MenuItems[0]);

		this.Content(navigationView);
	}

	void OnShellSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
	{
		var item = (NavigationViewItem)args.SelectedItem;

		switch ((AppPages)item.Content)
		{
			case Player:
				contentFrame.Content = pages[Player];
				break;
			case Playground:
				contentFrame.Content = pages[Playground];
				break;
			case XamlPage:
				contentFrame.Content = pages[XamlPage];
				break;
		}
	}
}

enum AppPages
{
	Player,
	Playground,
	XamlPage
}
