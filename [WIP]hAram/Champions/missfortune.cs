using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class missfortune : Base
    {
        public missfortune()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(E, wData);
            CastSpell(W, wData);

            target = GetTarget(R);
            if (R.CastIfWillHit(target, 3))
            {
                R.Cast(R.GetPrediction(target).CastPosition);
            }
        }
    }
}
