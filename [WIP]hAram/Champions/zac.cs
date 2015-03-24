using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class zac : Base
    {

        public zac()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
            E.SetSkillshot(1550, 250, 1500, false, SkillshotType.SkillshotCone);
            E.SetCharged("ZacE", "ZacE", 1150, 1550, 1.5f);
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);



            CastSpell(Q, qData);
            CastSpell(W, wData);

            target = TargetSelector.GetTarget(E.ChargedMaxRange, TargetSelector.DamageType.Magical);
            if (target != null)
            {
                if (!target.UnderTurret(true))
                {
                    var pred = E.GetPrediction(target);

                    if (pred.Hitchance == HitChance.Medium)
                    {
                        if (E.IsCharging)
                            E.Cast(target, false, false);
                        else
                        {
                            E.StartCharging();
                            orb.SetMovement(true);
                        }
                    }
                }
            }

            target = GetTarget(R);

            if (status == "Fight")
                R.Cast();
        }

        public override void Obj_AI_Base_OnIssueOrder(Obj_AI_Base sender, GameObjectIssueOrderEventArgs args)
        {
            if (sender.IsMe)
            {
                if (E.IsCharging)
                {
                    args.Process = false;
                    orb.SetMovement(false);
                }
            }
        }
    }
}
