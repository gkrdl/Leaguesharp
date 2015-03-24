using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class kayle : Base
    {
        public kayle()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            target = TargetSelector.GetTarget(500, TargetSelector.DamageType.Magical);
            
            if (target != null)
                E.Cast();
            
            CastSpell(Q, qData);

            var lessHealthHero = getObject.LessHealthHero(W.Range);
            
            if (lessHealthHero.HealthPercentage() <= 15)
                R.CastOnUnit(lessHealthHero);
            else if (lessHealthHero.HealthPercentage() <= 60)
                W.CastOnUnit(lessHealthHero);
        }
    }
}
