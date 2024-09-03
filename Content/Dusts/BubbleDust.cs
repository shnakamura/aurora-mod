namespace Aurora.Content.Dusts;

public class BubbleDust : ModDust
{
    public override void OnSpawn(Dust dust) {
        dust.frame = new Rectangle(0, Main.rand.Next(3) * 10, 10, 10);
        
        dust.noLight = true;
        dust.noGravity = true;
        
        dust.alpha = 100;
        
        dust.scale *= Main.rand.NextFloat(0.9f, 1.2f);
        dust.rotation = Main.rand.NextFloat(MathHelper.TwoPi);
    }
    
    public override bool Update(Dust dust) {
        dust.position += dust.velocity;
        dust.velocity *= 0.99f;

        dust.scale -= 0.025f;
        
        dust.rotation += dust.velocity.ToRotation() * 0.1f;

        var isColliding = !WorldGen.TileEmpty((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f));

        if (isColliding) {
            dust.alpha += 15;
        }
        else {
            dust.alpha += 3;
        }
        
        var isVisible = dust.scale <= 0f || dust.alpha >= 255;    
    
        if (isVisible) {
            dust.active = false;
        }

        return false;
    }
}