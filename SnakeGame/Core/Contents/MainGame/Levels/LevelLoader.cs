using SnakeGame.Core.Contents.MainGame.GameObjects.Enemies;
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

            foreach (var enemy in dto.Enemies)
            {
                var obj = enemy.Type switch
                {
                    "BlackSpider" => new BlackSpiderEnemyObject(enemy.X, enemy.Y),
                    _ => new BlackSpiderEnemyObject(enemy.X, enemy.Y)
                };

                result.FillCell(enemy.X, enemy.Y, obj);
            }

            return result;
        }
    }
}
