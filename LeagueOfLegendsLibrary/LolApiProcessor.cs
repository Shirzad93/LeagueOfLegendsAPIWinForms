using LeagueOfLegendsAPIWinForms.Models;
using LeagueOfLegendsLibrary;
using LeagueOfLegendsLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueOfLegendsAPIWinForms
{
    public class LolApiProcessor
    {

        public static string key = "RGAPI-6cab67e4-e56d-4a61-9694-4210b038938c";

        /// <summary>
        /// get summoner information based on region and summoner name
        /// </summary>
        /// <param name="region"></param>
        /// <param name="SummonerName"></param>
        /// <returns></returns>
        public static async Task<SummonerDTO> LoadSummoner(string region, string SummonerName)
        {
            string url = "";

            if (!string.IsNullOrEmpty(SummonerName))
            {
                url = $"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/by-name/{SummonerName}?api_key={key}";
            }
            else
            {
                url = $"https://euw1.api.riotgames.com/lol/summoner/v4/summoners/by-name/shirtzoo?api_key={key}";
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

        /// <summary>
        /// The champion information
        /// </summary>
        /// <param name="region"></param>
        /// <param name="SummonerID"></param>
        /// <returns></returns>
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

        /// <summary>
        /// more information about the champion
        /// </summary>
        /// <returns></returns>
        public static async Task<Rootobject> GetDetailedChampInfo()
        {
            //string url = "https://ddragon.canisback.com/11.9.1/data/en_US/champion.json";
            string url = "https://ddragon.canisback.com/latest/data/en_US/champion.json";

            string response = await ApiHelper.ApiClient.GetStringAsync(url);

            Rootobject championData = JsonConvert.DeserializeObject<Rootobject>(response);


            return championData;
        }

       /// <summary>
       /// Get the summoner/users rank
       /// </summary>
       /// <param name="region"></param>
       /// <param name="SummonerID"></param>
       /// <returns></returns>
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

        /// <summary>
        /// Gets the summoners score
        /// </summary>
        /// <param name="SummonerID"></param>
        /// <returns></returns>
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
            SummonerScore score = new SummonerScore
            {
                SummonerScores = response
            };

            return score;
        }

        /// <summary>
        /// Gets the summoners score
        /// </summary>
        /// <param name="SummonerID"></param>
        /// <returns></returns>
        public static async Task<List<Rootobject>> GetChampionTags()
        {

            string url = "https://api.npoint.io/17749797a03ae45a70d1";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<Rootobject> championData = await response.Content.ReadAsAsync<List<Rootobject>>();

                    return championData;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


    }
}
