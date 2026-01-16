using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using WindowResizer.Models;

namespace WindowResizer.Services
{
    public sealed class SettingsStore
    {
        private readonly string _path;

        public SettingsStore(string appName = "WindowResizer")
        {
            var dir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                appName);

            Directory.CreateDirectory(dir);
            _path = Path.Combine(dir, "resolutions.json");
        }

        public List<ResolutionOption> LoadCustom()
        {
            try
            {
                if (!File.Exists(_path)) return new List<ResolutionOption>();
                var json = File.ReadAllText(_path);
                var items = JsonSerializer.Deserialize<List<ResolutionOption>>(json);
                return items ?? new List<ResolutionOption>();
            }
            catch
            {
                return new List<ResolutionOption>();
            }
        }

        public void SaveCustom(IEnumerable<ResolutionOption> custom)
        {
            var json = JsonSerializer.Serialize(custom, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, json);
        }
    }
}
