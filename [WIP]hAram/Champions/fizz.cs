using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class fizz : Base
    {
        public fizz()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(R, rData);
            CastSpell(W, wData);

            var closetTarget = getObject.ClosetHero();

            if (E.IsKillable(target))
                CastSpell(E, eData);
            else if (Player.Distance(closetTarget) <= 250)
                AntiGapClose(E);
        }
    }
}
