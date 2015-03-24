using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class zilean : Base
    {

        public zilean()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);



            CastSpell(Q, qData);
            CastSpell(W, wData);
            CastSpell(E, eData);

            var lessHealthHero = getObject.LessHealthHero(R.Range);

            if (lessHealthHero.HealthPercentage() <= 8)
                R.CastOnUnit(lessHealthHero);
        }
    }
}
