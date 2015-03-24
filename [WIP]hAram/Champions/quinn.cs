using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class quinn : Base
    {
        public quinn()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(W, wData);

            target = GetTarget(E);
            if (Killable(false, false, true, false) || Player.Distance(target) <= 200)
                CastSpell(E, eData);
            
            target = GetTarget(R);
            if (Killable(false, false, false, true) || (status == "Fight" && Player.HealthPercentage() <= 40))
            {
                CastSpell(R, rData);
            }
        }
    }
}
