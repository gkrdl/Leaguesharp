using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class cassiopeia : Base
    {
        public cassiopeia()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(W, wData);

            if (target.HasBuffOfType(BuffType.Poison))
                CastSpell(E, eData);


            var closetTarget = getObject.ClosetHero();

            if (R.IsKillable(target) || R.CastIfWillHit(target, 2) || (status == "Fight" && Player.HealthPercentage() <= 30))
                CastSpell(R, rData);
            else if (Player.Distance(closetTarget) <= 250)
                CastSpell(R, rData);
        }
    }
}
