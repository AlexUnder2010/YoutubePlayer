using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCLdbpm90zKV6xS6ePe3CC9zUELNFFUfUk",
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
                searchListRequest.Q = textBox1.Text;
                searchListRequest.MaxResults = 50;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();

            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                        listBox1.Items.Add(String.Format(searchResult.Snippet.Title) + Environment.NewLine);
                        listBox4.Items.Add(String.Format(searchResult.Id.VideoId) + Environment.NewLine);
                        break;

                    case "youtube#channel":
                        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        listBox2.Items.Add(String.Format(searchResult.Snippet.Title) + Environment.NewLine);
                        break;

                    case "youtube#playlist":
                        playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                        listBox3.Items.Add(String.Format(searchResult.Snippet.Title) + Environment.NewLine);
                        break;
                }
             
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            string temp_text = listBox1.GetItemText(listBox1.Items[i]);
            listBox1.Items[i] = listBox4.Items[i]; 
            string path = @"http://www.youtube.com/v/" + listBox4.GetItemText(listBox4.Items[i]) + "?autoplay=1";
            axShockwaveFlash1.LoadMovie(0, path);
            listBox1.Items[i] = temp_text;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                int i = listBox1.SelectedIndex;
                string temp_text = listBox1.GetItemText(listBox1.Items[i]);
                listBox1.Items[i] = listBox4.Items[i];
                string path = @"http://www.youtube.com/v/" + listBox4.GetItemText(listBox4.Items[i]) + "?autoplay=1";
                axShockwaveFlash1.LoadMovie(0, path);
                listBox1.Items[i] = temp_text;
            }
        }

    }
}
