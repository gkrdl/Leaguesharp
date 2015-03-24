using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class yasuo : Base
    {

        public yasuo()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);



            CastSpell(Q, qData);
            CastSpell(W, wData);

            if (Killable(true, false, true, false))
                CastSpell(E, eData);

            target = GetTarget(R);

            if (R.CastIfWillHit(target, 3) || Killable(true, false, true, true))
                CastSpell(R, rData);
        }
    }
}
