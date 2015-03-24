using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class lulu : Base
    {
        public lulu()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(E, wData);            
            CastSpell(Q, qData);
            CastSpell(W, wData);

            var lessHealthHero = getObject.LessHealthHero(R.Range);

            if (lessHealthHero.HealthPercentage() <= 20)
            {
                R.CastOnUnit(lessHealthHero);
            }
        }
    }
}
