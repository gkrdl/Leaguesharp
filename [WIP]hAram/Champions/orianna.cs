using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class orianna : Base
    {
        public orianna()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
            R = new Spell(SpellSlot.R, 370);
            R.SetSkillshot(0.60f, 370, float.MaxValue, false, SkillshotType.SkillshotCircle);
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            CastSpell(Q, qData);
            CastSpell(W, wData);

            if (status == "Fight")
                E.CastOnUnit(Player);

            target = GetTarget(R);
            if (Killable(false, false, false, true) || R.CastIfWillHit(target, 2) || (status == "Fight" && Player.HealthPercentage() <= 30))
                R.Cast();
        }
    }
}
