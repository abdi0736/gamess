using System;
using System.Xml.Linq;

namespace Gamess.world
{
    public static class Config
    {
        public static int MaxX { get; private set; } = 10;
        public static int MaxY { get; private set; } = 10;
        public static string Level { get; private set; } = "novice";
        public static string LogOutput { get; private set; } = "Console";
        public static string LogLevel { get; private set; } = "Verbose";

        public static void Load(string filePath)
        {
            try
            {
                var doc = XDocument.Load(filePath);
                MaxX = int.Parse(doc.Root.Element("MaxX")?.Value ?? "10");
                MaxY = int.Parse(doc.Root.Element("MaxY")?.Value ?? "10");
                Level = doc.Root.Element("Level")?.Value ?? "novice";
                LogOutput = doc.Root.Element("LogOutput")?.Value ?? "Console";
                LogLevel = doc.Root.Element("LogLevel")?.Value ?? "Verbose";

                GameLogger.Log("Configuration loaded successfully.");
            }
            catch (Exception ex)
            {
                GameLogger.Log($"Error loading configuration: {ex.Message}");
            }
        }
    }
}