using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class xerath : Base
    {

        public xerath()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);


            CastSpell(W, wData);
            target = GetTarget(Q);
            
            if (target != null)
            {
                if (Q.IsCharging)
                    Q.Cast(target, false, false);
                else
                    Q.StartCharging();
            }

            CastSpell(E, eData);

            target = GetTarget(R);

            if (target != null)
            {
                if (R.IsKillable(target))
                {
                    CastSpell(R, rData);
                }
            }
        }
    }
}
