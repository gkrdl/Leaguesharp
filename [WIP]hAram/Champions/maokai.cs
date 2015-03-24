using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class maokai : Base
    {
        public maokai()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            if (rangeAllyCnt + 1 >= rangeEnemyCnt)
                CastSpell(E, wData);            
            CastSpell(Q, qData);
            CastSpell(W, wData);

            target = GetTarget(R);
            if (status == "Fight" && R.Instance.ToggleState == 1)
                R.Cast();
            else if (status != "Fight" && R.Instance.ToggleState != 1)
                R.Cast();
        }
    }
}
