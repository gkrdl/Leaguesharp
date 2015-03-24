using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class lux : Base
    {
        public lux()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(E, wData);            
            CastSpell(Q, qData);

            var lessHealthHero = getObject.LessHealthHero(W.Range);
            if (status == "Fight")
                W.Cast(lessHealthHero.Position);

            target = GetTarget(R);
            if (Killable(false, false, false, true) || R.CastIfWillHit(target, 3) || (status == "Fight" && Player.HealthPercentage() <= 20))
            {
                CastSpell(R, rData);
            }
        }
    }
}
