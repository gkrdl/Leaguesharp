using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class shen : Base
    {

        public shen()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            

            CastSpell(Q, qData);
            if (status == "Fight")
                CastSpell(W, wData);


            target = GetTarget(E);
            if (Killable(true, true, true, true) || E.CastIfWillHit(target, 2) || rangeAllyCnt + 1 >= rangeEnemyCnt)
                CastSpell(E, eData);

            var lessHealthHero = getObject.LessHealthHero(R.Range);
            if (lessHealthHero.HealthPercentage() <= 20 || (status == "Fight" && Player.HealthPercentage() <= 15))
            {
                CastSpell(R, rData);
            }
        }
    }
}
