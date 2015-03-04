using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using hAram.Libraries;

namespace hAram
{
    internal class Program
    {
        private static Menu config;
        private static Orbwalking.Orbwalker orb;
        private static Spell Q;
        private static Spell W;
        private static Spell E;
        private static Spell R;

        private static Vector3[] buffs = { new Vector3(8922, 10, 7868), new Vector3(7473, 10, 6617), new Vector3(5929, 10, 5190), new Vector3(4751, 10, 3901)};
        private static Obj_AI_Hero Player = ObjectHandler.Player;
        private static Obj_AI_Hero target = null;
        private static Obj_AI_Hero followTarget = null;
        private static string[] Assasin = { "akali", "diana", "evelynn", "fizz", "katarina", "nidalee" };
        private static string[] ADTank = { "drmnudo", "garen", "gnar", "hecarim", "jarvan iv", "nasus", "sion", "skarner", "udyr", "volibear", "warwick", "xinzhao", "yorick" };
        private static string[] ADCarry = { "ashe", "caitlyn", "corki", "draven", "ezreal", "gankplank", "graves", "jinx", "kogmaw", "lucian", "missfortune", "quinn", "sivir", "Thresh", "tristana", "tryndamere", "twitch", "urgot", "varus", "vayne" };
        private static string[] APTank = { "alistar", "amumu", "blitzcrank", "braum", "chogath", "leona", "malphite", "maokai", "nautilus", "rammus", "sejuani", "shen", "singed", "zac"};
        private static string[] APCarry = { "ahri", "anivia", "annie", "brand", "cassiopeia", "fiddlesticks", "galio", "gragas", "heimerdinger", "janna", "karma", "karthus", "leblanc", "lissandra", "lulu", "lux", "malzahar", "morgana", "nami", "nunu", "oriana", "ryze", "sona", "soraka", "swain", "syndra", "taric", "twistedfate", "veigar", "velkoz", "viktor", "xerath", "ziggs", "zillean", "zyra" };
        private static string[] APHybrid = { "kayle", "teemo" };
        private static string[] Bruiser = { "darius", "irelia", "khazix", "leesin", "olaf", "pantheon", "renekton", "rengar", "riven", "shyvana", "talon", "trundle", "vi", "wukong", "yasuo", "zed" };
        private static string[] ADCaster = { "aatrox", "fiora", "jax", "jayce", "nocturne", "poppy"};
        private static string[] APOther = { "elise", "kennen", "mordekaiser", "rumble", "vladimir" };
        private static int[] Shoplist;
        private static int lastShopID = -1;
        private static int heroType = 0;
        private static long lastFollow = 0;
        private static long followDelay = 6000000;
        private static long lastFollowTarget = 0;
        private static long nextFollowTargetDelay = 300000000;
        private static string status = string.Empty;

        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            Game.PrintChat("Loaded hAram");
            InitMenu();
            InitPlayer();
            orb.ActiveMode = Orbwalking.OrbwalkingMode.LaneClear;
            Game.OnGameUpdate += Game_OnGameUpdate;
        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            bool enabled = config.Item("Enabled").GetValue<bool>();
            if (!Player.IsDead && enabled)
            {
                target = TargetSelector.GetTarget(Player.AttackRange, TargetSelector.DamageType.Physical);

                orb.ActiveMode = Orbwalking.OrbwalkingMode.None;
                orb.InAutoAttackRange(target);
                orb.SetAttack(true);
                orb.SetMovement(false);

                float followRange = Player.AttackRange < 400 ? Player.AttackRange : 200;
                target = TargetSelector.GetTarget(followRange, TargetSelector.DamageType.Physical);
                BuyItems();
                CastSpells();

                if (target == null)
                    Following();
                AutoLevel();
            }
            else
                RefreshLastShop();
        }

        private static void InitMenu()
        {
            config = new Menu("hAram", "hAram", true);
            config.AddSubMenu(new Menu("Orbwalking", "Orbwalking"));
            orb = new Orbwalking.Orbwalker(config);
            config.AddItem(new MenuItem("Enabled", "Enabled").SetValue(true));
            //config.AddSubMenu()
            config.AddToMainMenu();
        }

