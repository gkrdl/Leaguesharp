using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class xinzhao : Base
    {

        public xinzhao()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);




            CastSpell(Q, qData);
            CastSpell(W, wData);
            if (Killable(true, true, true, true))
                CastSpell(E, eData);

            target = GetTarget(R);

            if (target != null)
            {
                if (R.IsKillable(target) || R.CastIfWillHit(target, 3))
                {
                    CastSpell(R, rData);
                }
            }
        }
    }
}
