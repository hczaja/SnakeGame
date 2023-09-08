using SnakeGame.Core.Contents.MainGame.GameObjects.Interactive;
using SnakeGame.Core.Contents.MainGame.GameObjects.Player;
using SnakeGame.Core.Contents.MainGame.GameObjects.Walls;
using SnakeGame.Core.Contents.MainGame.Levels.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            var builder = new StringBuilder();
            using (var file = new StreamReader("Assets/Levels/01_01.json"))
            {
                string line = string.Empty;
                while ((line = file.ReadLine()) != null)
                {
                    builder.Append(line);
                }
            }

            string json = builder.ToString();
            var dto = JsonSerializer.Deserialize<LevelDto>(json);

            var result = new Level(dto.Name, 32, 32, state);
            result.FillCell(dto.Player.X, dto.Player.Y, new SnakeObject(dto.Player.X, dto.Player.Y));

            foreach (var wall in  dto.Walls)
            {
                result.FillCell(wall.X, wall.Y, new WallObject(wall.X, wall.Y));
            }

            foreach (var apple in dto.Apples)
            {
                var type = apple.Type switch
                {
                    "Red" => AppleType.Red,
                    "Yellow" => AppleType.Yellow,
                    "Green" => AppleType.Green,
                    _ => AppleType.Red
                };

                result.FillCell(apple.X, apple.Y, new AppleObject(apple.X, apple.Y, type));
            }

            foreach (var key in dto.Keys)
            {
                var type = key.Type switch
                {
                    "Bronze" => KeyType.Bronze,
                    "Silver" => KeyType.Silver,
                    "Gold" => KeyType.Gold,
                    _ => KeyType.Bronze
                };

                result.FillCell(key.X, key.Y, new KeyObject(key.X, key.Y, type));
            }

            foreach (var door in dto.Doors)
            {
                result.FillCell(door.X, door.Y, new DoorObject(door.X, door.Y));
            }

            foreach (var fountain in dto.Fountains)
            {
                result.FillCell(fountain.X, fountain.Y, new FountainObject(fountain.X, fountain.Y));
            }

            //string name = string.Empty;
            //int n = 0, m = 0;

            //var mapBuilder = new StringBuilder();

            //using (StreamReader file = new StreamReader(path))
            //{
            //    string line = string.Empty;

            //    bool loadContext = true;
            //    while ((line = file.ReadLine()) != null)
            //    {
            //        if (loadContext)
            //        {
            //            loadContext = false;
            //            var context = line.Split(COL_SPACING);

            //            name = context[0];
            //            n = int.Parse(context[1]);
            //            m = int.Parse(context[2]);
            //        }
            //        else
            //        {
            //            mapBuilder.AppendLine(line);   
            //        }
            //    }

            //    file.Close();
            //}

            //result = new Level(name, n, m, state);

            //string map = mapBuilder.ToString();
            //var rows = map.Split(ROW_SPACING).Where(r => !string.IsNullOrEmpty(r)).ToArray();

            //var portals = new List<PortalObject>();
            //for (int i = 0; i < rows.Length; i++)
            //{
            //    var cols = rows[i].Split(COL_SPACING);
            //    for (int j = 0; j < cols.Length; j++)
            //    {
            //        IGameObject obj = cols[j] switch
            //        {
            //            WALL => new WallObject(j, i),
            //            SNAKE => new SnakeObject(j, i),
            //            APPLE => new AppleObject(j, i),
            //            EMPTY => EmptyObject.Instance,
            //            RED_PORTAL_START => new SpiderEnemyObject(j, i), /*new PortalObject(j, i, PortalType.Red)*/
            //            //RED_PORTAL_END => new PortalObject(j, i, PortalType.Red),
            //            BLUE_PORTAL_START => new PortalObject(j, i, PortalType.Blue),
            //            BLUE_PORTAL_END => new PortalObject(j, i, PortalType.Blue),
            //            _ => EmptyObject.Instance
            //        };

            //        if (obj is PortalObject p)
            //            portals.Add(p);

            //        result.FillCell(j, i, obj);
            //    }
            //}

            //foreach (var group in portals.GroupBy(p => p.Type))
            //{
            //    var start = group.First();
            //    var end = group.Last();

            //    start.SetDestination(end);
            //    end.SetDestination(start);
            //}


            return result;
        }
    }
}
