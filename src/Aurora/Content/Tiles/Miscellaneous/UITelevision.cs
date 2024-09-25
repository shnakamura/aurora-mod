using Microsoft.Xna.Framework.Media;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Aurora.Content.Tiles;

public sealed class UITelevision : UIState
{
	/// <summary>
	///		The unique identifier of this state.
	/// </summary>
	public const string Identifier = $"{nameof(Aurora)}:{nameof(UITelevision)}";

	private static readonly Asset<Video>?[] Videos = [
		ModContent.Request<Video>($"{nameof(Aurora)}/Assets/Videos/Video0", AssetRequestMode.ImmediateLoad),
		ModContent.Request<Video>($"{nameof(Aurora)}/Assets/Videos/Video1", AssetRequestMode.ImmediateLoad),
		ModContent.Request<Video>($"{nameof(Aurora)}/Assets/Videos/Video2", AssetRequestMode.ImmediateLoad),
		ModContent.Request<Video>($"{nameof(Aurora)}/Assets/Videos/Video3", AssetRequestMode.ImmediateLoad),
	];

	public static int Index {
		get => index;
		private set => index = (value % Videos.Length + Videos.Length) % Videos.Length;
	}

	private static int index;

	private static VideoPlayer? player;

	public override void OnInitialize() {
		base.OnInitialize();

		var root = new UIElement {
			HAlign = 0.5f,
			VAlign = 0.5f,
			Width = { Pixels = 500f + 4f + 50f },
			Height = { Pixels = 300f }
		};

		Append(root);

		var background = new UIPanel(
			ModContent.Request<Texture2D>($"{nameof(Aurora)}/Assets/Textures/UI/PanelBackground"),
			ModContent.Request<Texture2D>($"{nameof(Aurora)}/Assets/Textures/UI/PanelBorder"),
			13
		) {
			Width = { Pixels = 500f },
			Height = { Precent = 1f },
			OverrideSamplerState = SamplerState.PointClamp
		};

		root.Append(background);

		var panel = new UIPanel(
			ModContent.Request<Texture2D>($"{nameof(Aurora)}/Assets/Textures/UI/PanelBackground"),
			ModContent.Request<Texture2D>($"{nameof(Aurora)}/Assets/Textures/UI/PanelBorder"),
			13
		) {
			HAlign = 1f,
			Width = { Pixels = 50f },
			Height = { Precent = 1f },
			OverrideSamplerState = SamplerState.PointClamp
		};

		root.Append(panel);
	}

	public override void OnActivate() {
		base.OnActivate();

		player = new VideoPlayer() {
			IsLooped = true
		};

		player.Play(Videos[Index].Value);
	}

	public override void OnDeactivate() {
		base.OnDeactivate();

		player?.Dispose();
		player = null;
	}

	public override void Draw(SpriteBatch spriteBatch) {
		base.Draw(spriteBatch);

		if (player?.State != MediaState.Playing) {
			return;
		}

		var texture = player.GetTexture();
		var dimensions = GetDimensions();

		var margin = 10;

		var width = 500;
		var height = 300;

		var offset = 27;

		var rectangle = new Rectangle(
			Main.screenWidth / 2 - width / 2 + margin / 2 - offset,
			Main.screenHeight / 2 - height / 2 + margin / 2,
			width - margin,
			height - margin
		);

		spriteBatch.Draw(texture, rectangle, Color.White * 0.9f);
	}
}
