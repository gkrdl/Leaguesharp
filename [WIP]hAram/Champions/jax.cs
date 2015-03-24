using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class jax : Base
    {
        public jax()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            if (Killable(true, true, true, true))
                CastSpell(Q, qData);
            CastSpell(E, eData);
            CastSpell(W, wData);

            if (E.Instance.ToggleState != 1)
                E.Cast();

            target = GetTarget(R);

            if (status == "Fight" && Player.HealthPercentage() <= 60)
                CastSpell(R, rData);
        }
    }
}
