using System.Xml.Linq;
using System;
using Gamess.Model;

namespace Gamess.Config
{
    public class GameConfig
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public GameDifficulty Level { get; set; }
        public string LogOutput { get; set; } = "Console";
        public string LogLevel { get; set; } = "Info";

        public static GameConfig LoadFromFile(string path)
        {
            var xml = XDocument.Load(path);
            var config = new GameConfig
            {
                MaxX = int.Parse(xml.Root.Element("World")?.Element("MaxX")?.Value ?? "10"),
                MaxY = int.Parse(xml.Root.Element("World")?.Element("MaxY")?.Value ?? "10"),
                Level = Enum.TryParse(xml.Root.Element("GameLevel")?.Value, true, out GameDifficulty lvl) ? lvl : GameDifficulty.Normal,
                LogOutput = xml.Root.Element("Logging")?.Element("Output")?.Value ?? "Console",
                LogLevel = xml.Root.Element("Logging")?.Element("Level")?.Value ?? "Info"
            };

            return config;
        }
    }
}