using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class rammus : Base
    {
        public rammus()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(W, wData);
            CastSpell(E, eData);

            
            target = GetTarget(R);
            if (Killable(false, false, false, true) || (status == "Fight" && Player.HealthPercentage() <= 30))
            {
                CastSpell(R, rData);
            }
        }
    }
}
