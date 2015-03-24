using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class soraka : Base
    {

        public soraka()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);



            CastSpell(Q, qData);
            CastSpell(E, eData);

            var lessHealtHero = getObject.LessHealthHero(W.Range);
            if (lessHealtHero.HealthPercentage() <= Player.HealthPercentage())
            {
                W.CastOnUnit(lessHealtHero);
            }

            target = GetTarget(R);
            if (lessHealtHero.HealthPercentage() <= 30)
            {
                CastSpell(R, rData);
            }
        }
    }
}
