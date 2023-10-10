using Engine.Core;
using SnakeGame;
using Snakeventures;

var settings = new SnakeSettings();
var engine = new GameEngine(
    new SnakeventuresCore(settings), settings);

engine.Start();