using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostroDiBiscottiMonoGame
{
    public class Level
    {
        public string Tiles { get; set; }

        public Level(string levelData)
        {
            Tiles = levelData;
        }
    }
}
