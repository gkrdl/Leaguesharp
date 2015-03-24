using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace hAram.Util
{
    class GetObject
    {
        Obj_AI_Hero Player = ObjectHandler.Player;

        public Obj_AI_Hero LessHealthHero(float range)
        {
            return ObjectHandler.Get<Obj_AI_Hero>().Allies
                        .Where(hero => Player.Distance(hero) <= range)
                        .OrderBy(h => h.HealthPercentage()).ToList()[0];
        }

        public Obj_AI_Hero ClosetHero()
        {
            return ObjectHandler.Get<Obj_AI_Hero>().Enemies
                        .OrderBy(hero => Player.Distance(hero)).ToList()[0];
        }

        public Obj_AI_Hero MoreDistanceHero(float range)
        {
            return ObjectHandler.Get<Obj_AI_Hero>().Allies
                        .Where(h => Player.Distance(h) <= range)
                        .OrderByDescending(hero => Player.Distance(hero)).ToList()[0];
        }

        public Obj_AI_Hero MoreRangeHero(float range)
        {
            return ObjectHandler.Get<Obj_AI_Hero>().Allies
                        .Where(hero => Player.Distance(hero) <= range)
                        .OrderByDescending(h => Orbwalking.GetRealAutoAttackRange(h)).ToList()[0];
        }

        public List<Obj_AI_Hero> GetAlliesList()
        {
            return ObjectHandler.Get<Obj_AI_Hero>().Allies;
        }

        public Obj_AI_Minion GetObj()
        {
            return ObjectManager.Get<Obj_AI_Minion>()
                            .Where(o => o.IsValid && o.Name == "Seed")
                            .OrderBy(t => Player.Distance(t))
                            .ToList()[0];
        }


    }
}
