using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoNetConf.Pages;
sealed partial class ComicsPage : Page
{
	public ComicsPage()
	{
		this.DataContext(new ComicsViewModel(), (page, vm) =>
		{
			page.Padding(58)
				.Background(Colors.Fuchsia)
				.Content
				(
					new Image()
						.Source(() => vm.ImageUrl)
						.HorizontalAlignment(HorizontalAlignment.Center)
						.VerticalAlignment(VerticalAlignment.Center)
						// .Height(300)
						// .Width(300)
						.Stretch(Stretch.Uniform)
				);
		});
	}
}
