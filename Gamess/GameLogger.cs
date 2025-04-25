using System;
using System.Diagnostics;
using System.IO;

namespace Gamess
{
    public class GameLogger
    {
        private static bool isInitialized = false;

        public static void Initialize(string output, string level)
        {
            if (isInitialized) return;

            Trace.Listeners.Clear();

            // Add console listener if needed
            if (output.Equals("Console", StringComparison.OrdinalIgnoreCase) || output.Equals("Both", StringComparison.OrdinalIgnoreCase))
                Trace.Listeners.Add(new ConsoleTraceListener());

            // Add file listener if needed
            if (output.Equals("File", StringComparison.OrdinalIgnoreCase) || output.Equals("Both", StringComparison.OrdinalIgnoreCase))
            {
                string logFile = $"log_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                Trace.Listeners.Add(new TextWriterTraceListener(File.CreateText(logFile)));
            }

            // Ensure logs are flushed immediately
            Trace.AutoFlush = true;

            // Set the desired log level
            SetLogLevel(level);

            isInitialized = true;
        }

        private static void SetLogLevel(string level)
        {
            level = level?.ToLower() ?? "verbose"; // Default to "verbose"

            // Configure the TraceLevel based on the provided level
            switch (level)
            {
                case "error":
                    TraceSwitch.Level = TraceLevel.Error;
                    break;
                case "warning":
                    TraceSwitch.Level = TraceLevel.Warning;
                    break;
                case "info":
                    TraceSwitch.Level = TraceLevel.Info;
                    break;
                case "verbose":
                default:
                    TraceSwitch.Level = TraceLevel.Verbose;
                    break;
            }
        }

        public static void Log(string message, TraceLevel level = TraceLevel.Info)
        {
            if ((int)TraceSwitch.Level >= (int)level)
                Trace.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{level}] {message}");
        }

        public static TraceSwitch TraceSwitch { get; private set; } = new TraceSwitch("GameTrace", "Trace settings for the game");
    }
}
