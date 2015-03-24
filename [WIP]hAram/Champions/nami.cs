using hAram.Util;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class nami : Base
    {
        public nami()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            var lowHealthHero = getObject.LessHealthHero(W.Range);
            var moreRangeHero = getObject.MoreRangeHero(E.Range);
            if (lowHealthHero != null)
            {
                if (lowHealthHero.HealthPercentage() <= 70)
                    W.CastOnUnit(lowHealthHero);
            }
            if (moreRangeHero != null)
            {
                if (moreRangeHero.HealthPercentage() >= 50)
                    E.CastOnUnit(moreRangeHero);
            }
            
            CastSpell(Q, qData);
            CastSpell(W, wData);
            


            target = GetTarget(R);
            if (R.IsKillable(target) || R.CastIfWillHit(target, 2))
            {
                CastSpell(R, rData);
            }
        }
    }
}
