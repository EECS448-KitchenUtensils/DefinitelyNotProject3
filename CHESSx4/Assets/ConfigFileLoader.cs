using System;
using System.IO;
using UnityEngine;

namespace AssemblyCSharp
{
    /// <summary>
    /// Quick and dirty way to change the server address on the fly
    /// </summary>
    public class ConfigFileLoader
    {
        public ConfigFileLoader(string file = "Assets/config.txt")
        {
            _fileName = file;
            Server = "ws://127.0.0.1:1337";
        }

        public void ReadFile()
        {
            if (File.Exists(_fileName))
            {
                string[] lines = new string[0];
                try
                {
                    lines = File.ReadAllLines(_fileName);
                }
                catch(IOException e)
                {
                    Debug.LogError($"Could not load config file {_fileName}");
                }
                if (lines.Length >= 1)
                {
                    Server = lines[0];
                    Debug.Log($"Set server address to {Server}");
                }
                else
                {
                    Debug.LogError($"{_fileName} was empty!");
                }
            }
            else
            {
                Debug.LogError($"Failed to open config file: {_fileName}");
            }
        }

        private readonly string _fileName;

        public string Server { get; private set; }
    }
}

