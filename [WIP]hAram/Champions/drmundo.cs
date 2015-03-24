using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class drmundo : Base
    {
        public drmundo()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(E, eData);

            target = GetTarget(W);
            if (target != null && W.Instance.ToggleState == 1)
                CastSpell(W, wData);
            else if (target == null && W.Instance.ToggleState != 1)
                CastSpell(W, wData);

            if (Player.HealthPercentage() <= 30)
                CastSpell(R, rData);
        }
    }
}
