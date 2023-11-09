using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode.Search;
using YoutubeExplode;
using YoutubeExplode.Common;

namespace UnoNetConf.Services;




sealed class AllServices
{
	static readonly Lazy<AllServices> lazyServices = new(() => new());
	readonly YoutubeClient client;

	public static AllServices Current => lazyServices.Value;
	AllServices() => client = new YoutubeClient();

	public async IAsyncEnumerable<SearchResult> SearchMediaAsync(string name, [EnumeratorCancellation] CancellationToken token = default)
	{
		int count = 0;
		await foreach (var item in client.Search.GetVideosAsync(name, token).ConfigureAwait(false))
		{
			if (count > 30)
				break;
			count++;

			yield return new SearchResult(item.Title, item.Thumbnails.TryGetWithHighestResolution()!.Url, item.Duration.ToString()!);
		}
	}
}
