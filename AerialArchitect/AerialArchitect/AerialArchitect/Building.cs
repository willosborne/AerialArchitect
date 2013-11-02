using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;

namespace AerialArchitect
{
    class Building
    {
        public string Name;
        public string Description;

        public List<DrawableBuildingPart> BuildingParts;
        //public DrawableBuildingPart[] BuildingParts;
        public Vector2 Position
        {
            get
            {
                if (Body != null)
                    return Body.Position;
                else
                    throw new Exception("Building not initialized.");
            }
            set
            {
                if (Body != null)
                    Body.Position = value;
                else
                    throw new Exception("Building not initialized.");
            }
        }
        public float Rotation = 0;
        public Body Body;
        //public

        public Building()
        {
            //BuildingParts = new List<DrawableBuildingPart>();
        }
    }
}
