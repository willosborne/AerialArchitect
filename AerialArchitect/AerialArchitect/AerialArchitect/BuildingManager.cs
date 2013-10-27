using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;
using System.IO;

namespace AerialArchitect
{
    static class BuildingManager
    {
        static Lua lua;
        static string baseBuildingsDirectory = "buildings";

        static Dictionary<string, string> InitPaths;

        public static string GetPath(string key)
        {
            return InitPaths[key];
        }

        static BuildingManager()
        {
            lua = new Lua();
            InitPaths = new Dictionary<string,string>();
            //Scan();
            //lua.
        }

        public static void Scan()
        {
            string fullPath = Environment.CurrentDirectory;
            fullPath = Path.Combine(fullPath, Constants.DataDirectory, baseBuildingsDirectory);
            Console.WriteLine(fullPath);

            DirectoryInfo dir = new DirectoryInfo(fullPath);

            foreach (var file in dir.GetFiles())
            {
                lua = new Lua();
                lua.DoFile(file.FullName);
                string name = (string)lua["name"];
                Console.WriteLine(name);
                Console.WriteLine(file.FullName);
                InitPaths.Add(name, file.FullName);
            }
            //Console.WriteLine("foo!");
        }
    }
}
