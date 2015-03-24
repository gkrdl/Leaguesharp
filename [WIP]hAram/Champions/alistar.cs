using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class alistar : Base
    {
        public alistar()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            var lowHealthHero = getObject.LessHealthHero(E.Range);

            if (lowHealthHero.HealthPercentage() <= 60)
                CastSpell(E, eData);

            CastSpell(W, wData);
            CastSpell(Q, qData);

            if (status == "Fight" && Player.HealthPercentage() <= 50)
                CastSpell(R, rData);
        }
    }
}
