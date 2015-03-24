using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class tryndamere : Base
    {

        public tryndamere()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);



            if (Player.HealthPercentage() <= 80)
                Q.Cast();

            CastSpell(W, wData);

            if (rangeAllyCnt > rangeEnemyCnt)
                CastSpell(E, eData);

            if (Player.HealthPercentage() <= 10)
                R.Cast();
        }
    }
}
