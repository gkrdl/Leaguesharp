using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class blitzcrank : Base
    {
        public blitzcrank()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(W, wData);
            CastSpell(Q, qData);
            CastSpell(E, eData);


            var closetTarget = getObject.ClosetHero();

            
            if (Killable(true, true, true, true) || R.CastIfWillHit(target, 2) || (status == "Fight" && Player.HealthPercentage() <= 30))
                CastSpell(R, rData);
            else if (Player.Distance(closetTarget) <= 250)
                CastSpell(R, rData);
        }
    }
}
