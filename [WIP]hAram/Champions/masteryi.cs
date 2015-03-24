using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class masteryi : Base
    {
        public masteryi()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(E, wData);
            CastSpell(W, wData);

            target = GetTarget(R);
            if (status == "Fight")
            {
                CastSpell(Q, qData);
                R.Cast();
            }
        }
    }
}
