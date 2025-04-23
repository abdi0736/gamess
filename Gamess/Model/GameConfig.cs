using System.Xml;

namespace Gamess
{
    public class GameConfig
    {
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }
        public string GameLevel { get; private set; }
        public string LoggingLevel { get; private set; } = "Verbose";
        public string LoggingOutput { get; private set; } = "Console";

        public void Load(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
    
            MaxX = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("World/MaxX")?.InnerText);
            MaxY = Convert.ToInt32(doc.DocumentElement.SelectSingleNode("World/MaxY")?.InnerText);
            GameLevel = doc.DocumentElement.SelectSingleNode("GameLevel")?.InnerText.Trim();

            var loggingNode = doc.DocumentElement.SelectSingleNode("Logging");
            if (loggingNode != null)
            {
                LoggingLevel = loggingNode.SelectSingleNode("Level")?.InnerText.Trim() ?? "Verbose";
                LoggingOutput = loggingNode.SelectSingleNode("Output")?.InnerText.Trim() ?? "Console";
            }
        }
    }
}