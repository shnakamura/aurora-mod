namespace Aurora.Core.Graphics;

/// <summary>
///     Handles the rendering of the player's full texture and storing it as a <see cref="RenderTarget2D" />.
/// </summary>
[Autoload(Side = ModSide.Client)]
public sealed class PlayerRenderSystem : ModSystem
{
    /// <summary>
    ///     The render target that contains the player's full texture.
    /// </summary>
    public static RenderTarget2D? Target { get; private set; }

    public override void Load() {
        base.Load();

        Main.QueueMainThreadAction(
            () => {
                Target = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
            }
        );
        
        Main.OnResolutionChanged += ResizeTarget;
    }

    public override void Unload() {
        base.Unload();
        
        Main.QueueMainThreadAction(
	        () => {
		        Target?.Dispose();
		        Target = null;
	        }
	    );

        Main.OnResolutionChanged -= ResizeTarget;
    }

    public override void PreUpdateEntities() {
	    base.PreUpdateEntities();
	    
	    var spriteBatch = Main.spriteBatch;
	    var device = Main.graphics.GraphicsDevice;

	    var oldTargets = device.GetRenderTargets();

	    device.SetRenderTarget(Target);
	    device.Clear(Color.Transparent);

	    spriteBatch.Begin(default, default, default, default, default, default, Main.GameViewMatrix.TransformationMatrix);

	    var player = Main.LocalPlayer;

	    if (player.active) {
		    Main.PlayerRenderer?.DrawPlayer(Main.Camera, player, player.position, player.fullRotation, player.fullRotationOrigin);
	    }

	    spriteBatch.End();

	    device.SetRenderTargets(oldTargets);
    }

    private static void ResizeTarget(Vector2 resolution) {
        Main.QueueMainThreadAction(
            () => {
                Target = new RenderTarget2D(Main.graphics.GraphicsDevice, (int)resolution.X, (int)resolution.Y);
            }
        );
    }
}
