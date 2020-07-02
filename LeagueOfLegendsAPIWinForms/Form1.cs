using LeagueOfLegendsAPIWinForms.Models;
using LeagueOfLegendsLibrary;
using Squirrel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public MainForm()
        {
            InitializeComponent();
            this.SummonerNameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CheckEnterKeyPress);
            ReagionComboBox.DataSource = Enum.GetValues(typeof(regions));

            SummonerNameBox.Select();

            CheckForUpdates();

        }

        /// <summary>
        /// in progress
        /// </summary>
        /// <returns></returns>
        private async Task CheckForUpdates()
        {

            using (var manager = new UpdateManager(@"D:\Temp\Release"))
            {
                await manager.UpdateApp();
            }
        }
        private void SearchSummoner_Click(object sender, EventArgs e)
        {
            showStats();
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
                showStats();
            }
        }

        private void showStats()
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
        private void showStats(string name)
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
            showStats("Shirtzoo");
        }

        private void AthiarLabel_Click(object sender, EventArgs e)
        {
            showStats("Raihta");

        }

        private void hkmatLabel_Click(object sender, EventArgs e)
        {
            showStats("hkoo94");
        }

        private void sizarLabel_Click(object sender, EventArgs e)
        {
            showStats("Quzzelkort");
        }

        private void nawziiiLabel_Click(object sender, EventArgs e)
        {
            showStats("Thekurdishwarrio");

        }

        private void fouadLabel_Click(object sender, EventArgs e)
        {
            showStats("fouadiii");
        }

        private void zkayLabel_Click(object sender, EventArgs e)
        {
            showStats("ZeeKai");

        }

        private void Ejlabel_Click(object sender, EventArgs e)
        {
            showStats("ZeeKai");
        }
    }
}
