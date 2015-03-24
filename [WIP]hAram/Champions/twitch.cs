using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class twitch : Base
    {

        public twitch()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(W, wData);
            CastSpell(E, eData);

            target = GetTarget(R);

            var closetTarget = getObject.ClosetHero();
            if (status == "Fight" && Player.Distance(closetTarget) <= 350)
                R.Cast();
        }
    }
}
