using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class lissandra : Base
    {
        public lissandra()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(W, wData);

            var closetTarget = getObject.ClosetHero();
            if (Killable(true, true, true, true))
                CastSpell(E, eData);
            else if (Player.Distance(closetTarget) <= 250)
                AntiGapClose(E);

            if (eData.ToggleState != 1)
                E.Cast();
            
            target = GetTarget(R);
            if (Killable(true, true, false, true) || status == "Fight" && Player.HealthPercentage() <= 20)
            {
                R.CastOnUnit(target);
            }
        }
    }
}
