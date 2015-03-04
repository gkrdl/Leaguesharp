using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace hAram.Champion
{
    public class Base
    {
        public Obj_AI_Hero Player = ObjectManager.Player;
        public string ChampName = ObjectManager.Player.ChampionName;
        public bool ComboMode = true;
        
        public Spell Q;
        public Spell W;
        public Spell E;
        public Spell R;

        
    }

    
}
