using LeagueOfLegendsAPIWinForms.Models;
using LeagueOfLegendsLibrary;
using Squirrel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LeagueOfLegendsLibrary.Models.Regions;

namespace LeagueOfLegendsAPIWinForms
{
    public partial class MainForm : Form
    {
        UpdateManager manager;
        public MainForm()
        {
            InitializeComponent();
            this.SummonerNameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CheckEnterKeyPress);
            ReagionComboBox.DataSource = Enum.GetValues(typeof(regions));

            SummonerNameBox.Select();
            AddVersionNumber();
            UpdateApp();

        }

        public void AddVersionNumber()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versioninfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            this.Text += $" V.{versioninfo.FileVersion }";
            VersionNumber.Text = $" Shiroo V.{versioninfo.FileVersion }";
        }

        /// <summary>
        /// in progress
        /// </summary>
        /// <returns></returns>
        private async Task UpdateApp()
        {
            manager = await UpdateManager.GitHubUpdateManager(@"https://github.com/Shirzad93/LeagueOfLegendsAPIWinForms");
            await manager.UpdateApp();
        }


        private void SearchSummoner_Click(object sender, EventArgs e)
        {
            ShowStats();
        }

        /// <summary>
        /// Enter key when pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckEnterKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowStats();
            }
        }

        private void ShowStats()
        {
            var summonerName = SummonerNameBox.Text;
            var region = ReagionComboBox.Text;

            using (SummonerProfile sp = new SummonerProfile())
            {
                sp.region = region;
                sp.summonerName = summonerName;

                sp.ShowDialog();
            }
        }

        /// <summary>
        /// show stats
        /// </summary>
        /// <param name="name"></param>
        private void ShowStats(string name)
        {
            var summonerName = name;
            var region = ReagionComboBox.Text;

            using (SummonerProfile sp = new SummonerProfile())
            {
                sp.region = region;
                sp.summonerName = summonerName;

                sp.ShowDialog();
            }
        }
        private void ShirooLabel_Click(object sender, EventArgs e)
        {
            ShowStats("Shîroo");
        }

        private void AthiarLabel_Click(object sender, EventArgs e)
        {
            ShowStats("raihta");
        }

        private void HkmatLabel_Click(object sender, EventArgs e)
        {
            ShowStats("hkoo94");
        }

        private void SizarLabel_Click(object sender, EventArgs e)
        {
            ShowStats("Quzzelkort");
        }

        private void NawziiiLabel_Click(object sender, EventArgs e)
        {
            ShowStats("Thekurdishwarrio");
        }

        private void FouadLabel_Click(object sender, EventArgs e)
        {
            ShowStats("fouadiii");
        }

        private void ZkayLabel_Click(object sender, EventArgs e)
        {
            ShowStats("Carl XVÏ Gustaf");
        }

        private void Ejlabel_Click(object sender, EventArgs e)
        {
            ShowStats("bahlek");
        }

    }
}
