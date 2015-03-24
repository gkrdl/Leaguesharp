using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class galio : Base
    {
        public galio()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(E, eData);

            W.CastOnUnit(Player);

            target = GetTarget(R);
            if (rangeAllyCnt + 1 >= rangeEnemyCnt && R.CastIfWillHit(target, 2))
                CastSpell(R, rData);
            else if (R.IsKillable(target) || (status == "Fight" && Player.HealthPercentage() <= 20))
                CastSpell(R, rData);

        }
    }
}