        private static void InitPlayer()
        {
            if (ADCarry.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 1;
                int[] shoplist = { 3006, 1042, 3086, 3087, 3144, 3153, 1038, 3181, 1037, 3035, 3026, 0 };
                Shoplist = shoplist;
            }
            else if (ADTank.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 2;
                int[] shoplist = { 3047, 1011, 3134, 3068, 3024, 3025, 3071, 3082, 3143, 3005, 0 };
                Shoplist = shoplist;
            }
            else if (APTank.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 3;
                int[] shoplist = { 3111, 1031, 3068, 1057, 3116, 1026, 3001, 3082, 3110, 3102, 0 };
                Shoplist = shoplist;
            }
            else if (APHybrid.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 4;
                int[] shoplist = { 1001, 3108, 3115, 3020, 1026, 3136, 3089, 1043, 3091, 3151, 3116 };
                Shoplist = shoplist;
            }
            else if (Bruiser.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 5;
                int[] shoplist = { 3111, 3134, 1038, 3181, 3155, 3071, 1053, 3077, 3074, 3156, 3190 };
                Shoplist = shoplist;
            }
            else if (Assasin.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 6;
                int[] shoplist = { 3020, 3057, 3100, 1026, 3089, 3136, 3151, 1058, 3157, 3135, 0 };
                Shoplist = shoplist;
            }
            else if (APCarry.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 7;
                int[] shoplist = { 3028, 1001, 3020, 3136, 1058, 3089, 3174, 3151, 1026, 3001, 3135, 0 };
                Shoplist = shoplist;
            }
            else if (APOther.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 8;
                int[] shoplist = { 3145, 3020, 3152, 1026, 3116, 1058, 3089, 1026, 3001, 3157 };
                Shoplist = shoplist;
            }
            else if (ADCaster.Contains(Player.ChampionName.ToLower()))
            {
                heroType = 9;
                int[] shoplist = { 3111, 3044, 3086, 3078, 3144, 3153, 3067, 3065, 3134, 3071, 3156, 0 };
                Shoplist = shoplist;
            }
            else
            {
                int[] shoplist = { 3111, 3044, 3086, 3078, 3144, 3153, 3067, 3065, 3134, 3071, 3156, 0 };
                Shoplist = shoplist;
            }
            
            ChampSpellData spellData = new ChampSpellData();
            List<Spell> lstQ = spellData.GetSpellData(Player.ChampionName.ToLower(), SpellSlot.Q, "Q");
            List<Spell> lstW = spellData.GetSpellData(Player.ChampionName.ToLower(), SpellSlot.W, "W");
            List<Spell> lstE = spellData.GetSpellData(Player.ChampionName.ToLower(), SpellSlot.E, "E");
            List<Spell> lstR = spellData.GetSpellData(Player.ChampionName.ToLower(), SpellSlot.R, "R");

            float spellRange = Player.AttackRange < 400 ? 400 : Player.AttackRange;
            Q = lstQ.Count > 0 ? lstQ[0] : new Spell(SpellSlot.Q, spellRange);
            W = lstW.Count > 0 ? lstW[0] : new Spell(SpellSlot.W, spellRange);
            E = lstE.Count > 0 ? lstE[0] : new Spell(SpellSlot.E, spellRange);
            R = lstR.Count > 0 ? lstR[0] : new Spell(SpellSlot.R, spellRange);

        }

        private static Obj_AI_Hero GetClosetTarget()
        {
            TargetSelector.Mode = TargetSelector.TargetingMode.Closest;
            return TargetSelector.GetTarget(Player.AttackRange, LeagueSharp.Common.TargetSelector.DamageType.Physical);
        }

        private static Obj_AI_Hero GetFollowTarget(Obj_AI_Hero exceptHero)
        {
            Obj_AI_Hero target = null;

            List<Obj_AI_Hero> lstAlies = ObjectHandler.Get<Obj_AI_Hero>().Allies;



            foreach (Obj_AI_Hero hero in lstAlies)
            {
                if (!hero.IsDead
                    && !hero.InFountain()
                    && !hero.IsMe
                    && !hero.Equals(exceptHero))
                {
                    target = hero;
                    lastFollowTarget = DateTime.Now.Ticks;
                }
            }

                

            
            return target;
        }

        private static void Following()
        {
            if ((DateTime.Now.Ticks - lastFollowTarget > nextFollowTargetDelay) || followTarget.IsDead || followTarget.HealthPercentage() < 10)
                followTarget = GetFollowTarget(followTarget);

            if (status != "GetBuff" && (DateTime.Now.Ticks - lastFollow > followDelay))
            {
                 //&& Geometry.Distance(Player, target) > 300
                Random r = new Random();
                int distance1 = r.Next(150, 300);
                int distance2 = r.Next(150, 300);

                if (Player.AttackRange > 400)
                {
                    if (Player.Team == GameObjectTeam.Chaos)
                        Player.IssueOrder(GameObjectOrder.MoveTo, new Vector3(followTarget.Position.X + distance1, followTarget.Position.Y, followTarget.Position.Z + distance2));
                    else
                        Player.IssueOrder(GameObjectOrder.MoveTo, new Vector3(followTarget.Position.X - distance1, followTarget.Position.Y, followTarget.Position.Z - distance2));
                }
                else
                {
                    if (Player.Team == GameObjectTeam.Order)
                        Player.IssueOrder(GameObjectOrder.MoveTo, new Vector3(followTarget.Position.X + distance1, followTarget.Position.Y, followTarget.Position.Z + distance2));
                    else
                        Player.IssueOrder(GameObjectOrder.MoveTo, new Vector3(followTarget.Position.X - distance1, followTarget.Position.Y, followTarget.Position.Z - distance2));
                }
                lastFollow = DateTime.Now.Ticks;
            }
        }

