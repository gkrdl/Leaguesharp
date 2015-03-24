using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class vladimir : Base
    {

        public vladimir()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);



            CastSpell(Q, qData);
            CastSpell(E, eData);


            var closetTarget = getObject.ClosetHero();
            if (Player.Distance(closetTarget) <= 250)
                W.Cast();

            target = GetTarget(R);

            if (target != null)
            {
                if (R.IsKillable(target) || (status == "Fight" && Player.HealthPercentage() <= 25))
                {
                    CastSpell(R, rData);
                }
            }
        }
    }
}
