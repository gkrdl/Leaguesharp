using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class thresh : Base
    {

        public thresh()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);



            if (rangeAllyCnt + 1 >= rangeEnemyCnt)
                CastSpell(Q, qData);

            var moreRangeHero = getObject.MoreRangeHero(W.Range);
            W.Cast(moreRangeHero.Position);

            target = GetTarget(E);
            if (target != null)
            {
                var x = Player.Position.X * 2 - target.Position.X;
                var y = Player.Position.Y * 2 - target.Position.Y;
                var z = Player.Position.Z;
                E.Cast(new Vector3(x, y, z));
            }


            target = GetTarget(R);
            if (Killable(false, false, true, true) || R.CastIfWillHit(target, 3) || Player.HealthPercentage() <= 20)
            {
                R.Cast();
            }
        }
    }
}
