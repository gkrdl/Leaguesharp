using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class sivir : Base
    {

        public sivir()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            

            CastSpell(Q, qData);
            CastSpell(W, wData);

            if (status == "Fight")
                CastSpell(E, eData);

            if (Killable(true, true, true, true) || (status == "Fight" && Player.HealthPercentage() <= 50))
            {
                CastSpell(R, rData);
            }
        }

        public override void Orbwalking_AfterAttack(AttackableUnit unit, AttackableUnit target)
        {
            if (target.IsEnemy)
            {
                W.Cast();
            }
        }
    }
}
