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
	
	public readonly Point16 Coordinates;

	public readonly TelevisionTileEntityData Data;

	private UIElement root;
	
	public UITelevision(Point16 coordinates, TelevisionTileEntityData data) {
		Coordinates = coordinates;
		Data = data;
	}
	
	public override void OnInitialize() {
		base.OnInitialize();

		var position = Coordinates.ToVector2() * 16f - new Vector2(48f, 80f);
		var transform = Vector2.Transform(position - Main.screenPosition, Main.GameViewMatrix.ZoomMatrix) / Main.UIScale;

		root = new UIElement {
			Left = { Pixels = transform.X },
			Top = { Pixels = transform.Y },
			Width = { Pixels = 64f },
			Height = { Pixels = 26f }
		};
		
		Append(root);

		var panel = new UIPanel(
			ModContent.Request<Texture2D>($"{nameof(Aurora)}/Assets/Textures/UI/PanelBackground"),
			ModContent.Request<Texture2D>($"{nameof(Aurora)}/Assets/Textures/UI/PanelBorder"),
			13
		) {
			Width = { Precent = 1f },
			Height = { Precent = 1f }
		};
		
		root.Append(panel);

		var power = new UIImage(ModContent.Request<Texture2D>($"{nameof(Aurora)}/Assets/Textures/UI/PowerIcon")) {
			VAlign = 0.5f,
			Left = { Pixels = 2f },
			OverrideSamplerState = SamplerState.PointClamp
		};

		power.OnLeftClick += (_, _) => Data.Toggle();
		
		power.OnUpdate += _ => {
			power.ImageScale = MathHelper.SmoothStep(power.ImageScale, power.IsMouseHovering ? 1.1f : 1f, 0.2f);
		};
		
		root.Append(power);
		
		var next = new UIImage(ModContent.Request<Texture2D>($"{nameof(Aurora)}/Assets/Textures/UI/NextIcon")) {
			HAlign = 0.5f,
			VAlign = 0.5f,
			OverrideSamplerState = SamplerState.PointClamp
		};

		next.OnLeftClick += (_, _) => Data.Next();
		
		next.OnUpdate += _ => {
			next.ImageScale = MathHelper.SmoothStep(next.ImageScale, next.IsMouseHovering ? 1.1f : 1f, 0.2f);
		};
		
		root.Append(next);
		
		var previous = new UIImage(ModContent.Request<Texture2D>($"{nameof(Aurora)}/Assets/Textures/UI/PreviousIcon")) {
			HAlign = 1f,
			VAlign = 0.5f,
			Left = { Pixels = -2f },
			OverrideSamplerState = SamplerState.PointClamp
		};

		previous.OnLeftClick += (_, _) => Data.Previous();
		
		previous.OnUpdate += _ => {
			previous.ImageScale = MathHelper.SmoothStep(previous.ImageScale, previous.IsMouseHovering ? 1.1f : 1f, 0.2f);
		};
		
		root.Append(previous);
	}

	public override void Update(GameTime gameTime) {
		base.Update(gameTime);
		
		if (root.ContainsPoint(Main.MouseScreen)) {
			Main.LocalPlayer.mouseInterface = true;
		}

		var position = Coordinates.ToVector2() * 16f - new Vector2(48f, 80f);
		var transform = Vector2.Transform(position - Main.screenPosition, Main.GameViewMatrix.ZoomMatrix) / Main.UIScale;

		root.Left.Set(transform.X, 0f);
		root.Top.Set(transform.Y, 0f);
	}
}
