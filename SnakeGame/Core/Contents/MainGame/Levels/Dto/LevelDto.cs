using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Core.Contents.MainGame.Levels.Dto
{
    public class LevelDto
    {
        public string Name { get; set; }

        public required PlayerDto Player { get; set; }
        public required WallDto[] Walls { get; set; }
        public required DoorDto[] Doors { get; set; }
        public required AppleDto[] Apples { get; set; }
        public required FountainDto[] Fountains { get; set; }
        public EnemyDto[]? Enemies { get; set; }
    }

    public class PlayerDto
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class WallDto
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class AppleDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public required string Type { get; set; }
    }

    public class EnemyDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public required string Type { get; set; }
    }

    public class DoorDto
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class FountainDto
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
