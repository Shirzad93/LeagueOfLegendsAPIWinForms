using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOfLegendsLibrary.Models
{
    public class ChampionMasteryDTO
    {
        public Boolean ChestGranted { get; set; }
        public int ChampionLevel { get; set; }
        public int ChampionPoints { get; set; }
        public long ChampionId { get; set; }
    }
}
