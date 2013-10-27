using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AerialArchitect
{
    class DrawableBuildingPart
    {
        public Texture2D Image;
        public Vector2 Offset;
        
        private float rotation;
        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                rotation = MathHelper.WrapAngle(rotation);
            }
        }


        private Vector2 origin = Constants.InvalidVector2;
        public Vector2 Origin
        {
            get
            {
                if (origin == Constants.InvalidVector2)
                    origin = new Vector2(Image.Width / 2, Image.Height / 2);               

                return origin;           
            }
            set
            {
                origin = value;
            }
        }
    }
}
