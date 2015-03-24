using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class zed : Base
    {

        public zed()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);


            if (W.Instance.ToggleState == 1)
                CastSpell(W, wData);
            CastSpell(Q, qData);
            CastSpell(E, eData);


            target = GetTarget(R);

            if (Killable(true, true, true, true))
                R.CastOnUnit(target);
            
        }
    }
}
