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
        #region 멤버, 변수
        private static Spell Q;
        private static Spell W;
        private static Spell E;
        private static Spell R;

        //private static Vector3[] buffs = { new Vector3(8922, 10, 7868), new Vector3(7473, 10, 6617), new Vector3(5929, 10, 5190), new Vector3(4751, 10, 3901)};
        private static Obj_AI_Hero Player = ObjectHandler.Player;
        #endregion

        #region 초기화
        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            var type = Type.GetType("hAram.Champions." + Player.ChampionName.ToLowerInvariant());
            Activator.CreateInstance(type);
           
        }
        #endregion
        
    }
}

