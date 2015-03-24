using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class kalista : Base
    {
        private Obj_AI_Hero contractTarget;
        public kalista()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");



            List<Obj_AI_Hero> lstAlies = getObject.GetAlliesList().OrderBy(r => r.AttackRange).ToList();

            if (!lstAlies[0].IsMe)
                contractTarget = lstAlies[0];
            else
                contractTarget = lstAlies[1];

            Player.Spellbook.CastSpell(SpellSlot.Item6, contractTarget);
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            

            target = GetTarget(E);
            if (target.Health <= E.GetDamage(target))
                CastSpell(E, eData);

            target = GetTarget(R);
            if ((status == "Fight" && Player.HealthPercentage() <= 40) || contractTarget.HealthPercentage() <= 30)
                CastSpell(R, rData);
        }
    }
}
