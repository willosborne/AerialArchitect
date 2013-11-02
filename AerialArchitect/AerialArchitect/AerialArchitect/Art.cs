using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework;
//using FarseerPhysics.Common;

namespace AerialArchitect
{
    static class Art
    {
        static BlendState blendColor, blendAlpha;
        static string baseArtDirectory = "images";

        static GraphicsDevice device;

        public static Dictionary<string, Texture2D> Images;

        public static void Initialize(GraphicsDevice graphics)
        {
            device = graphics;
            Images = new Dictionary<string, Texture2D>();
        }

        public static Texture2D LoadImage(string fileName)
        {
            if (Images.ContainsKey(fileName))
            {
                return Images[fileName];
            }
            else
            {
                //string fullPath = Environment.CurrentDirectory;
                //fullPath = Path.Combine(fullPath, Constants.DataDirectory, baseArtDirectory, path);
                //Console.WriteLine(fullPath);
                Texture2D image = LoadTextureStream(fileName);
                Images.Add(fileName, image);
                return image;
            }
        }       

        static Texture2D LoadTextureStream(string path)
        {
            
            string fullPath = Path.Combine(Environment.CurrentDirectory, Constants.DataDirectory, baseArtDirectory, path);
            Console.WriteLine(fullPath);

            Texture2D file = null;
            RenderTarget2D result = null;
 
            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                file = Texture2D.FromStream(device, stream);
            }
 
            //Setup a render target to hold our final texture which will have premulitplied alpha values
            result = new RenderTarget2D(device, file.Width, file.Height);
 
            device.SetRenderTarget(result);
            device.Clear(Color.Black);
 
            //Multiply each color by the source alpha, and write in just the color values into the final texture
            if (blendColor == null)
            {
                blendColor = new BlendState();
                blendColor.ColorWriteChannels = ColorWriteChannels.Red | ColorWriteChannels.Green | ColorWriteChannels.Blue;
 
                blendColor.AlphaDestinationBlend = Blend.Zero;
                blendColor.ColorDestinationBlend = Blend.Zero;
 
                blendColor.AlphaSourceBlend = Blend.SourceAlpha;
                blendColor.ColorSourceBlend = Blend.SourceAlpha;
            }
 
            SpriteBatch spriteBatch = new SpriteBatch(device);
            spriteBatch.Begin(SpriteSortMode.Immediate, blendColor);
            spriteBatch.Draw(file, file.Bounds, Color.White);
            spriteBatch.End();
 
            //Now copy over the alpha values from the PNG source texture to the final one, without multiplying them
            if (blendAlpha == null)
            {
                blendAlpha = new BlendState();
                blendAlpha.ColorWriteChannels = ColorWriteChannels.Alpha;
 
                blendAlpha.AlphaDestinationBlend = Blend.Zero;
                blendAlpha.ColorDestinationBlend = Blend.Zero;
 
                blendAlpha.AlphaSourceBlend = Blend.One;
                blendAlpha.ColorSourceBlend = Blend.One;
            }
 
            spriteBatch.Begin(SpriteSortMode.Immediate, blendAlpha);
            spriteBatch.Draw(file, file.Bounds, Color.White);
            spriteBatch.End();
 
            //Release the GPU back to drawing to the screen
            device.SetRenderTarget(null);
 
            return result as Texture2D;
        }
    }
}
