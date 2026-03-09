using System.Collections.Generic;
using System.Windows;

namespace ReactionTimeGame
{
    public partial class ScoreBoardWindow : Window
    {
        public ScoreBoardWindow()
        {
            InitializeComponent();
            LoadScores();
        }

        private void LoadScores()
        {
            var scores = ScoreManager.Scores;

            if (scores.Count == 0)
            {
                ScoreGrid.Visibility = Visibility.Collapsed;
                EmptyMessage.Visibility = Visibility.Visible;
                return;
            }

            ScoreGrid.Visibility = Visibility.Visible;
            EmptyMessage.Visibility = Visibility.Collapsed;

            var rows = new List<ScoreRow>();
            for (int i = 0; i < scores.Count; i++)
            {
                rows.Add(new ScoreRow
                {
                    Rank = i + 1,
                    ReactionTimeDisplay = $"{scores[i].ReactionTimeSeconds:F3} s",
                    DateDisplay = scores[i].Date.ToString("yyyy.MM.dd HH:mm:ss")
                });
            }

            ScoreGrid.ItemsSource = rows;
        }

        private void ClearScores_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Biztosan törölni szeretnéd az összes eredményt?",
                "Megerősítés",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                ScoreManager.ClearScores();
                LoadScores();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class ScoreRow
    {
        public int Rank { get; set; }
        public string ReactionTimeDisplay { get; set; } = string.Empty;
        public string DateDisplay { get; set; } = string.Empty;
    }
}
