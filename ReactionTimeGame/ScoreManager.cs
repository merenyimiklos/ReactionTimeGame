using System.IO;
using System.Text.Json;

namespace ReactionTimeGame
{
    public static class ScoreManager
    {
        private static readonly string ScoreFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ReactionTimeGame", "scores.json");

        private static List<ScoreEntry> _scores = new();

        public static IReadOnlyList<ScoreEntry> Scores => _scores.AsReadOnly();

        static ScoreManager()
        {
            LoadScores();
        }

        public static void ClearScores()
        {
            _scores.Clear();
            SaveScores();
        }

        public static void AddScore(double reactionTimeSeconds)
        {
            _scores.Add(new ScoreEntry
            {
                Date = DateTime.Now,
                ReactionTimeSeconds = reactionTimeSeconds
            });
            _scores.Sort((a, b) => a.ReactionTimeSeconds.CompareTo(b.ReactionTimeSeconds));
            SaveScores();
        }

        private static void LoadScores()
        {
            try
            {
                if (File.Exists(ScoreFilePath))
                {
                    string json = File.ReadAllText(ScoreFilePath);
                    _scores = JsonSerializer.Deserialize<List<ScoreEntry>>(json) ?? new List<ScoreEntry>();
                }
            }
            catch
            {
                // Ha nem sikerül betölteni, üres listával indulunk
                _scores = new List<ScoreEntry>();
            }
        }

        private static void SaveScores()
        {
            try
            {
                string? directory = Path.GetDirectoryName(ScoreFilePath);
                if (directory != null)
                    Directory.CreateDirectory(directory);

                string json = JsonSerializer.Serialize(_scores, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ScoreFilePath, json);
            }
            catch
            {
                // Ha nem sikerül menteni, csendben folytatjuk (pl. nincs írási jog)
            }
        }
    }
}
