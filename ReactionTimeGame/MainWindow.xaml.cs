using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ReactionTimeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer ReactionTimer;  // Időzítő a várakozási időhöz
        private DispatcherTimer StopwatchTimer; // Stopper a reakcióidő méréséhez
        private DateTime ReactionStartTime;     // A gomb megjelenési időpontja
        private bool GameActive = false;        // A játék állapota

        public MainWindow()
        {
            InitializeComponent();

            // Timer inicializálása
            ReactionTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            // Az időzítő eseménykezelője
            ReactionTimer.Tick += ReactionTimer_Tick;


            // Stopper inicializálása
            StopwatchTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            // Az időzítő eseménykezelője
            StopwatchTimer.Tick += StopwatchTimer_Tick;
        }

        // Új játék indítása
        private void StartGame(object sender, RoutedEventArgs e)
        {
            if (GameActive) return; // Ha már fut a játék, ne lehessen újraindítani

            GameActive = true;
            ReactionButton.Visibility = Visibility.Hidden;
            InstructionText.Text = "Várj a gombra...";

            // Véletlenszerű késleltetés beállítása (1-5 másodperc között)
            Random rnd = new Random();
            int delay = rnd.Next(1000, 5000); // 1000-5000 ms
            // Időzítő indítása
            ReactionTimer.Interval = TimeSpan.FromMilliseconds(delay);
            ReactionTimer.Start();
        }

        // Ha letelt a véletlenszerű késleltetés, megjelenik a gomb
        private void ReactionTimer_Tick(object sender, EventArgs e)
        {
            ReactionTimer.Stop();
            ReactionStartTime = DateTime.Now; // Megjegyezzük a kezdő időpontot
            ReactionButton.Visibility = Visibility.Visible;
            InstructionText.Text = "KATTINTS MOST!";

            // Stopper indítása
            StopwatchTimer.Start();
        }

        // Reakciógomb megnyomása
        private void ReactionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!GameActive) return;

            GameActive = false;
            StopwatchTimer.Stop();

            // Reakcióidő kiszámítása
            TimeSpan reactionTime = DateTime.Now - ReactionStartTime;

            // Másodperc formátumban megjelenítés
            //formázás magyarázat --> F3: 3 tizedesjegyig, F0: egész szám, s -> másodperc
            //TotalSeconds: a TimeSpan osztály egyik tulajdonsága, ami a teljes időtartamot másodpercekben adja vissza
            //TotalSeconds:F3 -> a teljes időtartamot másodpercekben adja vissza, 3 tizedesjegy pontossággal
            InstructionText.Text = $"Reakcióidő: {reactionTime.TotalSeconds:F3} s";
            ReactionButton.Visibility = Visibility.Hidden;
        }

        // Stopper folyamatos frissítése (ezredmásodperc pontossággal)
        private void StopwatchTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapseddd = DateTime.Now - ReactionStartTime;
            //TotalMilliseconds: a TimeSpan osztály egyik tulajdonsága, ami a teljes időtartamot ezredmásodpercekben adja vissza
            //TotalMilliseconds:F0 -> a teljes időtartamot ezredmásodpercekben adja vissza, egész számra kerekítve
            InstructionText.Text = $"Idő: {elapsed.TotalMilliseconds:F0} ms";
        }
    }
}