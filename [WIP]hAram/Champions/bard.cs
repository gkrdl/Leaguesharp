using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class bard : Base
    {
        public bard()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);

            var lowHealthTarget = getObject.LessHealthHero(W.Range);
            if (lowHealthTarget.HealthPercentage() <= 75)
                W.Cast(lowHealthTarget.Position);

            target = GetTarget(R);
            if (target != null)
            {
                if (!target.UnderTurret(true)
                    && R.CastIfWillHit(target, 3))
                {
                    CastSpell(R, rData);
                    CastSpell(E, eData);
                }
            }
            if (status == "Fight" && Player.HealthPercentage() <= 30)
                CastSpell(R, rData);
        }
    }
}
