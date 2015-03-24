using LeagueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class aatrox : Base
    {
        public aatrox()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            if (rangeAllyCnt + 1 >= rangeEnemyCnt || Killable(true, true, true, true))
                CastSpell(Q, qData);

            CastSpell(W, wData);
            CastSpell(E, eData);
            CastSpell(R, rData);
        }

        
        
    }
}
