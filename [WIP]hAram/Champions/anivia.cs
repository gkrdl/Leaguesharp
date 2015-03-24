using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class anivia : Base
    {
        public anivia()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(E, eData);

            if (R.CastIfWillHit(target, 1) && R.Instance.ToggleState == 1)
                CastSpell(R, rData);
            else if (R.CastIfWillHit(target, 0) && R.Instance.ToggleState != 1)
                CastSpell(R, rData);

            CastSpell(W, wData);
            CastSpell(Q, qData);
        }
    }
}
