using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class yorick : Base
    {

        public yorick()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);



            CastSpell(Q, qData);
            CastSpell(W, wData);
            CastSpell(E, eData);

            target = GetTarget(R);

            var lessHealthHero = getObject.LessHealthHero(R.Range);

            if (lessHealthHero.HealthPercentage() <= 10)
                R.CastOnUnit(lessHealthHero);
        }
    }
}
