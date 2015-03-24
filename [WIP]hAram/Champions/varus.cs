using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class varus : Base
    {

        public varus()
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

            var closetTarget = getObject.ClosetHero();
            if (R.CastIfWillHit(target, 2) || (status == "Fight" && Player.HealthPercentage() <= 25))
                ;
            else if (Player.Distance(closetTarget) <= 250)
                CastSpell(R, rData);
            
        }
    }
}
