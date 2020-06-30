using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOfLegendsAPIWinForms.Models
{
    public class SummonerDTO
    {
        public string ID { get; set; }
        public int ProfileIconId { get; set; }
        public string Name { get; set; }
        public long SummonerLevel { get; set; }
        public string AccountId { get; set; }


    }
}
