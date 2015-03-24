using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class darius : Base
    {
        public darius()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(E, eData);
            CastSpell(W, wData);

            if (R.IsKillable(target) || (status == "Fight" && Player.HealthPercentage() <= 20))
                CastSpell(R, rData);
        }
    }
}
