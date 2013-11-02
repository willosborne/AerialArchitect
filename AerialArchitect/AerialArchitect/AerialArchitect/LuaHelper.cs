using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NLua;

namespace AerialArchitect
{
    static class LuaHelper
    {
        public static Color LuaToColor(LuaTable table)
        {
            if (table.Values.Count == 3)
            {
                Color color = new Color();
                color.R = (byte)(double)table[1];
                color.G = (byte)(double)table[2];
                color.B = (byte)(double)table[3];
                color.A = 255;

                return color;
            }
            else
            {
                throw new Exception("Invalid Lua Color!");
            }
        }

        public static Vector2 LuaToVector2(LuaTable table)
        {
            Vector2 vector = new Vector2();
            vector.X = (float)(double)table[0];
            vector.Y = (float)(double)table[1];

            return vector;
        }
    }
}
