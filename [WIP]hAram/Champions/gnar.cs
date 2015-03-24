using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class gnar : Base
    {
            Spell megaQ = new Spell(SpellSlot.Q, 1100);
            Spell megaW = new Spell(SpellSlot.W, 525);
            Spell megaE = new Spell(SpellSlot.E, 475);
            Spell megaR = new Spell(SpellSlot.R, 420);


        public gnar()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
            megaQ.SetSkillshot(0.25f, 80, 1200, true, SkillshotType.SkillshotLine);
            megaW.SetSkillshot(0.25f, 80, float.MaxValue, false, SkillshotType.SkillshotLine);
            megaE.SetSkillshot(0.5f, 150, float.MaxValue, false, SkillshotType.SkillshotCircle);
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            if (Player.BaseSkinName == "gnar")
            {
                CastSpell(Q, qData);

                var closetTarget = getObject.ClosetHero();


                if (Player.Distance(closetTarget) <= 250)
                    AntiGapClose(E);
            }
            else
            {
                CastSpell(megaQ, megaQ.Instance);
                CastSpell(megaW, megaW.Instance);
                CastSpell(megaE, megaE.Instance);

                target = GetTarget(megaR);

                if (megaR.IsKillable(target) || megaR.CastIfWillHit(target, 3) || (status == "Fight" && Player.HealthPercentage() <= 30))
                    CastSpell(megaR, megaR.Instance);
            }
        }
    }
}
