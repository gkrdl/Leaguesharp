using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class draven : Base
    {
        public draven()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(W, wData);

            target = GetTarget(R);
            if (R.IsKillable(target) || (status == "Fight" && Player.HealthPercentage() <= 30))
                CastSpell(R, rData);

            var closetTarget = getObject.ClosetHero();


            if (Player.Distance(closetTarget) <= 250)
                CastSpell(E, eData);
        }
    }
}
