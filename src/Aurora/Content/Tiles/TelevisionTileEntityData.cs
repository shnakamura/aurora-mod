using Microsoft.Xna.Framework.Media;
using ReLogic.Content;

namespace Aurora.Content.Tiles;

public sealed class TelevisionTileEntityData
{
	public static readonly Asset<Video>[] Videos = [
		ModContent.Request<Video>($"{nameof(Aurora)}/Assets/Videos/Video0", AssetRequestMode.ImmediateLoad),
		ModContent.Request<Video>($"{nameof(Aurora)}/Assets/Videos/Video1", AssetRequestMode.ImmediateLoad),
		ModContent.Request<Video>($"{nameof(Aurora)}/Assets/Videos/Video2", AssetRequestMode.ImmediateLoad),
		ModContent.Request<Video>($"{nameof(Aurora)}/Assets/Videos/Video3", AssetRequestMode.ImmediateLoad)
	];
	
	public int CurrentVideo {
		get => _currentVideo;
		set {
			if (value < 0) {
				_currentVideo = Videos.Length - 1;
			}
			else if (value > Videos.Length - 1) {
				_currentVideo = 0;
			}
			else {
				_currentVideo = value;
			}
		}
	}
	
	private int _currentVideo;

	public readonly VideoPlayer? Player = new() {
		IsLooped = true
	};
	
	public void Next() {
		if (Player.State != MediaState.Playing) {
			return;
		}
		
		CurrentVideo++;

		Player.Stop();
		Player.Play(Videos[CurrentVideo].Value);
	}

	public void Previous() {
		if (Player.State != MediaState.Playing) {
			return;
		}
		
		CurrentVideo--;
		
		Player.Stop();
		Player.Play(Videos[CurrentVideo].Value);
	}

	public void Toggle() {
		if (Player.State == MediaState.Playing) {
			Stop();
		}
		else {
			Start();
		}
	}

	public void Start() {
		if (Player.State == MediaState.Playing) {
			return;
		}

		Player.Play(Videos[CurrentVideo].Value);
	}

	public void Stop() {
		if (Player.State == MediaState.Stopped) {
			return;
		}
		
		Player.Stop();
	}
}
