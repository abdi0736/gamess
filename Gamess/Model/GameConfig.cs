using System;
using System.Xml;

public class GameConfig
{
    public int WorldWidth { get; private set; }
    public int WorldHeight { get; private set; }
    public string GameLevel { get; private set; }
    public string LoggingLevel { get; private set; } = "Verbose";
    public string LoggingOutput { get; private set; } = "Console";

    public void Load(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);

        // Hent World MaxX og MaxY
        var worldNode = doc.DocumentElement.SelectSingleNode("World");
        if (worldNode != null)
        {
            WorldWidth = Convert.ToInt32(worldNode.SelectSingleNode("MaxX")?.InnerText);
            WorldHeight = Convert.ToInt32(worldNode.SelectSingleNode("MaxY")?.InnerText);
        }

        // Hent GameLevel
        GameLevel = doc.DocumentElement.SelectSingleNode("GameLevel")?.InnerText.Trim();

        // Hent Logging info
        var loggingNode = doc.DocumentElement.SelectSingleNode("Logging");
        if (loggingNode != null)
        {
            LoggingLevel = loggingNode.SelectSingleNode("Level")?.InnerText.Trim() ?? "Verbose";
            LoggingOutput = loggingNode.SelectSingleNode("Output")?.InnerText.Trim() ?? "Console";
        }
    }
}