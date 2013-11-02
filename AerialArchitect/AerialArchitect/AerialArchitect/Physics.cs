using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AerialArchitect
{
    static class Physics
    {
        private const float MetreInPixels = 64;

        /// <summary>
        /// Converts a vector in pixels to metres for Farseer to use.
        /// </summary>
        /// <param name="pixels">A vector in pixels.</param>
        /// <returns></returns>
        public static Vector2 ToMetres(Vector2 pixels)
        {
            return pixels / MetreInPixels;
        }
        public static float ToMetres(float pixels)
        {
            return pixels / MetreInPixels;
        }

        /// <summary>
        /// Converts a vector using Farseer's metres to pixels.
        /// </summary>
        /// <param name="metres">A vector in metres.</param>
        /// <returns></returns>
        public static Vector2 ToPixels(Vector2 metres)
        {
            return metres * MetreInPixels;
        }
        public static float ToPixels(float metres)
        {
            return metres * MetreInPixels;
        }
    }
}
