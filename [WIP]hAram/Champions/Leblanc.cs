using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class leblanc : Base
    {
        public leblanc()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            target = GetTarget(Q);
            R.CastOnUnit(target);

            var closetTarget = getObject.ClosetHero();
            if (Killable(true, true, true, true))
                CastSpell(W, wData);
            else if (Player.Distance(closetTarget) <= 250)
                AntiGapClose(W);

            CastSpell(E, eData);


        }
    }
}
