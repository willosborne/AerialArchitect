using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;
using System.IO;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace AerialArchitect
{
    static class BuildingManager
    {
        static Lua lua;
        static string baseBuildingsDirectory = "buildings";

        static Dictionary<string, string> InitPaths;

        static World World;

        static List<Building> Buildings;

        public static string GetPath(string key)
        {
            return InitPaths[key];
        }

        static BuildingManager()
        {
            lua = new Lua();
            InitPaths = new Dictionary<string,string>();
            Buildings = new List<Building>();
            //Scan();
            //lua.
        }

        public static void Initialize(World _world)
        {
            World = _world;
        }

        /// <summary>
        /// Create a building.
        /// </summary>
        /// <param name="key">The key of the building to create as defined in its .lua file</param>
        /// <param name="position">Position, in pixels, to create the building at</param>
        /// <returns></returns>
        public static Building CreateBuilding(string key, Vector2 position)
        {
            Building Output = new Building();

            lua = new Lua();
            lua.DoFile(GetPath(key));

            if ((string)lua["type"] == "building")
            {
                Output.Name = (string)lua["name"];
                Output.Description = (string)lua["description"];
                //Output.Position = position;
                //Output.Rotation = (float)(double)lua["rotation"];

                #region Drawables
                LuaTable drawables = lua.GetTable("drawableParts");
                Output.BuildingParts = new List<DrawableBuildingPart>(drawables.Values.Count);

                foreach (LuaTable table in drawables.Values)
                {
                 
                    DrawableBuildingPart part = new DrawableBuildingPart();
                    part.Image = Art.LoadImage((string)table["imagePath"]);
                    part.Alpha = (float)(double)table["alpha"];
                    part.Tint = LuaHelper.LuaToColor((LuaTable)table["tint"]);
                    part.Offset = new Vector2((int)(double)table["offsetX"], (int)(double)table["offsetY"]);
                    part.Rotation = (float)(double)table["rotation"];

                    Output.BuildingParts.Add(part);
                    //part.Offset = new Vector2(
                }
                #endregion

                #region Physics
                Output.Body = BodyFactory.CreateBody(World);
                Output.Body.BodyType = BodyType.Dynamic;
                Output.Body.FixedRotation = true;
                Output.Body.Position = Physics.ToMetres(position);

                LuaTable physicsShapes = lua.GetTable("physicsShapes");
                LuaTable rectangles = (LuaTable)physicsShapes["rectangles"];
                foreach (LuaTable table in rectangles.Values)
                {
                    Vector2 centre = new Vector2(
                        Physics.ToMetres((float)(double)table["centreX"]),
                        Physics.ToMetres((float)(double)table["centreY"])
                        );
                    float width = Physics.ToMetres((float)(double)table["width"]);
                    float height = Physics.ToMetres((float)(double)table["height"]);
                    //float rotation = (float)(double)table["rotation"];
                    //Body body = BodyFactory.CreateRectangle(World, width, height, 1, centre);
                    Fixture fix = FixtureFactory.AttachRectangle(width, height, 1, centre, Output.Body);
                    //Fixture fix = new Fixture(
                    //FixtureFactory.
                    //FixtureDef
                    //fix.
                    //body.Rotation = rotation;
                    //Body
                }
                LuaTable circles = (LuaTable)physicsShapes["circles"];
                foreach (LuaTable table in circles)
                {
                    Vector2 centre = new Vector2(
                        Physics.ToMetres((float)(double)table["centreX"]),
                        Physics.ToMetres((float)(double)table["centreY"])
                        );
                    float radius = Physics.ToMetres((float)(double)table["radius"]);
                }
                #endregion
            }
            //lua.



            Buildings.Add(Output);
            return Output;
        }

        public static void Scan()
        {
            string fullPath = Environment.CurrentDirectory;
            fullPath = Path.Combine(fullPath, Constants.DataDirectory, baseBuildingsDirectory);
            //Console.WriteLine(fullPath);

            DirectoryInfo dir = new DirectoryInfo(fullPath);

            foreach (var file in dir.GetFiles("*.lua"))
            {
                lua = new Lua();
                lua.DoFile(file.FullName);
                if ((string)lua["type"] == "building")
                {
                    string key = (string)lua["key"];
                    Console.WriteLine(key);
                    //Console.WriteLine(file.FullName);
                    InitPaths.Add(key, file.FullName);
                }
                //lua.Close();
            }
            //Console.WriteLine("foo!");
        }

        public static void DrawBuilding(SpriteBatch spriteBatch, Building building)
        {
            Vector2 basePos = Physics.ToPixels(building.Body.Position);

            for (int i = 0; i < building.BuildingParts.Count; i++)
            {
                DrawableBuildingPart part = building.BuildingParts[i];

                spriteBatch.Draw(part.Image, basePos + part.Offset, null, part.Tint * part.Alpha, part.Rotation, part.Origin, 1, 0, 0);
                //building.Bui
            }
        }
    }
}
