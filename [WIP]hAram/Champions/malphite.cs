using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class malphite : Base
    {
        public malphite()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(E, wData);            
            CastSpell(Q, qData);
            CastSpell(W, wData);

            target = GetTarget(R);
            if (Killable(true, false, true, true) || R.CastIfWillHit(target, 3) || (status == "Fight" && Player.HealthPercentage() <= 20))
            {
                CastSpell(R, rData);
            }
        }
    }
}
