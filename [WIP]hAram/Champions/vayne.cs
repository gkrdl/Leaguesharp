using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class vayne : Base
    {

        public vayne()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            TargetSelector.Mode = TargetSelector.TargetingMode.Closest;
            target = TargetSelector.GetTarget(Orbwalking.GetRealAutoAttackRange(Player), TargetSelector.DamageType.Physical);


            var closetTarget = getObject.ClosetHero();

            if (Player.Distance(closetTarget) <= 300)
            {
                if (E.IsReady())
                    CastSpell(E, eData);
                else
                    AntiGapClose(Q); 
            }
             

            if (status == "Fight" && Player.Distance(closetTarget) <= 400)
                R.Cast();
        }
    }
}
