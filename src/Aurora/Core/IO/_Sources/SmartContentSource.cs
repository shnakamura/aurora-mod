using System.Collections.Generic;
using System.IO;
using System.Linq;
using ReLogic.Content;
using ReLogic.Content.Sources;

// Code written and provided by @steviegt6 at GitHub.
namespace Aurora.Core.IO;

/// <summary>
///		A wrapper around an <see cref="IContentSource"/> instance with additional functionalities
///		for directory redirects.
/// </summary>
internal sealed class SmartContentSource(IContentSource source) : IContentSource
{
	private readonly Dictionary<string, string> redirects = [];

	IContentValidator IContentSource.ContentValidator {
		get => source.ContentValidator;
		set => source.ContentValidator = value;
	}

	RejectedAssetCollection IContentSource.Rejections => source.Rejections;

	IEnumerable<string> IContentSource.EnumerateAssets() {
		return source.EnumerateAssets().Select(RewritePath);
	}

	string IContentSource.GetExtension(string assetName) {
		return source.GetExtension(RewritePath(assetName));
	}

	Stream IContentSource.OpenStream(string fullAssetName) {
		return source.OpenStream(RewritePath(fullAssetName));
	}

	/// <summary>
	///		Adds a directory redirect from <paramref name="fromDir"/> to <paramref name="toDir"/>.
	/// </summary>
	/// <param name="fromDir">The directory to redirect from.</param>
	/// <param name="toDir">The directory to redirect to.</param>
	public void AddRedirect(string fromDir, string toDir) {
		redirects.Add(fromDir, toDir);
	}

	private string RewritePath(string path) {
		foreach (var (from, to) in redirects) {
			if (path.StartsWith(from)) {
				return path.Replace(from, to);
			}
		}

		return path;
	}
}
