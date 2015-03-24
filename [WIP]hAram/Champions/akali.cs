using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class akali : Base
    {
        public akali()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);

            if (Killable(true, true, true, true) || (status == "Fight" && Player.HealthPercentage() <= 30))
                CastSpell(R, rData);
            
            CastSpell(E, eData);
            CastSpell(W, wData);
        }
    }
}
