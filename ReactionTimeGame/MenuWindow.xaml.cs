using System.Windows;

namespace ReactionTimeGame
{
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            var gameWindow = new MainWindow(this);
            gameWindow.Show();
            Hide();
        }

        private void Scoreboard_Click(object sender, RoutedEventArgs e)
        {
            var scoreboard = new ScoreBoardWindow();
            scoreboard.Owner = this;
            scoreboard.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
