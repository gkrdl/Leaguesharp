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
    class nidalee : Base
    {
        private Spell cougarQ = new Spell(SpellSlot.Q, 200f);
        private Spell cougarW = new Spell(SpellSlot.W, 375f);
        private Spell cougarE = new Spell(SpellSlot.E, 275f);
        public nidalee()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            if (Player.Spellbook.GetSpell(SpellSlot.Q).Name.Equals("JavelinToss"))
            {
                CastSpell(Q, qData);
                CastSpell(W, wData);

                var lessHealthHero = getObject.LessHealthHero(E.Range);
                if (lessHealthHero != null)
                {
                    if (lessHealthHero.HealthPercentage() <= 70)
                    {
                        E.CastOnUnit(lessHealthHero);
                    }
                }

                target = GetTarget(cougarW);
                if (target != null)
                {
                    if (target.HasBuff("nidaleepassivehunted", true))
                    {
                        R.Cast();
                    }
                }
            }
            else
            {
                CastSpell(cougarQ, cougarQ.Instance);
                CastSpell(cougarE, cougarE.Instance);
                CastSpell(cougarW, cougarW.Instance);

                if ((!cougarQ.IsReady() && !cougarW.IsReady() && !cougarE.IsReady()))
                {
                    R.Cast();
                }
            }
        }

        public override void AntiGapcloser_OnEnemyGapcloser(ActiveGapcloser gapcloser)
        {
            if (gapcloser.Sender.IsEnemy)
            {
                if (Player.Distance(gapcloser.End) <= 200)
                {
                    if (Player.Spellbook.GetSpell(SpellSlot.Q).Name.Equals("JavelinToss"))
                    {
                        if (R.IsReady())
                        {
                            R.Cast();
                            AntiGapClose(W);
                        }
                    }
                    else
                        AntiGapClose(W);
                }
            }
        }
    }
}
