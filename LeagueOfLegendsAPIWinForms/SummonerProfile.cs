using LeagueOfLegendsAPIWinForms.Models;
using LeagueOfLegendsLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        #region Icon

        int GetSummonerIconWidth = 100;
        int GetSummonerIconHeight = 100;

        int GetChampionIconWidth = 40;
        int GetChampionIconHeight = 40;

        #endregion Icon

        #region Lists

        List<string> listOfChampionNames = new List<string>();
        List<string> listOfChampionNamesAvailable = new List<string>();
        List<string> listOfChampionNamesNotAvailable = new List<string>();

        
        List<string> listOfTop_Laner_Available = new List<string>();
        List<string> listOfTop_Laner_NotAvailable = new List<string>();

        List<string> listOfTJungle_Available = new List<string>();
        List<string> listOfTJungle_NotAvailable = new List<string>();

        List<string> listOfMid_Laner_Available = new List<string>();
        List<string> listOfMid_Laner_NotAvailable = new List<string>();

        List<string> listOfSupport_Laner_Available = new List<string>();
        List<string> listOfSupport_Laner_NotAvailable = new List<string>();

        List<string> listOfADC_Available = new List<string>();
        List<string> listOfADC_NotAvailable = new List<string>();

        #endregion Lists

        
        PictureBox picture = new PictureBox();
        public SummonerProfile()
        {
            InitializeComponent();
            ApiHelper.InitializeClinet();
            SearchChampionTxtbox.Select();
        }
        
        private void SummonerProfile_Load(object sender, EventArgs e)
        {
            picture = new PictureBox()
            {
                Name = "pictureBox",

            };
            this.Controls.Add(picture);

            //Information about the champion for the summoner/player
            AddChampionsToList();
            SearchChampionTxtbox.Focus();
        }

        /// <summary>
        /// AddChampionsToList
        /// </summary>
        /// <param name="AllChampsSelected"></param>
        public async void AddChampionsToList()
        {
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
            DetailedSummonerDetails(summonerID);

            //summoner score
            var summonerScore = await LolApiProcessor.GetSummonerScore(summonerID);
            ScoreHolder.Text = summonerScore.SummonerScores;

            var championInfo = await LolApiProcessor.LoadChampion(region, summonerID);
            long ChampionID = 0;
            //bool champexist = false;

            var ChampionTagInfo = await LolApiProcessor.GetChampionTags();
            
            List<ChampionData> championData = new List<ChampionData>();

            foreach (var item in ChampionTagInfo)
            {
                foreach (var TagInfo in item.Data)
                {
                    championData.Add(new ChampionData(TagInfo.Value.Key, TagInfo.Value.Tags));
                }
            }

            foreach (var champ in championInfo)
            {
                ChampionID = champ.ChampionId;
                string championName = testDetailedChampionInfo.Where(x => x.Key == ChampionID).FirstOrDefault().Id;

                listOfChampionNames.Add(championName);
                //champexist = championData.Any(x => x.Key == ChampionID);

                if (champ.ChestGranted == true)
                {
                    string champinfoNotAvailable = chestsNotAvailableCounter++ + ". " + " (Lvl: " + champ.ChampionLevel.ToString() + ") - " + championName;
                    
                        foreach (var tag in championData)
                        {
                            long champtagID = (long)tag.Key;
                            if (champtagID == ChampionID)
                            {
                                foreach (var lane in tag.Tags)
                                {
                                    switch (lane)
                                    {
                                        case "Top_Laner":
                                            listOfTop_Laner_NotAvailable.Add(champinfoNotAvailable);
                                            break;
                                        case "Jungle":
                                            listOfTJungle_NotAvailable.Add(champinfoNotAvailable);
                                            break;

                                        case "Mid_Laner":
                                            listOfMid_Laner_NotAvailable.Add(champinfoNotAvailable);
                                            break;

                                        case "Support_Laner":
                                            listOfSupport_Laner_NotAvailable.Add(champinfoNotAvailable);
                                            break;
                                        case "ADC":
                                            listOfADC_NotAvailable.Add(champinfoNotAvailable);
                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    LaneCover.Hide();
                    ChesNotAvailableListBox.Items.Add(champinfoNotAvailable);
                    listOfChampionNamesNotAvailable.Add(champinfoNotAvailable);
                    
                }
                else 
                {
                    string champinfoAvailable = chestsAvailableCounter++ + ". " + " (Lvl: " + champ.ChampionLevel.ToString() + ") - " + championName;
                    
                    //if (champexist == true)
                    //{
                        foreach (var tag in championData)
                        {
                            long champtagID = (long)tag.Key;
                            if (champtagID == ChampionID)
                            {
                                foreach (var lane in tag.Tags)
                                {
                                    switch (lane)
                                    {
                                        case "Top_Laner":
                                            listOfTop_Laner_Available.Add(champinfoAvailable);
                                            break;
                                        case "Jungle":
                                            listOfTJungle_Available.Add(champinfoAvailable);
                                            break;

                                        case "Mid_Laner":
                                            listOfMid_Laner_Available.Add(champinfoAvailable);
                                            break;

                                        case "Support_Laner":
                                            listOfSupport_Laner_Available.Add(champinfoAvailable);
                                            break;
                                        case "ADC":
                                            listOfADC_Available.Add(champinfoAvailable);
                                            break;

                                        default:
                                            //nothing default
                                            break;
                                    }
                                }
                            }

                        }
                    //}

                    ChestAvailableListBox.Items.Add(champinfoAvailable);
                    listOfChampionNamesAvailable.Add(champinfoAvailable);
                }
                
            }

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

        private async void DetailedSummonerDetails(string summonerID)
        {
            //All info about the summoner by id
            var championRank = await LolApiProcessor.GetChampionRank(region, summonerID);
            //"RANKED_TFT_PAIRS" been added!

            foreach (var item in championRank)
            {
                if (item.QueueType == "RANKED_SOLO_5x5")
                {
                    LeaguePoints.Text = item.LeaguePoints.ToString() + " LP";
                    GetSoloRankedIcons(item.Rank, item.Tier, item.QueueType);
                    WinsHolder.Text = item.Wins.ToString() + " W";
                    LossesHolder.Text = item.Losses.ToString() + " L";
                }
                else if (item.QueueType == "RANKED_TFT_PAIRS")
                {
                    //TODO: new queue type? DO NOTHING! 
                   
                }
                else
                {
                    FlexLeaguePoints.Text = item.LeaguePoints.ToString() + " LP";

                    GetSoloRankedIcons(item.Rank, item.Tier, item.QueueType);
                    flexWinHolder.Text = item.Wins.ToString() + " W";
                    FlexLossesHolder.Text = item.Losses.ToString() + " L";
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
            int TierIconWidth = 120;
            int TierIconHeight = 120;

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

        private void SearchChampionTxtbox_TextChanged(object sender, EventArgs e)
        {
            List<string> SearchChampionNames = new List<string>();
            List<string> SearchChampionNamesNot = new List<string>();


            TextBox SearchedValue = sender as TextBox;
            string SearchedName = SearchedValue.Text.ToLower();
            foreach (var item in ChestAvailableListBox.Items)
            {
                string itemLowered = item.ToString().ToLower();
                if (itemLowered.Contains(SearchedName))
                {
                    SearchChampionNames.Add(item.ToString());
                }
            }

            foreach (var item in ChesNotAvailableListBox.Items)
            {
                string itemLowered = item.ToString().ToLower();
                if (itemLowered.Contains(SearchedName))
                {
                    SearchChampionNamesNot.Add(item.ToString());
                }
            }

            ChestAvailableListBox.DataSource = null;
            ChestAvailableListBox.Items.Clear();
            ChestAvailableListBox.DataSource = SearchChampionNames;
            ChesNotAvailableListBox.DataSource = SearchChampionNamesNot;


            SearchedValue.KeyPress += new KeyPressEventHandler(Keypressed);

        }

        public void Keypressed(Object o, KeyPressEventArgs e)
        {
            // to indicate the event is handled.
            if (e.KeyChar == (char)Keys.Back)
            {

            }
        }

        public void SearchChampionBtn_Click(object sender, EventArgs e)
        {

            ChestAvailableListBox.DataSource = listOfChampionNamesAvailable;
            ChesNotAvailableListBox.DataSource = listOfChampionNamesNotAvailable;

            SearchChampionTxtbox.Text = "";

        }

        private void All_Laner_Btn_Click(object sender, EventArgs e)
        {
            ChestAvailableListBox.DataSource = listOfChampionNamesAvailable;
            ChesNotAvailableListBox.DataSource = listOfChampionNamesNotAvailable;
        }

        private void Top_Laner_Btn_Click(object sender, EventArgs e)
        {
            ChestAvailableListBox.DataSource = listOfTop_Laner_Available;
            ChesNotAvailableListBox.DataSource = listOfTop_Laner_NotAvailable;
        }

        private void Jungle_Btn_Click(object sender, EventArgs e)
        {
            ChestAvailableListBox.DataSource = listOfTJungle_Available;
            ChesNotAvailableListBox.DataSource = listOfTJungle_NotAvailable;
        }

        private void Mid_Laner_Btn_Click(object sender, EventArgs e)
        {
            ChestAvailableListBox.DataSource = listOfMid_Laner_Available;
            ChesNotAvailableListBox.DataSource = listOfMid_Laner_NotAvailable;
        }

        private void Support_Laner_Btn_Click(object sender, EventArgs e)
        {
            ChestAvailableListBox.DataSource = listOfSupport_Laner_Available;
            ChesNotAvailableListBox.DataSource = listOfSupport_Laner_NotAvailable;
        }

        private void ADC_Btn_Click(object sender, EventArgs e)
        {
            ChestAvailableListBox.DataSource = listOfADC_Available;
            ChesNotAvailableListBox.DataSource = listOfADC_NotAvailable;
        }

        //private void ShowAllChamps_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!ShowAllChamps.ThreeState)
        //    {
        //        ShowAllChamps.ThreeState = true;
        //        AddChampionsToList();

        //    }
        //    else
        //    {
        //        ShowAllChamps.ThreeState = false;
        //        AddChampionsToList();

        //    }

        //}
    }
}