using System.Security.Cryptography;

namespace Engine.Random;

public class Dice
{
    public static Dice Instance { get; set; } = new Dice();

    private Dice() { }

    public int Roll6k() => RandomNumberGenerator.GetInt32(5) + 1;
    public int Roll10k() => RandomNumberGenerator.GetInt32(9) + 1;
    public int Roll12k() => RandomNumberGenerator.GetInt32(11) + 1;
}
