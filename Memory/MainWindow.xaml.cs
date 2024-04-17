using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace memory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Carte1;
        public int Carte2;
        public int Score;
        public int Chrono;
        private DispatcherTimer timerCartes = new DispatcherTimer();
        private DispatcherTimer timerPartie = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            timerCartes.Interval = TimeSpan.FromMilliseconds(400);
            timerCartes.Tick += TimerCartes_Tick;
            timerPartie.Interval = TimeSpan.FromSeconds(1);
            timerPartie.Tick += TimerPartie_Tick;
        }

        private void TimerPartie_Tick(object sender, EventArgs e) { 

            Chrono -= 1;
            LabelTemps.Content = Chrono;
            if (Chrono == 0)
            {
                timerPartie.Stop();
                MessageBox.Show("PERDU");
            }
        }


        private void ButtonNouvellePartie_Click(object sender, RoutedEventArgs e)
        {
            int Nb;
            Random listeAlea = new Random();
            List<int> Tirage1 = new List<int>();
            List<int> Tirage2 = new List<int>();
            Carte1 = -1;
            Carte2 = -1;
            Score = 0;
            for (int i = 0; i < 24; i++)
            {
                ((Image)WrapPanelGrilleCartes.Children[i]).Source = new BitmapImage(new Uri("/Resources/Vide.jpg", UriKind.Relative));
                Nb = listeAlea.Next(1, 13);
                while(Tirage1.Contains(Nb) && Tirage2.Contains(Nb))
                {
                    Nb = listeAlea.Next(1, 13);
                }
                if(!(Tirage1.Contains(Nb)))
                {
                    Tirage1.Add(Nb);
                }
                else
                {
                    Tirage2.Add(Nb);
                }
                ((Image)WrapPanelGrilleCartes.Children[i]).Tag = Nb;
            }
            WrapPanelGrilleCartes.IsEnabled = true;
            MessageBox.Show("Bonne Chance");
            Chrono = 60;
            timerPartie.Start();
            LabelTemps.Content = Chrono;
        }



        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image MonImage = (Image)sender;
            int indice = WrapPanelGrilleCartes.Children.IndexOf(MonImage);
            String Tag = ((Image)WrapPanelGrilleCartes.Children[indice]).Tag.ToString();
            switch (Tag)
            {
                case "1" : ((Image)WrapPanelGrilleCartes.Children[indice]).Source = new BitmapImage(new Uri("/Resources/Abeille.jpg", UriKind.Relative)); break;
                case "2": ((Image)WrapPanelGrilleCartes.Children[indice]).Source = new BitmapImage(new Uri("/Resources/Baleine.jpg", UriKind.Relative)); break;
                case "3": ((Image)WrapPanelGrilleCartes.Children[indice]).Source = new BitmapImage(new Uri("/Resources/Chien.jpg", UriKind.Relative)); break;
                case "4": ((Image)WrapPanelGrilleCartes.Children[indice]).Source = new BitmapImage(new Uri("/Resources/Cochon.jpg", UriKind.Relative)); break;
                case "5": ((Image)WrapPanelGrilleCartes.Children[indice]).Source = new BitmapImage(new Uri("/Resources/Elephant.jpg", UriKind.Relative)); break;
                case "6": ((Image)WrapPanelGrilleCartes.Children[indice]).Source = new BitmapImage(new Uri("/Resources/Hippo.jpg", UriKind.Relative)); break;
                case "7": ((Image)WrapPanelGrilleCartes.Children[indice]).Source = new BitmapImage(new Uri("/Resources/Koala.jpg", UriKind.Relative)); break;
                case "8": ((Image)WrapPanelGrilleCartes.Children[indice]).Source = new BitmapImage(new Uri("/Resources/Lion.jpg", UriKind.Relative)); break;
                case "9": ((Image)WrapPanelGrilleCartes.Children[indice]).Source = new BitmapImage(new Uri("/Resources/Monton.jpg", UriKind.Relative)); break;
                case "10": ((Image)WrapPanelGrilleCartes.Children[indice]).Source = new BitmapImage(new Uri("/Resources/Ours.jpg", UriKind.Relative)); break;
                case "11": ((Image)WrapPanelGrilleCartes.Children[indice]).Source = new BitmapImage(new Uri("/Resources/Panda.jpg", UriKind.Relative)); break;
                case "12": ((Image)WrapPanelGrilleCartes.Children[indice]).Source = new BitmapImage(new Uri("/Resources/Vache.jpg", UriKind.Relative)); break;
            }

            if (Carte1 == -1)
            {
                Carte1 = indice;
            }
            else
            {
                Carte2 = indice;
                timerCartes.Start();
            }
        }

        private void TimerCartes_Tick(object sender, EventArgs e)
        {
            timerCartes.Stop();
            if (((Image)WrapPanelGrilleCartes.Children[Carte1]).Tag.ToString() != ((Image)WrapPanelGrilleCartes.Children[Carte2]).Tag.ToString())
            {
                ((Image)WrapPanelGrilleCartes.Children[Carte1]).Source = new BitmapImage(new Uri("/Resources/Vide.jpg", UriKind.Relative));
                ((Image)WrapPanelGrilleCartes.Children[Carte2]).Source = new BitmapImage(new Uri("/Resources/Vide.jpg", UriKind.Relative));
            }
            else
            {
                ((Image)WrapPanelGrilleCartes.Children[Carte2]).IsEnabled = false;
                ((Image)WrapPanelGrilleCartes.Children[Carte1]).IsEnabled = false;
                Score += 1;
            }
            Carte1 = -1;
            Carte2 = -1;
            if (Score == 12)
            {
                timerPartie.Stop();
                MessageBox.Show("Victoire");
            }
        }

        private void ButtonQuitter_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
