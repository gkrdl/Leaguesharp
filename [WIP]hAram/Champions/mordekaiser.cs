using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class mordekaiser : Base
    {
        public mordekaiser()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(E, wData);
            CastSpell(Q, qData);

            target = GetTarget(W);
            if (status == "Fight")
                W.CastOnUnit(target);
            
            

            target = GetTarget(R);
            if (Killable(false, false, false, true))
                R.CastOnUnit(target);
        }
    }
}
