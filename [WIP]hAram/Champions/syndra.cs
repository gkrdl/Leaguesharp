using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Champions
{
    class syndra : Base
    {

        public syndra()
        {
            Game.PrintChat("hAram : " + Player.ChampionName + "Loaded.");
        }

        public override void Game_OnUpdate(EventArgs args)
        {
            base.Game_OnUpdate(args);

            var wTarget = getObject.GetObj(); 
    

            CastSpell(Q, qData);

            target = GetTarget(W);
            if (target != null)
            {
                if (W.Instance.ToggleState == 1)
                    W.Cast(wTarget.Position);

                if (W.Instance.ToggleState != 1)
                    W.Cast(W.GetPrediction(target).CastPosition);
                
            }
            
            CastSpell(E, eData);

            target = GetTarget(R);

            if (Killable(false, false, false, true) || (status == "Fight" && Player.HealthPercentage() <= 30))
                CastSpell(R, rData);
        }
    }
}
