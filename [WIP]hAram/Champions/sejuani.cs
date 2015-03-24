using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class sejuani : Base
    {

        public sejuani()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            
            if (Killable(true, true, true, true) || rangeAllyCnt + 1 >= rangeEnemyCnt)
                CastSpell(Q, qData);
            
            CastSpell(W, wData);
            CastSpell(E, eData);

            target = GetTarget(R);
            if (Killable(true, true, true, true) || R.CastIfWillHit(target, 3) || (status == "Fight" && Player.HealthPercentage() <= 30))
            {
                CastSpell(R, rData);
            }
        }
    }
}
