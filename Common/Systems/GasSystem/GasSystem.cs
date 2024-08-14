using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace Aurora.Common.Systems.GasSystem
{
    public class GasSystem : ModSystem
    {
        public static int[,] Gas;
        public static List<Point> Active;
        public const int NumGas = 3;
        public static int Sulfur = 1;
        public static int Steam = 2;
        public static int Swamp = 3;

        /// <summary>
        ///  A cache of the previous gas state. Don't use it in your own code, the base code handles everything that it does with it.
        /// </summary>
        public static float[] GasCache;

        private int clear = 0;
        public override void OnWorldLoad()
        {
            Gas = new int[Main.tile.Width, Main.tile.Height];
            GasCache = new float[NumGas];
            Active = new();
        }

        public static bool Clear = false;

        /// <summary>
        ///  Adds gas of type Type at the tile coord pos.
        ///  Note: If adding gass in a tile, do so in the PreDraw() method. 
        /// </summary>
        public static void AddGas(Point pos, int type)
        {
            Gas[pos.X,pos.Y] = type;
            if(!Active.Contains(pos)) Active.Add(pos);
        }

        public override void PostUpdateEverything()
        {
            
            for(int Balls = Active.Count -1; Balls >= 0; Balls--)
            {
                Point p = Active[Balls];
                if (Gas[p.X,p.Y] == 0)
                {
                    Active.Remove(p);
                }
            }

            if (Clear)
            {
                Clear = false;
                Array.Clear(Gas);
            }


           
        }

        /// <summary>
        ///  Returns an array containing the "concentration" of all gasses between 0 - 1 based off maxDist
        ///  Note: If using in a ModPlayer, use in the UpdateEquips() method. Idk why but thats the only way it works tmk
        /// </summary>
        public static float[] GasAtPos(Vector2 pos, float maxDist)
        {
            float[] gasses = new float[NumGas];
            if (Active.Count == 0) return null;
            foreach(Point p in Active)
            {
                float distance = Vector2.Distance(p.ToVector2() * 16, pos);
                if (distance <= maxDist)
                {
                    //Main.NewText(Gas[p.X, p.Y]);
                    gasses[Gas[p.X, p.Y]] = MathHelper.Lerp(1f, 0f, distance / maxDist);
                }
            }

            if (Clear) { GasCache = gasses; return gasses; }
            else return GasCache;


        }
    }
}
