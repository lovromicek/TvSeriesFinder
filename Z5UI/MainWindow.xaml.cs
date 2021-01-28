using System;
using System.Collections;
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
            var series = WebApi.GetShowListFromApi(name);
            ShowsList.ItemsSource = series;
            ShowsList.Items.Refresh();        
        }
        private void ShowsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var shows = ShowsList.SelectedItem as TvSeriesTitles;
            SeasonsList.ItemsSource = SeasonApi.GetSeasonListFromApi(shows.show.id);
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
            var episodes = EpisodeApi.GetEpisodeListFromApi(season.id);
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
            string summary = episode.summary;
            if (episode.summary != null)
            {
                summary = summary.Replace("<p>", "");
                summary = summary.Replace("</p>", "");
                summary = summary.Replace("<b>", "");
                summary = summary.Replace("</b>", "");
                summary = summary.Replace("<i>", "");
            }
            if (summary == null)
                summary = "No summary found for this episode.";
            MessageBoxResult result = MessageBox.Show(summary, "Episode info");
        }
        private void SortAscending_Click(object sender, RoutedEventArgs e)
        {
            var list = EpisodesList.Items.Cast<Episode>().OrderBy(a => a.name).ToList();
            EpisodesList.ItemsSource = list;
            EpisodesList.Items.Refresh();
        }
        private void SortDescending_Click(object sender, RoutedEventArgs e)
        {
            var list = EpisodesList.Items.Cast<Episode>().OrderByDescending(a => a.name).ToList();
            EpisodesList.ItemsSource = list;
            EpisodesList.Items.Refresh();
        }
        private void SortByEpisodeNumberAscending_Click(object sender, RoutedEventArgs e)
        {
            var list = EpisodesList.Items.Cast<Episode>().OrderBy(a => a.number).ToList();
            EpisodesList.ItemsSource = list;
            EpisodesList.Items.Refresh();
        }
        private void SortByEpisodeNumberDescending_Click(object sender, RoutedEventArgs e)
        {
            var list = EpisodesList.Items.Cast<Episode>().OrderByDescending(a => a.number).ToList();
            EpisodesList.ItemsSource = list;
            EpisodesList.Items.Refresh();
        }
    }
}
