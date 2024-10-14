using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostroDiBiscottiMonoGame
{
    public class Levels
    {
        public static List<string> list;
        public Levels() 
        {
            list = new List<string>();
            string levelsDirectory = Path.Combine(Environment.CurrentDirectory, "Levels");
            string[] files = Directory.GetFiles(levelsDirectory, "*.txt");
            foreach (string file in files)
            {
                string levelData = File.ReadAllText(file);
                levelData = levelData.Replace("\r\n", "").Replace("\n", "");
                Level level = new Level(levelData);
                list.Add(level.Tiles);
            }
        }
    }
}
