using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class ryze : Base
    {

        public ryze()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            target = GetTarget(W);
            if (target != null && R.IsReady())
                CastSpell(R, rData);
            
            CastSpell(Q, qData);
            CastSpell(W, wData);
            CastSpell(E, eData);

            target = GetTarget(R);

            if (status == "Fight" && Player.HealthPercentage() <= 30)
            {
                CastSpell(R, rData);
            }
        }
    }
}
