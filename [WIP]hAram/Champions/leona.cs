using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class leona : Base
    {
        public leona()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(W, wData);

            if (Killable(true, true, true, false))
                CastSpell(E, eData);

            if ((rangeAllyCnt + 1 >= rangeEnemyCnt && R.CastIfWillHit(target, 3)) || status == "Fight" && Player.HealthPercentage() <= 20)
            {
                CastSpell(R, rData);
            }
        }
    }
}
