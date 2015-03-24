using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class reksai : Base
    {      
        private Spell burrowQ = new Spell(SpellSlot.Q, 1500, TargetSelector.DamageType.Magical);
        private Spell burrowW = new Spell(SpellSlot.W, 250);
        private Spell burrowE = new Spell(SpellSlot.E, 750);
        
        public reksai()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
            burrowQ.SetSkillshot(0.125f, 60, 4000, true, SkillshotType.SkillshotLine);
            burrowE.SetSkillshot(0, 60, 1600, false, SkillshotType.SkillshotLine);
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            

            if (Player.GetSpell(SpellSlot.Q).SData.CastRange.Equals(burrowQ.Range))
            {
                CastSpell(burrowQ, burrowQ.Instance);
                CastSpell(burrowW, burrowW.Instance);
            }
            else
            {
                CastSpell(Q, qData);
                CastSpell(E, eData);

                target = GetTarget(E);
                if (Player.Distance(target) > Orbwalking.GetRealAutoAttackRange(Player))
                {
                    CastSpell(W, wData);
                }
            }
        }
    }
}
