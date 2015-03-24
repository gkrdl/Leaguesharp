using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class ashe : Base
    {
        public ashe()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(W, wData);

            if (Q.Instance.ToggleState == 1)
                CastSpell(Q, qData);

            var closetTarget = getObject.ClosetHero();

            if (Killable(true, true, true, true) || R.CastIfWillHit(target, 2) || (status == "Fight" && Player.HealthPercentage() <= 30))
                CastSpell(R, rData);
            else if (Player.Distance(closetTarget) <= 250)
                CastSpell(R, rData);
            
        }
    }
}
