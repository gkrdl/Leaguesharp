using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class leesin : Base
    {
        public leesin()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);

            if (Killable(true, false, true, true))
                Q.Cast();

            W.CastOnUnit(Player);
            CastSpell(E, eData);

            if (Killable(false, false, false, true) || status == "Fight" && Player.HealthPercentage() <= 20)
            {
                CastSpell(R, rData);
            }
        }
    }
}
