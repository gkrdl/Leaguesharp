using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class tristana : Base
    {

        public tristana()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);
            TargetSelector.Mode = TargetSelector.TargetingMode.Closest;
            target = TargetSelector.GetTarget(Orbwalking.GetRealAutoAttackRange(Player), TargetSelector.DamageType.Physical);

            if (target != null)
            {
                if (target is Obj_AI_Hero)
                {
                    if (W.IsReady())
                        AntiGapClose(W);
                    else if (R.IsReady())
                        R.CastOnUnit(target);
                }
            }

            CastSpell(Q, qData);
            CastSpell(E, eData);
        }
    }
}
