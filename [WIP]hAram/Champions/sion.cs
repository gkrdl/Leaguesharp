using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class sion : Base
    {

        public sion()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            

            CastSpell(Q, qData);
            CastSpell(W, wData);
            CastSpell(E, eData);

            if (Killable(true, true, true, true) || (status == "Fight" && Player.HealthPercentage() <= 20))
            {
                CastSpell(R, rData);
            }
        }
    }
}
