using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace hAram
{
    internal class Program
    {
        private static Vector3[] buffs = { new Vector3(8922, 10, 7868), new Vector3(7473, 10, 6617), new Vector3(5929, 10, 5190), new Vector3(4751, 10, 3901)};
        private static Obj_AI_Hero Player = ObjectHandler.Player;
        private static Obj_AI_Hero target = null;
        private static Vector3 spawnPosition = new Vector3();
        private static string[] Assasin = { "akali", "diana", "evelynn", "fizz", "katarina", "nidalee" };
        private static string[] ADTank = { "drmnudo", "garen", "gnar", "hecarim", "jarvan iv", "nasus", "sion", "skarner", "udyr", "volibear", "warwick", "xinzhao", "yorick" };
        private static string[] ADCarry = { "ashe", "caitlyn", "corki", "draven", "ezreal", "gankplank", "graves", "jinx", "kogmaw", "lucian", "missfortune", "quinn", "sivir", "Thresh", "tristana", "tryndamere", "twitch", "urgot", "varus", "vayne" };
        private static string[] APTank = { "alistar", "amumu", "blitzcrank", "braum", "chogath", "leona", "malphite", "maokai", "nautilus", "rammus", "sejuani", "shen", "singed", "zac"};
        private static string[] APCarry = { "ahri", "anivia", "annie", "brand", "cassiopeia", "fiddlesticks", "galio", "gragas", "heimerdinger", "janna", "karma", "karthus", "leblanc", "lissandra", "lulu", "lux", "malzahar", "morgana", "nami", "nunu", "oriana", "ryze", "sona", "soraka", "swain", "syndra", "taric", "twistedfate", "veigar", "velkoz", "viktor", "xerath", "ziggs", "zillean", "zyra" };
        private static string[] APHybrid = { "kayle", "teemo" };
        private static string[] Bruiser = { "darius", "irelia", "khazix", "leesin", "olaf", "pantheon", "renekton", "rengar", "riven", "shyvana", "talon", "trundle", "vi", "wukong", "yasuo", "zed" };
        private static string[] ADCaster = { "aatrox", "fiora", "jax", "jayce", "nocturne", "poppy"};
        private static string[] APOther = { "elise", "kennen", "mordekaiser", "rumble", "vladimir" };
        private static int[] Shoplist;
        private static int heroType = 0;

        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            InitPlayer();

            Game.OnGameUpdate += Game_OnGameUpdate;
        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            if (!Player.IsDead)
            {
                
            }
        }

        private static void InitPlayer()
        {
            spawnPosition = Player.Position;

            if (ADCarry.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 1;
                int[] shoplist = { 3006, 1042, 3086, 3087, 3144, 3153, 1038, 3181, 1037, 3035, 3026, 0 };
                Shoplist = shoplist;
            }
            else if (ADTank.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 2;
                int[] shoplist = { 3047, 1011, 3134, 3068, 3024, 3025, 3071, 3082, 3143, 3005, 0 };
                Shoplist = shoplist;
            }
            else if (APTank.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 3;
                int[] shoplist = { 3111, 1031, 3068, 1057, 3116, 1026, 3001, 3082, 3110, 3102, 0 };
                Shoplist = shoplist;
            }
            else if (APHybrid.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 4;
                int[] shoplist = { 1001, 3108, 3115, 3020, 1026, 3136, 3089, 1043, 3091, 3151, 3116 };
                Shoplist = shoplist;
            }
            else if (Bruiser.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 5;
                int[] shoplist = { 3111, 3134, 1038, 3181, 3155, 3071, 1053, 3077, 3074, 3156, 3190 };
                Shoplist = shoplist;
            }
            else if (Assasin.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 6;
                int[] shoplist = { 3020, 3057, 3100, 1026, 3089, 3136, 3151, 1058, 3157, 3135, 0 };
                Shoplist = shoplist;
            }
            else if (APCarry.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 7;
                int[] shoplist = { 3028, 1001, 3020, 3136, 1058, 3089, 3174, 3151, 1026, 3001, 3135, 0 };
                Shoplist = shoplist;
            }
            else if (APOther.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 8;
                int[] shoplist = { 3145, 3020, 3152, 1026, 3116, 1058, 3089, 1026, 3001, 3157 };
                Shoplist = shoplist;
            }
            else if (ADCaster.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 9;
                int[] shoplist = { 3111, 3044, 3086, 3078, 3144, 3153, 3067, 3065, 3134, 3071, 3156, 0 };
                Shoplist = shoplist;
            }
            else
            {
                int[] shoplist = { 3111, 3044, 3086, 3078, 3144, 3153, 3067, 3065, 3134, 3071, 3156, 0 };
                Shoplist = shoplist;
            }
        }
    }
}
