using System;
using System.IO;
using System.Text.Json;

namespace WindowResizer.Services
{
    public sealed class PreferencesStore
    {
        private readonly string _path;

        private sealed class Prefs { public string? Language { get; set; } }

        public PreferencesStore(string appName = "WindowResizer")
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appName);
            Directory.CreateDirectory(dir);
            _path = Path.Combine(dir, "prefs.json");
        }

        public string? LoadLanguage()
        {
            try
            {
                if (!File.Exists(_path)) return null;
                var json = File.ReadAllText(_path);
                return JsonSerializer.Deserialize<Prefs>(json)?.Language;
            }
            catch { return null; }
        }

        public void SaveLanguage(string code)
        {
            var json = JsonSerializer.Serialize(new Prefs { Language = code }, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_path, json);
        }
    }
}
