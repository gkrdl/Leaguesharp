using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class caitlyn : Base
    {
        public caitlyn()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(W, wData);


            var closetTarget = getObject.ClosetHero();


            if (Player.Distance(closetTarget) <= 250)
                CastSpell(E, eData);

            if (R.IsKillable(target) 
                && (TargetSelector.GetTarget(Orbwalking.GetRealAutoAttackRange(Player), TargetSelector.DamageType.Physical) == null))
                CastSpell(R, rData);
        }
    }
}
