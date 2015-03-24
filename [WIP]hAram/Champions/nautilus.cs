using hAram.Util;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class nautilus : Base
    {
        public nautilus()
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
            if (Killable(false, false, false, true) || (status == "Fight" && Player.HealthPercentage() <= 40))
            {
                CastSpell(R, rData);
            }
        }
    }
}
