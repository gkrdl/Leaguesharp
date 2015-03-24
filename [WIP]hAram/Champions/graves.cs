using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class graves : Base
    {
        public graves()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(W, wData);

            var closetTarget = getObject.ClosetHero();

            if (Player.Distance(closetTarget) <= 200)
            {
                AntiGapClose(E);
            }

            target = GetTarget(R);

            if (target != null)
            {
                if (R.IsKillable(target) || R.CastIfWillHit(target, 3) || (status == "Fight" && Player.HealthPercentage() <= 30))
                    CastSpell(R, rData);
            }
        }
    }
}
