using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class shyvana : Base
    {

        public shyvana()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            

            CastSpell(Q, qData);
            CastSpell(W, wData);
            CastSpell(E, eData);

            if (Killable(true, true, true, true) || rangeAllyCnt + 1 >= rangeEnemyCnt || (status == "Fight" && Player.HealthPercentage() <= 15))
            {
                CastSpell(R, rData);
            }
        }
    }
}
