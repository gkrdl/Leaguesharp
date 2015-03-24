using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class gangplank : Base
    {
        public gangplank()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(E, eData);

            if (Player.HealthPercentage() <= 80)
                CastSpell(W, wData);

            target = GetTarget(R);
            if (R.IsKillable(target) || R.CastIfWillHit(target, 3) || (status == "Fight" && Player.HealthPercentage() <= 20))
            {
                CastSpell(R, rData);
            }
        }
    }
}
