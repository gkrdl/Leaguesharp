using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class lucian : Base
    {
        public lucian()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(W, wData);            
            CastSpell(R, rData);

            var closetTarget = getObject.ClosetHero();
            if (Player.Distance(closetTarget) <= 250)
                AntiGapClose(E);
            
        }
    }
}
