using LeagueOfLegendsAPIWinForms.Models;
using LeagueOfLegendsLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueOfLegendsAPIWinForms
{
    public partial class SummonerProfile : Form
    {
        public string region;
        public string summonerName;
        
        public int chestsAvailableCounter = 1;
        public int chestsNotAvailableCounter = 1;

        int GetSummonerIconWidth = 100;
        int GetSummonerIconHeight = 100;
        
        int GetChampionIconWidth = 40;
        int GetChampionIconHeight = 40;

        List<string> listOfChampionNames = new List<string>();
        
        int TierIconWidth = 120;
        int TierIconHeight = 120;
        PictureBox picture = new PictureBox();
        public SummonerProfile()
        {
            InitializeComponent();
            ApiHelper.InitializeClinet();
        }


        private async void SummonerProfile_Load(object sender, EventArgs e)
        {
            picture = new PictureBox()
            {
                Name = "pictureBox",

            };
            this.Controls.Add(picture);

            //Summoner Info
            var summonerInfo = await LolApiProcessor.LoadSummoner(region, summonerName);
            var summonerID = summonerInfo.ID;
            var icon = summonerInfo.ProfileIconId;

            IdHolder.Text = summonerID;
            ProfileNameHolder.Text = summonerInfo.Name;
            LevelHolder.Text = summonerInfo.SummonerLevel.ToString();
            
            //All info about the champion
            Rootobject detailedChampionInfo = await LolApiProcessor.GetDetailedChampInfo();
            var testDetailedChampionInfo = detailedChampionInfo.Data.Values.OrderBy(x => x.Key).ToList();

            
            GetSummonerIcon(icon);

            //All info about the summoner by id
            var championRank  = await LolApiProcessor.GetChampionRank(region, summonerID);

            foreach (var item in championRank)
            {
                if (item.QueueType == "RANKED_SOLO_5x5")
                {
                    LeaguePoints.Text = item.LeaguePoints.ToString() + " LP";
                    GetSoloRankedIcons(item.Rank, item.Tier, item.QueueType);
                    WinsHolder.Text = item.Wins.ToString() + " W";
                    LossesHolder.Text = item.Losses.ToString() + " L";
                }
                else //flex
                {
                    FlexLeaguePoints.Text = item.LeaguePoints.ToString() + " LP";

                    GetSoloRankedIcons(item.Rank, item.Tier, item.QueueType);
                    flexWinHolder.Text = item.Wins.ToString() + " W";
                    FlexLossesHolder.Text = item.Losses.ToString() + " L";
                }
            }
            
            //summoner score
            var summonerScore = await LolApiProcessor.GetSummonerScore(summonerID);
            ScoreHolder.Text = summonerScore.SummonerScores;

            //Information about the champion for the summoner/player
            var championInfo = await LolApiProcessor.LoadChampion(region, summonerID);
            long ChampionID = 0;

            foreach (var champ in championInfo)
            {

                ChampionID = champ.ChampionId;
                var championName = testDetailedChampionInfo.Where(x => x.Key == champ.ChampionId).FirstOrDefault().Id;
                //await GetChampionImg(championName);

                listOfChampionNames.Add(championName);

                if (champ.ChestGranted == true)
                {
                    ChesNotAvailableListBox.Items.Add(chestsNotAvailableCounter++ + ". " + " (Lvl: " + champ.ChampionLevel.ToString() + ") - " + championName);
                    //ChesNotAvailableListBox.Items.Add("______________________________");
                }
                else /*if(champ.ChampionPoints > 0)*/
                {
                    ChestAvailableListBox.Items.Add(chestsAvailableCounter++ + ". " + " (Lvl: " + champ.ChampionLevel.ToString() + ") - " + championName);
                    //ChestAvailableListBox.Items.Add("______________________________");
                }
            }
            //IN WORK!
            //await GetChampionImg(listOfChampionNames);
        }

        /// <summary>
        /// The icon for the summoner/user
        /// </summary>
        /// <param name="icon"></param>
        private void GetSummonerIcon(long icon)
        {
            string path = $"https://opgg-static.akamaized.net/images/profile_icons/profileIcon{icon}.jpg";

            WebRequest request = WebRequest.Create(path);

            using (var response = request.GetResponse())
            {
                using (var str = response.GetResponseStream())
                {
                    IconBox.Image = ReSize(Bitmap.FromStream(str), GetSummonerIconWidth, GetSummonerIconHeight);
                }
            }
        }

        /// <summary>
        /// Get Champion Images
        /// </summary>
        /// <param name="listOfChampionNames"></param>
        /// <returns></returns>
        private async Task GetChampionImg(List<string> listOfChampionNames)
        {
            List<string> paths = new List<string>();
            foreach (var name in listOfChampionNames)
            {
                paths.Add($"http://ddragon.leagueoflegends.com/cdn/10.13.1/img/champion/{name}.png");
            }

            foreach (var path in paths)
            {
                WebRequest request = WebRequest.Create(path);

                using (var response = await Task.Run(() => request.GetResponse()))
                {
                    using (var str = response.GetResponseStream())
                    {
                        picture.Image = ReSize(Bitmap.FromStream(str), GetChampionIconWidth, GetChampionIconHeight);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the icons for the ranks
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="tier"></param>
        /// <param name="QueueType"></param>
        private void GetSoloRankedIcons(string rank, string tier, string QueueType)
        {
            var tierLower = tier.ToLower();
            //var rankLevel = rank;

            switch (rank)
            {
                case "I":
                    rank = "1";
                    break;
                case "II":
                    rank = "2";
                    break;
                case "III":
                    rank = "3";
                    break;
                default:
                    rank = "4";
                    break;
            }

            string path = $"https://opgg-static.akamaized.net/images/medals/{tierLower}_{rank}.png";
            WebRequest request = WebRequest.Create(path);

            using (var response = request.GetResponse())
            {
                using (var str = response.GetResponseStream())
                {
                    if (QueueType == "RANKED_SOLO_5x5")
                    {
                        SOLOpictureBox.Image = ReSize(Bitmap.FromStream(str), TierIconWidth, TierIconHeight);
                        SoloRankPositionText.Text = tier + " " + rank;
                    }
                    else //flex
                    {
                        FLEXpictureBox.Image = ReSize(Bitmap.FromStream(str), TierIconWidth, TierIconHeight);
                        flexRankPositiontext.Text = tier + " " + rank;
                    }
                }
            }

        }

        /// <summary>
        /// Resize The images
        /// </summary>
        /// <param name="img"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        Image ReSize(Image img, int Width, int Height)
        {
            Bitmap newSize = new Bitmap(img, Width, Height);
            Graphics g = Graphics.FromImage(newSize);

            return newSize;
        }
    }
}