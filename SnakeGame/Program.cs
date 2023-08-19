using Engine.Core;
using SnakeGame;

var settings = new SnakeSettings();
var engine = new GameEngine(new Snake(settings), settings);
engine.Start();