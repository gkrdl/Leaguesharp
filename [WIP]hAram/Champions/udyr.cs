using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class udyr : Base
    {

        public udyr()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);



            CastSpell(Q, qData);
            CastSpell(W, wData);
            CastSpell(E, eData);

            target = GetTarget(R);

            if (target != null && R.Instance.ToggleState == 1)
            {
                CastSpell(R, rData);
            }
            else if (target == null && R.Instance.ToggleState != 1)
            {
                CastSpell(R, rData);
            }
        }
    }
}
