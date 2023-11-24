using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoNetConf.Pages;
partial class PlayerPage : Page
{
	static SolidColorBrush backgroundColor = default!;


	const string playPath = @"F1 M 5.351562 1.523438 C 4.726562 1.158855 4.101562 1.145834 3.476562 1.484375 C 2.851562 1.848959 2.526042 2.395834 2.5 3.125 L 2.5 16.875 C 2.526042 17.604166 2.851562 18.151041 3.476562 18.515625 C 4.101562 18.854166 4.726562 18.841146 5.351562 18.476562 L 16.601562 11.601562 C 17.174479 11.236979 17.473957 10.703125 17.5 10 C 17.473957 9.322917 17.174479 8.789062 16.601562 8.398438 L 5.351562 1.523438 Z ";

	const string stopPath = @"F1 M 1.25 3.75 C 1.276042 3.046875 1.523438 2.460938 1.992188 1.992188 L 1.992188 1.992188 C 2.460938 1.523438 3.046875 1.276043 3.75 1.25 L 16.25 1.25 C 16.953125 1.276043 17.539062 1.523438 18.007812 1.992188 C 18.476562 2.460938 18.723957 3.046875 18.75 3.75 L 18.75 16.25 C 18.723957 16.953125 18.476562 17.539062 18.007812 18.007812 C 17.539062 18.476562 16.953125 18.723959 16.25 18.75 L 3.75 18.75 C 3.046875 18.723959 2.460938 18.476562 1.992188 18.007812 C 1.523438 17.539062 1.276042 16.953125 1.25 16.25 L 1.25 3.75 Z ";

	static readonly Resource<Geometry> playIcon =
		StaticResource.Create<Geometry>(nameof(playIcon), playPath);

	static readonly Resource<Geometry> stopIcon =
		StaticResource.Create<Geometry>(nameof(stopIcon), stopPath);

	public PlayerPage()
	{
		backgroundColor = (SolidColorBrush)Resources["ApplicationPageBackgroundThemeBrush"];
		this.DataContext(new PlayerViewModel(), (page, vm) =>
		{
			page
			.Background(backgroundColor)
			.Content(MainContent(vm))
			.Padding(0, 58, 0, 20)
			.Resources(r => r.Add(playIcon).Add(stopIcon));
		});
	}

	enum PlayerRows
	{
		GridImg = 1
	}

	Grid MainContent(PlayerViewModel vm)
	{
		return new Grid().Children
			(
				GridImg(vm).Grid(PlayerRows.GridImg)

			).RowDefinitions("100, *, 100");
	}

	Grid GridImg(PlayerViewModel vm)
	{
		new Grid().Children
			(
				new CircleImage()
				.Source(() => vm.Source)
				.Background(backgroundColor)
				.Grid()
				.Margin(20)
				.Assign(out var cicleImg),

				new CircleProgress()
				.StrokeWidth(2)
				.ProgressColor(Colors.Fuchsia)
				.LineBackgroundColor(Colors.Black)
				.IsHitTestVisible(false)
				.Progress(x => x.Bind(() => vm.Progress).Mode(BindingMode.TwoWay))
				.Grid()
				.Assign(out var circleP),

				new Button().Content
				(
					x => x.Bind(() => vm.IsPlaying).Convert(isPlaying =>
					{
						if (isPlaying)
						{
							return new PathIcon()
							.Data(StaticResource.Get<Geometry>(nameof(stopIcon)))
							.Foreground(Colors.White);
						}

						return new PathIcon()
						.Data(StaticResource.Get<Geometry>(nameof(playIcon)))
						.Foreground(Colors.White);
					})
				)
				.Width(60)
				.Height(60)
				.HorizontalAlignment(HorizontalAlignment.Center)
				.VerticalAlignment(VerticalAlignment.Center)
				.Background("#DE5154")
				.CornerRadius(30)
				.Command(() => vm.PlayExecuteCommand)
				.Grid(rowSpan: 3)

			).RowDefinitions("*, 48")
			.Assign(out Grid gridImg);

			gridImg.SizeChanged += (_, __) =>
			{
				var size = Math.Min(gridImg.ActualWidth, gridImg.ActualHeight);
				cicleImg.Width = size - 50;
				circleP.Width = gridImg.ActualHeight + 20;
				cicleImg.Invalidate();
				circleP.Invalidate();
			};

		return gridImg;
	}
}
