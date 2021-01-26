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
using Z5;

namespace Z5UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SearchSeries_Click(object sender, RoutedEventArgs e)
        {
            string name = TitleInput.Text;
            var series = WebApi.start_get(name);
            ShowsList.ItemsSource = series;
            ShowsList.Items.Refresh();        
        }
        private void ShowsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var shows = ShowsList.SelectedItem as TvSeriesTitles;
            SeasonsList.ItemsSource = SeasonApi.start_get(shows.show.id);
            SeasonsList.Items.Refresh();
            TBlockName.Text = $"Selected show: {shows.show.name}";
            TBlockLang.Text = $"Language: {shows.show.language}";
            TBlockGenres.Text = $"Genre(s): {string.Join(',', shows.show.genres)}";
            if (shows.show.genres.Count == 0)
                TBlockGenres.Text = "Genre(s): N/A";
            TBlockRating.Text = $"Rating: {shows.show.rating.average}";
            if (shows.show.rating.average == null)
                TBlockRating.Text = "Rating: No rating yet";
        }
        private void EpisodesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var season = SeasonsList.SelectedItem as Season;
            var episodes = EpisodeApi.start_get(season.id);
            EpisodesList.Items.Refresh();
            foreach (var episode in episodes.ToList())
            {
                if(episode.number == null)
                {
                    episodes.Remove(episode);
                }
            }
            EpisodesList.ItemsSource = episodes;
            EpisodesList.Items.Refresh();
        }
        private void ShowSummary_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var episode = EpisodesList.SelectedItem as Episode;
            string summary = episode.summary.Replace("<p>", "");
            summary = summary.Replace("</p>", "");
            if (summary.Length == 0)
                summary = "No summary found for this episode.";
            MessageBoxResult result = MessageBox.Show(summary, "Episode info");
        }
    }
}
