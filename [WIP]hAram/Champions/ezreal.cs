using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class ezreal : Base
    {
        public ezreal()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(W, wData);

            target = GetTarget(R);
            if (R.IsKillable(target) || R.CastIfWillHit(target, 2))
                CastSpell(R, rData);

            var closetTarget = getObject.ClosetHero();


            if (Player.Distance(closetTarget) <= 250)
                AntiGapClose(E);
        }
    }
}
