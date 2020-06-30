using LeagueOfLegendsAPIWinForms.Models;
using LeagueOfLegendsLibrary;
using LeagueOfLegendsLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOfLegendsAPIWinForms
{
    public class LolApiProcessor
    {

        public static string key = "RGAPI-6cab67e4-e56d-4a61-9694-4210b038938c";
        public static async Task<SummonerDTO> LoadSummoner(string region, string SummonerName)
        {
            string url = "";

            if (!string.IsNullOrEmpty(SummonerName))
            {
                url = $"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/by-name/{SummonerName}?api_key={key}";
            }
            else
            {
                url = "https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/shirtzoo?api_key={key}";
            }

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    SummonerDTO summoner = await response.Content.ReadAsAsync<SummonerDTO>();

                    return summoner;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        public static async Task<List<ChampionMasteryDTO>> LoadChampion(string region, string SummonerID)
        {
            string url = "";

            if (!string.IsNullOrEmpty(SummonerID))
            {
                url = $"https://{region}.api.riotgames.com/lol/champion-mastery/v4/champion-masteries/by-summoner/{SummonerID}?api_key={key}";
            }
            else
            {
                url = $"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/shirtzoo?api_key={key}";
            }

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<ChampionMasteryDTO> championData = await response.Content.ReadAsAsync<List<ChampionMasteryDTO>>();

                    return championData;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<Rootobject> GetDetailedChampInfo()
        {
            string url = "http://ddragon.leagueoflegends.com/cdn/10.3.1/data/en_US/champion.json";

            string response = await ApiHelper.ApiClient.GetStringAsync(url);

            Rootobject championData = JsonConvert.DeserializeObject<Rootobject>(response);


            return championData;
        }

        public static async Task<List<LeagueEntryDTO>> GetChampionRank(string region, string SummonerID)
        {
            string url = "";

            if (!string.IsNullOrEmpty(SummonerID))
            {
                url = $"https://{region}.api.riotgames.com/lol/league/v4/entries/by-summoner/{SummonerID}?api_key={key}";
            }
            else
            {
                url = $"NOPE";
            }

            string response = await ApiHelper.ApiClient.GetStringAsync(url);

            var championRanks = JsonConvert.DeserializeObject<List<LeagueEntryDTO>>(response);

            return championRanks;

        }

        public static async Task<SummonerScore> GetSummonerScore(string SummonerID)
        {
            string url = "";

            if (!string.IsNullOrEmpty(SummonerID))
            {
                url = $"https://euw1.api.riotgames.com/lol/champion-mastery/v4/scores/by-summoner/{SummonerID}?api_key={key}";
            }
            else
            {
                url = $"NOPE";
            }


            string response = await ApiHelper.ApiClient.GetStringAsync(url);
            SummonerScore score = new SummonerScore();

            score.SummonerScores = response;

            return score;
        }



    }
}
