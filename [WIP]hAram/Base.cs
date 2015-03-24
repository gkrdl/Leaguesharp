using hAram.Util;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hAram
{
    class Base
    {
        #region 멤버, 변수
        public static Menu config;
        public static Orbwalking.Orbwalker orb;
        public Spell Q;
        public Spell W;
        public Spell E;
        public Spell R;

        //publi  Vector3[] buffs = { new Vector3(8922, 10, 7868), new Vector3(7473, 10, 6617), new Vector3(5929, 10, 5190), new Vector3(4751, 10, 3901)};
        public Obj_AI_Hero Player = ObjectHandler.Player;
        public Obj_AI_Hero target = null;
        public Obj_AI_Hero followTarget = null;
        public GetObject getObject { get; set; }

        public string[] Assasin = { "akali", "darius", "diana", "evelynn", "fizz", "katarina", "nidalee" };
        public string[] ADTank = { "drmnudo", "garen", "gnar", "hecarim", "irelia", "jarvan iv", "jax", "leesin", "olaf", "renekton", "rengar", "shyvana", "sion", "skarner", "thresh", "trundle", "udyr", "volibear", "warwick", "wukong", "xinzhao", "yorick" };
        public string[] ADCarry = { "ashe", "caitlyn", "corki", "draven", "ezreal", "gankplank", "graves", "jinx", "kalista", "kogmaw", "lucian", "masteryi", "missfortune", "quinn", "sivir", "tristana", "tryndamere", "twitch", "urgot", "varus", "vayne" };
        public string[] APTank = { "alistar", "amumu", "blitzcrank", "braum", "chogath", "leona", "malphite", "maokai", "nasus", "nautilus", "rammus", "sejuani", "shen", "singed", "zac" };
        public string[] APCarry = { "ahri", "anivia", "annie", "azir", "brand", "cassiopeia", "fiddlesticks", "galio", "gragas", "heimerdinger", "janna", "karma", "karthus", "leblanc", "lissandra", "lulu", "lux", "malzahar", "morgana", "nami", "nunu", "orianna", "ryze", "sona", "soraka", "swain", "syndra", "taric", "twistedfate", "veigar", "velkoz", "viktor", "xerath", "ziggs", "zilean", "zyra" };
        public string[] APHybrid = { "kayle", "teemo" };
        public string[] Bruiser = { "khazix", "pantheon", "riven", "talon", "vi", "yasuo", "zed" };
        public string[] ADCaster = { "aatrox", "fiora", "jayce", "nocturne", "poppy" };
        public string[] APOther = { "elise", "kennen", "mordekaiser", "rumble", "vladimir" };

        public int[] Shoplist;
        public List<int> lstHasItem = new List<int>();
        public int lastShopID = -1;
        public int heroType = 0;
        public long lastFollow = 0;
        public long followDelay = 6000000;
        public Vector3 lastFollowTargetPos = new Vector3();
        public long lastFollowTarget = 0;
        public long nextFollowTargetDelay = 300000000;
        public string status = "Follow";
        public List<Obj_AI_Turret> lstTurrets = new List<Obj_AI_Turret>();
        public Obj_AI_Turret turret = null;

        public SpellDataInst qData = ObjectHandler.Player.Spellbook.GetSpell(SpellSlot.Q);
        public SpellDataInst wData = ObjectHandler.Player.Spellbook.GetSpell(SpellSlot.W);
        public SpellDataInst eData = ObjectHandler.Player.Spellbook.GetSpell(SpellSlot.E);
        public SpellDataInst rData = ObjectHandler.Player.Spellbook.GetSpell(SpellSlot.R);

        public int rangeAllyCnt = 0;
        public int rangeEnemyCnt = 0;
        #endregion
        
        #region 초기화
        protected Base()
        {
            getObject = new Util.GetObject();
            InitMenu();
            InitPlayer();
            Game.OnUpdate += Game_OnUpdate;
            Obj_AI_Hero.OnProcessSpellCast += Obj_AI_Hero_OnProcessSpellCast;
            Obj_AI_Base.OnIssueOrder += Obj_AI_Base_OnIssueOrder;
            AntiGapcloser.OnEnemyGapcloser += AntiGapcloser_OnEnemyGapcloser;
        }

        private static void InitMenu()
        {
            config = new Menu("hAram", "hAram", true);
            config.AddSubMenu(new Menu("Orbwalking", "Orbwalking"));
            orb = new Orbwalking.Orbwalker(config);
            config.AddToMainMenu();
        }

        public void InitPlayer()
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


            Q = new Spell(SpellSlot.Q, GetSpellRange(qData));
            Q.Speed = qData.SData.MissileSpeed;
            Q.Width = qData.SData.LineWidth;
            Q.Delay = qData.SData.SpellCastTime;

            W = new Spell(SpellSlot.W, GetSpellRange(wData));
            W.Speed = wData.SData.MissileSpeed;
            W.Width = wData.SData.LineWidth;


            E = new Spell(SpellSlot.E, GetSpellRange(eData));
            E.Speed = eData.SData.MissileSpeed;
            E.Width = eData.SData.LineWidth;

            R = new Spell(SpellSlot.R, GetSpellRange(rData));
            R.Speed = rData.SData.MissileSpeed;
            R.Width = rData.SData.LineWidth;
        }
        #endregion

        public virtual void Game_OnUpdate(EventArgs args)
        {
            if (!Player.IsDead)
            {
                SetAttack();
                BuyItems();
                Following();
                AutoLevel();
                GetBuffs();

                rangeAllyCnt = Player.CountAlliesInRange(600);
                rangeEnemyCnt = Player.CountEnemiesInRange(600);
            }
            else
                RefreshLastShop();
        }

        public virtual void Obj_AI_Hero_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args) { }

        public virtual void AntiGapcloser_OnEnemyGapcloser(ActiveGapcloser gapcloser) { }

        public virtual void Obj_AI_Base_OnIssueOrder(Obj_AI_Base sender, GameObjectIssueOrderEventArgs args) { }

        #region 사용자함수

        public  void SetAttack()
        {
            AttackableUnit orbTarget = orb.GetTarget();

            if (orbTarget != null)
            {
                if (orbTarget is Obj_AI_Hero)
                {
                    if (!Player.UnderTurret(true))
                    {
                        SetOrbWalk(orbTarget);
                    }
                    status = "Fight";
                }
                else if (orbTarget is Obj_AI_Minion)
                {
                    status = "Follow";
                    if (orbTarget.Health <= Player.GetAutoAttackDamage(Player, true))
                    {
                        orb.SetAttack(true);
                        Player.IssueOrder(GameObjectOrder.AttackUnit, orbTarget);
                    }
                }
            }
            else 
                status = "Follow";

            lstTurrets = ObjectHandler.Get<Obj_AI_Turret>().Enemies.ToList().FindAll(t => !t.IsDead);
            turret = lstTurrets.OrderBy(t => t.Distance(Player)).ToList().Count > 0 ? lstTurrets.OrderBy(t => t.Distance(Player)).ToList()[0] : null;

            if (turret != null)
            {

                if (turret.Distance(Player) <= Player.AttackRange)
                {
                    orb.SetAttack(true);
                }

            }
        }


        public  void SetOrbWalk(AttackableUnit attackTarget)
        {
            if (attackTarget is Obj_AI_Hero)
            {
                status = "Fight";

                float distance1 = 0;
                if (Player.Distance(attackTarget) <= Orbwalking.GetRealAutoAttackRange(Player) - 120)
                {
                    distance1 = (Orbwalking.GetRealAutoAttackRange(Player) - Player.Distance(attackTarget) - Player.Distance(attackTarget) / 2) / 2;
                    if (Player.Team == GameObjectTeam.Chaos)
                        Orbwalking.Orbwalk(attackTarget, new Vector3(Player.Position.X + distance1, Player.Position.Y + distance1, Player.Position.Z));
                    else
                        Orbwalking.Orbwalk(attackTarget, new Vector3(Player.Position.X - distance1, Player.Position.Y - distance1, Player.Position.Z));
                }
                else if (Player.Distance(attackTarget) >= Orbwalking.GetRealAutoAttackRange(Player))
                {
                    distance1 = (Orbwalking.GetRealAutoAttackRange(Player) - Player.Distance(attackTarget) - Player.Distance(attackTarget) / 2) / 2;
                    if (Player.Team == GameObjectTeam.Chaos)
                        Orbwalking.Orbwalk(attackTarget, new Vector3(Player.Position.X - distance1, Player.Position.Y - distance1, Player.Position.Z));
                    else
                        Orbwalking.Orbwalk(attackTarget, new Vector3(Player.Position.X + distance1, Player.Position.Y + distance1, Player.Position.Z));
                }
            }
        }

        public  Obj_AI_Hero GetFollowTarget(Obj_AI_Hero exceptHero)
        {
            Obj_AI_Hero targett = null;
            List<Obj_AI_Hero> lstAlies = ObjectHandler.Get<Obj_AI_Hero>().Allies;
            bool lessRangeHero = false;

            if (exceptHero != null)
            {
                foreach (Obj_AI_Hero hero in lstAlies)
                {
                    if (!hero.IsDead
                        && !hero.InFountain()
                        && !hero.IsMe
                        && hero.HealthPercentage() >= 25
                        && !hero.ChampionName.Equals(exceptHero.ChampionName))
                    {
                        if (Player.AttackRange > hero.AttackRange)
                        {
                            lessRangeHero = true;
                            break;

                        }
                    }
                }

                foreach (Obj_AI_Hero hero in lstAlies)
                {
                    if (lessRangeHero)
                    {
                        if (!hero.IsDead
                        && !hero.InFountain()
                        && !hero.IsMe
                        && hero.HealthPercentage() >= 25
                        && !hero.ChampionName.Equals(exceptHero.ChampionName))
                        {
                            if (Player.AttackRange > hero.AttackRange)
                            {
                                targett = hero;
                                lastFollowTarget = DateTime.Now.Ticks;
                                lastFollowTargetPos = targett.Position;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (!hero.IsDead
                        && !hero.InFountain()
                        && !hero.IsMe
                        && hero.HealthPercentage() >= 25
                        && !hero.ChampionName.Equals(exceptHero.ChampionName))
                        {
                            targett = hero;
                            lastFollowTarget = DateTime.Now.Ticks;
                            lastFollowTargetPos = targett.Position;
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (Obj_AI_Hero hero in lstAlies)
                {
                    if (!hero.IsDead
                        && !hero.InFountain()
                        && !hero.IsMe
                        && hero.HealthPercentage() >= 25)
                    {
                        if (Player.AttackRange > hero.AttackRange)
                        {
                            lessRangeHero = true;
                            break;

                        }
                    }
                }

                foreach (Obj_AI_Hero hero in lstAlies)
                {
                    if (lessRangeHero)
                    {
                        if (!hero.IsDead
                        && !hero.InFountain()
                        && !hero.IsMe
                        && hero.HealthPercentage() >= 25)
                        {
                            if (Player.AttackRange > hero.AttackRange)
                            {
                                targett = hero;
                                lastFollowTarget = DateTime.Now.Ticks;
                                lastFollowTargetPos = targett.Position;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (!hero.IsDead
                        && !hero.InFountain()
                        && !hero.IsMe
                        && hero.HealthPercentage() >= 25)
                        {
                            targett = hero;
                            lastFollowTarget = DateTime.Now.Ticks;
                            lastFollowTargetPos = targett.Position;
                            break;
                        }
                    }
                }
            }
            return targett;
        }

        public  void Following()
        {

            if ((DateTime.Now.Ticks - lastFollowTarget > nextFollowTargetDelay)
                || followTarget == null
                || followTarget.IsDead
                || followTarget.HealthPercentage() < 25)
            {
                followTarget = GetFollowTarget(followTarget);
            }

            if (followTarget != null)
            {
                if (status != "GetBuff" && status != "Fight" && (DateTime.Now.Ticks - lastFollow > followDelay))
                {
                    Random r = new Random();
                    int distance1 = r.Next(50, 200);
                    int distance2 = r.Next(50, 200);

                    if (Player.AttackRange >= followTarget.AttackRange)
                    {
                        if (Player.Team == GameObjectTeam.Chaos)
                        {
                            Player.IssueOrder(GameObjectOrder.MoveTo, new Vector3(followTarget.Position.X + distance1, followTarget.Position.Y, followTarget.Position.Z + distance2));
                            orb.SetOrbwalkingPoint(new Vector3(followTarget.Position.X + distance1, followTarget.Position.Y, followTarget.Position.Z + distance2));
                        }
                        else
                        {
                            Player.IssueOrder(GameObjectOrder.MoveTo, new Vector3(followTarget.Position.X - distance1, followTarget.Position.Y, followTarget.Position.Z - distance2));
                            orb.SetOrbwalkingPoint(new Vector3(followTarget.Position.X - distance1, followTarget.Position.Y, followTarget.Position.Z - distance2));
                        }
                    }
                    else
                    {
                        if (Player.Team == GameObjectTeam.Order)
                        {
                            Player.IssueOrder(GameObjectOrder.MoveTo, new Vector3(followTarget.Position.X + distance1, followTarget.Position.Y, followTarget.Position.Z + distance2));
                            orb.SetOrbwalkingPoint(new Vector3(followTarget.Position.X + distance1, followTarget.Position.Y, followTarget.Position.Z + distance2));
                        }
                        else
                        {
                            Player.IssueOrder(GameObjectOrder.MoveTo, new Vector3(followTarget.Position.X - distance1, followTarget.Position.Y, followTarget.Position.Z - distance2));
                            orb.SetOrbwalkingPoint(new Vector3(followTarget.Position.X - distance1, followTarget.Position.Y, followTarget.Position.Z - distance2));
                        }
                    }
                    lastFollow = DateTime.Now.Ticks;
                }
            }
        }

        public  void BuyItems()
        {
            if (Player.InFountain())
            {
                for (int i = 0; i < Shoplist.Length; i++)
                {
                    if (!lstHasItem.Contains(Shoplist[i]))
                    {
                        Items.Item Item = new Items.Item(Shoplist[i]);
                        Item.Buy();

                        InventorySlot[] slots = Player.InventoryItems;
                        for (int j = 0; j < slots.Length; j++)
                        {
                            if (slots[j].IsValidSlot()
                                && slots[j].Id != null
                                && slots[j].Id != 0
                                && Items.HasItem(Shoplist[i])
                                && !lstHasItem.Contains(Shoplist[i]))
                            {
                                lstHasItem.Add(Shoplist[i]);
                                break;
                            }
                        }

                    }
                }
            }
        }

        public void CastSpell(Spell spell, SpellDataInst sDataInst)
        {

            target = GetTarget(spell);
            if (target != null && spell.IsReady())
            {

                if (target.UnderTurret(true))
                    return;

                var pred = spell.GetPrediction(target);

                if (sDataInst.SData.IsToggleSpell)
                {
                    if (spell.Instance.ToggleState == 1)
                    {
                        if (sDataInst.SData.TargettingType == SpellDataTargetType.Location)
                            spell.Cast(pred.CastPosition);
                        else if (sDataInst.SData.TargettingType == SpellDataTargetType.Unit)
                            spell.CastOnUnit(target);
                        else
                            spell.Cast();
                    }
                }
                else
                {
                    if (spell.IsReady())
                    {
                        if (sDataInst.SData.TargettingType == SpellDataTargetType.Self)
                            spell.Cast();
                        else if (sDataInst.SData.TargettingType == SpellDataTargetType.Unit)
                            spell.CastOnUnit(target);
                        else if (pred.Hitchance >= HitChance.Medium)
                        {
                            spell.Cast(pred.CastPosition);
                        }
                    }
                }
            }
        }

        public void AntiGapClose(Spell spell)
        {
            if (Player.Team == GameObjectTeam.Chaos)
                spell.Cast(new Vector3(Player.Position.X + spell.Range / 2, Player.Position.Y + spell.Range / 2, Player.Position.Z));
            else
                spell.Cast(new Vector3(Player.Position.X - spell.Range / 2, Player.Position.Y - spell.Range / 2, Player.Position.Z));

        }
        

        public  void RefreshLastShop()
        {
            InventorySlot[] slots = Player.InventoryItems;

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].IsValidSlot()
                    && slots[i].Id != null
                    && slots[i].Id != 0)
                {
                    for (int j = 0; j < Shoplist.Length; j++)
                    {
                        if (Items.HasItem(Shoplist[j])
                            && !lstHasItem.Contains(Shoplist[j]))
                            lstHasItem.Add(Shoplist[j]);
                    }
                }
            }

        }

        public  float GetSpellRange(SpellDataInst targetSpell, bool IsChargedSkill = false)
        {
            if (targetSpell.SData.CastRangeDisplayOverride <= 50)
            {
                if (targetSpell.SData.CastRange <= 50)
                {
                    return targetSpell.SData.CastRadius;
                }
                else
                {
                    if (!IsChargedSkill)
                        return targetSpell.SData.CastRange;
                    else
                        return targetSpell.SData.CastRadius;
                }
            }
            else
                return Player.ChampionName.ToLowerInvariant() == "urgot" ? targetSpell.SData.CastRange : targetSpell.SData.CastRangeDisplayOverride;
        }

        public  void GetBuffs()
        {
            //var lstHealth = ObjectHandler.Get<Obj_AI_Base>().FindAll(health => health.Name.Contains("HA_AP_HealthRelic")).ToList().OrderBy(health => Player.Distance(health, true)).ToList();
            //Obj_AI_Base healthBuff = null;

            //if (lstHealth.Count > 0)
            //{
            //    healthBuff = lstHealth[0];
            //}
            //target = null;
            //target = TargetSelector.GetTarget(Player.AttackRange, TargetSelector.DamageType.Physical);

            //if (target == null && healthBuff != null)
            //{
            //    if (Player.HealthPercentage() <= 50 && Player.Distance(healthBuff.Position) > 50)
            //    {
            //        Console.WriteLine(Player.Distance(healthBuff.Position));
            //        status = "GetBuff";
            //        Player.IssueOrder(GameObjectOrder.MoveTo, healthBuff.Position);
            //    }
            //    else
            //    {
            //        status = "Follow";
            //    }
            //}
        }

        public  void AutoLevel()
        {
            if ((Q.Level + W.Level + E.Level + R.Level) < Player.Level)
            {
                int rLevel = 0;

                switch (Player.Level)
                {
                    case 6:
                        rLevel = 1;
                        break;
                    case 11:
                        rLevel = 2;
                        break;
                    case 16:
                        rLevel = 3;
                        break;
                }

                if (R.Level < Q.Level && R.Level != rLevel)
                    Player.Spellbook.LevelSpell(SpellSlot.R);
                if ((Q.Level <= E.Level && Q.Level != 5) || (Q.Level == 0))
                    Player.Spellbook.LevelSpell(SpellSlot.Q);
                else if ((E.Level <= W.Level && E.Level != 5) || (E.Level == 0))
                    Player.Spellbook.LevelSpell(SpellSlot.E);
                else
                    Player.Spellbook.LevelSpell(SpellSlot.W);
            }
        }

        public bool Killable(bool qFlag, bool wFlag, bool eFlag, bool rFlag)
        {
            if (target == null)
                return false;

            var damage = 0d;
            if (Q.IsReady() && qFlag)
                damage += Player.GetSpellDamage(target, SpellSlot.Q);

            if (W.IsReady() && wFlag)
                damage += Player.GetSpellDamage(target, SpellSlot.W);

            if (E.IsReady() && eFlag)
                damage += Player.GetSpellDamage(target, SpellSlot.E);

            if (R.IsReady() && rFlag)
                damage += Player.GetSpellDamage(target, SpellSlot.R);

            return target.Health <= damage;
        }

        public Obj_AI_Hero GetTarget(Spell spell)
        {
            if (heroType == 2 || heroType == 3 || heroType == 5 || heroType == 6 || heroType == 9)
                TargetSelector.Mode = TargetSelector.TargetingMode.AutoPriority;
            else if (heroType == 4 || heroType == 7 || heroType == 8)
                TargetSelector.Mode = TargetSelector.TargetingMode.LessCast;
            else
                TargetSelector.Mode = TargetSelector.TargetingMode.LessAttack;


            if (heroType == 3 || heroType == 4 || heroType == 6 || heroType == 7 || heroType == 8)
                target = TargetSelector.GetTarget(spell.Range, TargetSelector.DamageType.Magical);
            else
                target = TargetSelector.GetTarget(spell.Range, TargetSelector.DamageType.Physical);

            return target;
        }
        #endregion
    }
}
