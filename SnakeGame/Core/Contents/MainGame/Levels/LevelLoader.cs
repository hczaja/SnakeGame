using SnakeGame.Core.Contents.MainGame.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.Levels
{
    internal class LevelLoader
    {
        private static readonly string ROW_SPACING = Environment.NewLine;
        private static readonly string COL_SPACING = " ";

        private const string EMPTY = ".";
        private const string WALL = "=";
        private const string SNAKE = "s";
        private const string APPLE = "o";

        private const string RED_PORTAL_START = "[";
        private const string RED_PORTAL_END = "]";

        private const string BLUE_PORTAL_START = "{";
        private const string BLUE_PORTAL_END = "}";

        public Level Load(string path, IGameState state)
        {
            Level result = null;

            string name = string.Empty;
            int n = 0, m = 0;
            
            var mapBuilder = new StringBuilder();

            using (StreamReader file = new StreamReader(path))
            {
                string line = string.Empty;

                bool loadContext = true;
                while ((line = file.ReadLine()) != null)
                {
                    if (loadContext)
                    {
                        loadContext = false;
                        var context = line.Split(COL_SPACING);

                        name = context[0];
                        n = int.Parse(context[1]);
                        m = int.Parse(context[2]);
                    }
                    else
                    {
                        mapBuilder.AppendLine(line);   
                    }
                }

                file.Close();
            }

            result = new Level(name, n, m, state);
            
            string map = mapBuilder.ToString();
            var rows = map.Split(ROW_SPACING).Where(r => !string.IsNullOrEmpty(r)).ToArray();

            var portals = new List<PortalObject>();
            for (int i = 0; i < rows.Length; i++)
            {
                var cols = rows[i].Split(COL_SPACING);
                for (int j = 0; j < cols.Length; j++)
                {
                    IGameObject obj = cols[j] switch
                    {
                        WALL => new WallObject(j, i),
                        SNAKE => new SnakeObject(j, i),
                        APPLE => new AppleObject(j, i),
                        EMPTY => EmptyObject.Instance,
                        RED_PORTAL_START => new PortalObject(j, i, PortalType.Red),
                        RED_PORTAL_END => new PortalObject(j, i, PortalType.Red),
                        BLUE_PORTAL_START => new PortalObject(j, i, PortalType.Blue),
                        BLUE_PORTAL_END => new PortalObject(j, i, PortalType.Blue),
                        _ => EmptyObject.Instance
                    };

                    if (obj is PortalObject p)
                        portals.Add(p);

                    result.FillCell(j, i, obj);
                }
            }

            foreach (var group in portals.GroupBy(p => p.Type))
            {
                var start = group.First();
                var end = group.Last();

                start.SetDestination(end);
                end.SetDestination(start);
            }


            return result;
        }
    }
}