        private static void BuyItems()
        {
            if (Player.InFountain())
            {
                for (int i = lastShopID + 1; i < Shoplist.Length; i++)
                {
                    Items.Item Item = new Items.Item(Shoplist[i]);
                    Item.Buy();
                }
            }
        }

        private static void CastSpells()
        {
            target = null;
            TargetSelector.Mode = TargetSelector.TargetingMode.AutoPriority;
            if (heroType == 3 || heroType == 4 || heroType == 6 || heroType == 7 || heroType == 8)
                target = TargetSelector.GetTarget(W.Range, TargetSelector.DamageType.Magical);
            else
                target = TargetSelector.GetTarget(W.Range, TargetSelector.DamageType.Physical);

            if (target != null && W.IsReady())
            {
                var pred = W.GetPrediction(target);
                if (pred.Hitchance >= HitChance.Medium)
                {
                    if (W.Speed != 0)
                        W.Cast(pred.CastPosition);
                    else
                    {
                        W.CastOnUnit(target);
                        if (W.IsReady())
                            W.Cast();
                    }
                }
                
            }

            target = null;
            if (heroType == 3 || heroType == 4 || heroType == 6 || heroType == 7 || heroType == 8)
                target = TargetSelector.GetTarget(Q.Range, TargetSelector.DamageType.Magical);
            else
                target = TargetSelector.GetTarget(Q.Range, TargetSelector.DamageType.Physical);

            if (target != null && Q.IsReady())
            {
                var pred = Q.GetPrediction(target);
                if (pred.Hitchance >= HitChance.Medium)
                {
                    if (Q.Speed != 0)
                        Q.Cast(pred.CastPosition);
                    else
                    {
                        Q.CastOnUnit(target);
                        if (Q.IsReady())
                            Q.Cast();
                    }
                }
            }


            target = null;
            if (heroType == 3 || heroType == 4 || heroType == 6 || heroType == 7 || heroType == 8)
                target = TargetSelector.GetTarget(E.Range, TargetSelector.DamageType.Magical);
            else
                target = TargetSelector.GetTarget(E.Range, TargetSelector.DamageType.Physical);

            if (target != null && E.IsReady())
            {
                var pred = E.GetPrediction(target);
                if (pred.Hitchance >= HitChance.Medium)
                {
                    if (E.Speed != 0)
                        E.Cast(pred.CastPosition);
                    else
                    {
                        E.CastOnUnit(target);
                        if (E.IsReady())
                            E.Cast();
                    }
                }
            }


            target = null;
            if (heroType == 3 || heroType == 4 || heroType == 6 || heroType == 7 || heroType == 8)
                target = TargetSelector.GetTarget(R.Range, TargetSelector.DamageType.Magical);
            else
                target = TargetSelector.GetTarget(R.Range, TargetSelector.DamageType.Physical);

            if (target != null && R.IsReady() && R.IsKillable(target))
            {
                var pred = R.GetPrediction(target);
                if (pred.Hitchance >= HitChance.Medium)
                {
                    if (R.Speed != 0)
                        R.Cast(pred.CastPosition);
                    else
                    {
                        R.CastOnUnit(target);
                        if (R.IsReady())
                            R.Cast();
                    }
                }
            }
        }

        private static void RefreshLastShop()
        {
            InventorySlot[] slots = Player.InventoryItems;

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].IsValidSlot()
                    && slots[i].Id != null 
                    && slots[i].Id != 0)
                {
                    for (int j = lastShopID + 1; j < Shoplist.Length; j++)
                    {
                        if (Items.HasItem(Shoplist[j])
                            && lastShopID < j)
                        {
                            lastShopID = j;
                        }
                    }
                } 
            }
            
        }

        private static void GetBuffs()
        {
            
        }

        private static void AutoLevel()
        {
            if ((Q.Level + W.Level + E.Level + R.Level) < Player.Level)
            {

                if (R.Level < Q.Level)
                    Player.Spellbook.LevelSpell(SpellSlot.R);
                if ((Q.Level <= E.Level || Q.Level != 5) && E.Level > 0)
                    Player.Spellbook.LevelSpell(SpellSlot.Q);
                else if ((E.Level <= W.Level || E.Level != 5) && W.Level > 0)
                    Player.Spellbook.LevelSpell(SpellSlot.E);
                else
                    Player.Spellbook.LevelSpell(SpellSlot.W);
            }
        }
    }
}
