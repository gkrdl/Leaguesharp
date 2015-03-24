using hAram.Util;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class twistedfate : Base
    {

        public twistedfate()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);


            if (Player.ManaPercentage() > 20)
                CastSpell(Q, qData);

            TargetSelector.Mode = TargetSelector.TargetingMode.Closest;
            target = TargetSelector.GetTarget(Orbwalking.GetRealAutoAttackRange(Player), TargetSelector.DamageType.Physical);
            if (target != null)
            {
                if (Player.ManaPercentage() <= 20)
                    CardSelector.StartSelecting(Cards.Blue);
                else
                    CardSelector.StartSelecting(Cards.Yellow);
            }
        }
    }
}
