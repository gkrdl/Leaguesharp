using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class brand : Base
    {
        public brand()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(W, wData);
            CastSpell(Q, qData);
            CastSpell(E, eData);

            if (Killable(true, true, true, true) || (status == "Fight" && Player.HealthPercentage() <= 30))
                CastSpell(R, rData);
        }
    }
}
