using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class nunu : Base
    {
        public nunu()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            var moreRangeHero = getObject.MoreRangeHero(W.Range);

            if (moreRangeHero != null)
                W.CastOnUnit(moreRangeHero);
            else
                W.CastOnUnit(Player);

            CastSpell(E, eData);

            var minion = MinionManager.GetMinions(Player.Position, Q.Range)
                                    .OrderBy(m  => Player.Distance(m)).ToList()[0];

            if (minion != null && Player.HealthPercentage() <= 80)
                Q.CastOnUnit(minion);

            target = GetTarget(R);
            if (R.IsKillable(target) || R.CastIfWillHit(target, 3) || (status == "Fight" && Player.HealthPercentage() <= 30))
                CastSpell(R, rData);
        }
    }
}
