using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class katarina : Base
    {
        public katarina()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);
            var closetTarget = getObject.ClosetHero();
            if (Killable(true, true, true, false))
                CastSpell(E, eData);
            else if (Player.Distance(closetTarget) <= 250)
            {
                var moreDistanceHero = getObject.MoreDistanceHero(E.Range);
                E.CastOnUnit(moreDistanceHero);
            }
            
            CastSpell(Q, qData);
            CastSpell(W, wData);

            //Killable()
            target = GetTarget(R);
            if (R.IsKillable(target))
                ;
        }
    }
}
