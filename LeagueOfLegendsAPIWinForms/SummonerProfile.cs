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

        //List<string> chestsAvailable = new List<string>();
        //List<string> chestsNotAvailable = new List<string>();

        public int chestsAvailableCounter = 1;
        public int chestsNotAvailableCounter = 1;

        int GetSummonerIconWidth = 100;
        int GetSummonerIconHeight = 100;


        int GetChampionIconWidth = 40;
        int GetChampionIconHeight = 40;

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
            var summonerInfo = await LolApiProcessor.LoadSummoner(region, summonerName);
            var summonerID = summonerInfo.ID;
            var championName = "NoName";

            var championInfo = await LolApiProcessor.LoadChampion(region, summonerID);
            long ChampionID = 0;

            Rootobject detailedChampionInfo = await LolApiProcessor.GetDetailedChampInfo();

            var testDetailedChampionInfo = detailedChampionInfo.Data.Values.OrderBy(x => x.Key).ToList();

            var icon = summonerInfo.ProfileIconId;
            GetSummonerIcon(icon);

            var summonerScore = await LolApiProcessor.GetSummonerScore(summonerID);

            var championRank = await LolApiProcessor.GetChampionRank(region, summonerID);

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
            
            ProfileNameHolder.Text = summonerInfo.Name;
            LevelHolder.Text = summonerInfo.SummonerLevel.ToString();
            IdHolder.Text = summonerInfo.ID;

            ScoreHolder.Text = summonerScore.SummonerScores;


            foreach (var champ in championInfo)
            {
                ChampionID = champ.ChampionId;

                championName = testDetailedChampionInfo.Where(x => x.Key == champ.ChampionId).FirstOrDefault().Id;
                //GetChampionImg(championName);
                
                if (champ.ChestGranted == true)
                {
                    ChesNotAvailableListBox.Items.Add(chestsNotAvailableCounter++ + ". " + " (Lvl: " + champ.ChampionLevel.ToString() + ") - " + championName);
                    //ChesNotAvailableListBox.Items.Add("______________________________");
                    //PictureBox picture = new PictureBox();
                    //picture.Location = new Point(10 + chestsNotAvailableCounter, 10);
                    //this.Controls.Add(picture);
                }
                else if (champ.ChampionPoints > 0)
                {
                    ChestAvailableListBox.Items.Add(chestsAvailableCounter++ + ". " + " (Lvl: " + champ.ChampionLevel.ToString() + ") - " + championName );
                    //ChestAvailableListBox.Items.Add("______________________________");
                    //PictureBox picture = new PictureBox();
                    //picture.Location = new Point(10 + chestsAvailableCounter, 10);
                    //this.Controls.Add(picture);
                }
            }
        }

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

        private void GetChampionImg(string name)
        {
            string path = $"http://ddragon.leagueoflegends.com/cdn/10.13.1/img/champion/{name}.png";
            
            WebRequest request = WebRequest.Create(path);

            using (var response = request.GetResponse())
            {
                using (var str = response.GetResponseStream())
                {
                    picture.Image = ReSize(Bitmap.FromStream(str), GetChampionIconWidth, GetChampionIconHeight);
                }
            }
        }

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
        Image ReSize(Image img, int Width, int Height)
        {
            Bitmap newSize = new Bitmap(img, Width, Height);
            Graphics g = Graphics.FromImage(newSize);

            return newSize;
        }
    }
}


//test