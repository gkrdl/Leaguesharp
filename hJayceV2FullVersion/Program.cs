using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace hJayceV2FullVersion
{
    class Program
    {
        #region Member
        private const string champName = "Jayce";
        private static Obj_AI_Hero player = ObjectManager.Player;

        private static Menu configMenu;
        private static Orbwalking.Orbwalker Orbwalker;

        private static bool cancelMovt = false;

        private static int cannonRange = 500;
        private static int hammerRange = 125;

        private static Spell hammerQ;
        private static Spell hammerW;
        private static Spell hammerE;

        private static Spell cannonQ;
        private static Spell cannonQ2;
        private static Spell cannonW;
        private static Spell cannonE;

        private static Spell R;
        private static int blockCount = 0;

        private static string shotMode = string.Empty;
        private static string gapMode = string.Empty;

        private static double lastAttackSecond = 0;
        #endregion

        #region Initialize
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            if (!player.ChampionName.ToUpper().Equals(champName.ToUpper()))
                return;
            else
            {
                InitDefault();
                InitEvent();
                Game.PrintChat("hJayce V2 FullVersion Loaded.");
            }

        }

        private static void InitDefault()
        {
            //Skill Setting
            cannonQ = new Spell(SpellSlot.Q, 1050);
            cannonQ2 = new Spell(SpellSlot.Q, 1460);
            cannonW = new Spell(SpellSlot.W, cannonRange);
            cannonE = new Spell(SpellSlot.E, 650);

            hammerQ = new Spell(SpellSlot.Q, 600);
            hammerW = new Spell(SpellSlot.W, 285);
            hammerE = new Spell(SpellSlot.E, 230);

            R = new Spell(SpellSlot.R);

            cannonQ.SetSkillshot(0.15f, 70, 1200, true, SkillshotType.SkillshotLine);
            cannonQ2.SetSkillshot(0.15f, 70, 1680, true, SkillshotType.SkillshotLine);
            cannonE.SetSkillshot(0.1f, 120, float.MaxValue, false, SkillshotType.SkillshotCircle);
            hammerQ.SetTargetted(0.15f, float.MaxValue);
            hammerE.SetTargetted(0.15f, float.MaxValue);

            //Menu Setting
            configMenu = new Menu("hJayce V2 Full Version", "Jayce", true);

            configMenu.AddSubMenu(new Menu("Target Selector", "Target Selector"));
            
            TargetSelector.AddToMenu(configMenu.SubMenu("Target Selector"));
            TargetSelector.Mode = TargetSelector.TargetingMode.LessAttack;

            configMenu.AddSubMenu(new Menu("Orbwalker", "Orbwalker"));
            Orbwalker = new Orbwalking.Orbwalker(configMenu.SubMenu("Orbwalker"));

            configMenu.AddSubMenu(new Menu("Combo", "Combo"));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("UseCannonQ", "Use Cannon Q").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("UseCannonW", "Use Cannon W").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("UseCannonE", "Use Cannon E").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("UseCannonR", "Use Cannon R").SetValue(false));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("UseHammerQ", "Use Hammer Q").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("UseHammerW", "Use Hammer W").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("UseHammerE", "Use Hammer E").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("UseHammerR", "Use Hammer R").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("UseMuramana", "Use Muramana").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("MuramanaMana", "Muramana Min Mana %").SetValue(new Slider(20, 0, 100)));

            configMenu.AddSubMenu(new Menu("Harass", "Harass"));
            configMenu.SubMenu("Harass").AddItem(new MenuItem("UseCannonQ", "Use Cannon Q").SetValue(true));
            configMenu.SubMenu("Harass").AddItem(new MenuItem("UseCannonW", "Use Cannon W").SetValue(true));
            configMenu.SubMenu("Harass").AddItem(new MenuItem("UseCannonE", "Use Cannon E").SetValue(true));

            configMenu.AddSubMenu(new Menu("Misc", "Misc"));
            configMenu.SubMenu("Misc").AddItem(new MenuItem("AutoGate", "Auto Gate On").SetValue(true));

            configMenu.AddSubMenu(new Menu("Drawing", "Drawing"));
            configMenu.SubMenu("Drawing").AddItem(new MenuItem("QRange", "Q Range").SetValue(new Circle(true, System.Drawing.Color.FromArgb(255, 255, 255, 255))));
            configMenu.SubMenu("Drawing").AddItem(new MenuItem("WRange", "W Range").SetValue(new Circle(false, System.Drawing.Color.FromArgb(255, 255, 255, 255))));
            configMenu.SubMenu("Drawing").AddItem(new MenuItem("ERange", "E Range").SetValue(new Circle(true, System.Drawing.Color.FromArgb(255, 255, 255, 255))));


            configMenu.AddSubMenu(new Menu("KeyBindings", "KeyBindings"));
            configMenu.SubMenu("KeyBindings").AddItem(new MenuItem("ComboKey", "ComboKey").SetValue(new KeyBind(32, KeyBindType.Press, false)));
            configMenu.SubMenu("KeyBindings").AddItem(new MenuItem("HarassKey", "HarassKey").SetValue(new KeyBind("V".ToCharArray()[0], KeyBindType.Press, false)));
            configMenu.SubMenu("KeyBindings").AddItem(new MenuItem("HarassToggleKey", "HarassToggleKey").SetValue(new KeyBind("G".ToCharArray()[0], KeyBindType.Toggle, false)));


            configMenu.AddToMainMenu();   
        }

        private static void InitEvent()
        {
            Game.OnGameUpdate += Game_OnGameUpdate;
            Obj_AI_Hero.OnProcessSpellCast += Obj_AI_Hero_OnProcessSpellCast;
            Obj_AI_Base.OnIssueOrder += Obj_AI_Base_OnIssueOrder;
            Orbwalking.AfterAttack += Orbwalking_AfterAttack;
            Orbwalking.BeforeAttack += Orbwalking_BeforeAttack;
            Drawing.OnDraw += Drawing_OnDraw;
            
        }
        #endregion

        #region Event
        private static void Game_OnGameUpdate(EventArgs args)
        {   
            if (configMenu.SubMenu("KeyBindings").Item("ComboKey").GetValue<KeyBind>().Active)
                Combo();
            else if (configMenu.SubMenu("KeyBindings").Item("HarassKey").GetValue<KeyBind>().Active
                || configMenu.SubMenu("KeyBindings").Item("HarassToggleKey").GetValue<KeyBind>().Active)
            {
                Harass();
            }
            
            DateTime currentTime = DateTime.Now;
            TimeSpan currentSpan = new TimeSpan(currentTime.Ticks);
            
            var muramana = player.GetSpellSlot("Muramana");
            if (player.HasBuff("Muramana") 
                && lastAttackSecond + 1.5 <= currentSpan.TotalSeconds
                && configMenu.SubMenu("Combo").Item("UseMuramana").GetValue<bool>())
            {
                player.Spellbook.CastSpell(muramana);
            }
                
        }

        static void Obj_AI_Hero_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {

            if (sender.IsMe && args.SData.Name.Equals("jayceshockblast"))
            {

                Vector3 ePosition = player.ServerPosition + Vector3.Normalize(args.End - player.ServerPosition) * 50;
                if (cannonE.IsReady())
                {
                    if (shotMode.Equals("Combo")
                        && configMenu.SubMenu("Combo").Item("UseCannonE").GetValue<bool>())
                    {
                        cancelMovt = true;
                        cannonE.Cast(ePosition, true);
                        shotMode = string.Empty;
                    }
                    else if (shotMode.Equals("Harass")
                        && configMenu.SubMenu("Harass").Item("UseCannonE").GetValue<bool>())
                    {
                        cancelMovt = true;
                        cannonE.Cast(ePosition, true);
                        shotMode = string.Empty;
                    }
                    else if (configMenu.SubMenu("Misc").Item("AutoGate").GetValue<bool>())
                    {
                        cancelMovt = true;
                        cannonE.Cast(ePosition, true);
                    }
                }
            }
            else if (sender.IsMe && args.SData.Name.Equals("jayceaccelerationgate"))
            {
                cancelMovt = false;
                blockCount = 0;
            }
            else if (sender.IsMe && args.SData.Name.Equals("jaycetotheskies"))
            {
                Obj_AI_Hero target = TargetSelector.GetTarget(hammerW.Range, TargetSelector.DamageType.Physical);

                if (target != null
                    && hammerW.IsReady())
                {
                    if (gapMode.Equals("Combo")
                        && configMenu.SubMenu("Combo").Item("UseHammerW").GetValue<bool>())
                    {
                        hammerW.Cast(true);
                        gapMode = string.Empty;
                    }
                }
            }

        }


        private static void Obj_AI_Base_OnIssueOrder(Obj_AI_Base sender, GameObjectIssueOrderEventArgs args)
        {
            
            if (sender == null || !sender.IsValid || !sender.IsMe)
            {
                return;
            }
            else
            {
                if (cancelMovt)
                {
                    blockCount++;

                    if (blockCount == 100)
                    {
                        cancelMovt = false;
                        blockCount = 0;
                    }
                    else
                        args.Process = false;
                }
            }
        }

        private static void Orbwalking_BeforeAttack(Orbwalking.BeforeAttackEventArgs args)
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan currentSpan = new TimeSpan(currentTime.Ticks);

            var muramana = player.GetSpellSlot("Muramana");

            if (args.Target.IsEnemy)
            {
                lastAttackSecond = currentSpan.TotalSeconds;
                if (muramana != SpellSlot.Unknown && !player.HasBuff("Muramana")
                    && configMenu.SubMenu("Combo").Item("UseMuramana").GetValue<bool>()
                    && player.ManaPercentage() > configMenu.SubMenu("Combo").Item("MuramanaMana").GetValue<Slider>().Value)
                {
                    player.Spellbook.CastSpell(muramana);
                }
                else if (player.HasBuff("Muramana")
                    && player.ManaPercentage() <= configMenu.SubMenu("Combo").Item("MuramanaMana").GetValue<Slider>().Value)
                {
                    player.Spellbook.CastSpell(muramana);
                }
            }
            else
            {
                if (player.HasBuff("Muramana")
                    && configMenu.SubMenu("Combo").Item("UseMuramana").GetValue<bool>())
                {
                    player.Spellbook.CastSpell(muramana);
                }
            }
                
        }

        private static void Orbwalking_AfterAttack(AttackableUnit unit, AttackableUnit target)
        {
            bool useCannonWFlag = configMenu.SubMenu("Combo").Item("UseCannonW").GetValue<bool>();
            bool isHammerForm = getSpellData.SData.Name.Equals("jayceshockblast") ? false : true;
            bool comboFlag = configMenu.SubMenu("KeyBindings").Item("ComboKey").GetValue<KeyBind>().Active;
            
            if (!isHammerForm && comboFlag && useCannonWFlag && cannonW.IsReady() && unit.IsMe)
            {
                Orbwalking.ResetAutoAttackTimer();
                cannonW.Cast();
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            bool isHammerForm = getSpellData.SData.Name.Equals("jayceshockblast") ? false : true;
            var drawingQ = configMenu.SubMenu("Drawing").Item("QRange").GetValue<Circle>();
            if (drawingQ.Active)
            {
                if (isHammerForm)
                    Utility.DrawCircle(player.Position, hammerQ.Range, drawingQ.Color);
                else
                    Utility.DrawCircle(player.Position, cannonQ.Range, drawingQ.Color);
            }

            var drawingW = configMenu.SubMenu("Drawing").Item("WRange").GetValue<Circle>();
            if (drawingW.Active)
            {
                if (isHammerForm)
                    Utility.DrawCircle(player.Position, hammerW.Range, drawingQ.Color);
                else
                    Utility.DrawCircle(player.Position, cannonW.Range, drawingQ.Color);
            }

            var drawingE = configMenu.SubMenu("Drawing").Item("ERange").GetValue<Circle>();
            if (drawingE.Active)
            {
                if (isHammerForm)
                    Utility.DrawCircle(player.Position, hammerE.Range, drawingE.Color);
                else
                    Utility.DrawCircle(player.Position, cannonE.Range, drawingE.Color);
            }
        }

        #endregion

        #region Method
        private static SpellDataInst getSpellData = player.Spellbook.GetSpell(SpellSlot.Q);
        private static void Combo()
        {
            Orbwalker.SetAttack(true);
            
            bool useCannonQFlag = configMenu.SubMenu("Combo").Item("UseCannonQ").GetValue<bool>();
            bool useCannonWFlag = configMenu.SubMenu("Combo").Item("UseCannonW").GetValue<bool>();
            bool useCannonEFlag = configMenu.SubMenu("Combo").Item("UseCannonE").GetValue<bool>();
            bool useCannonRFlag = configMenu.SubMenu("Combo").Item("UseCannonR").GetValue<bool>();

            bool useHammerQFlag = configMenu.SubMenu("Combo").Item("UseHammerQ").GetValue<bool>();
            bool useHammerWFlag = configMenu.SubMenu("Combo").Item("UseHammerW").GetValue<bool>();
            bool useHammerEFlag = configMenu.SubMenu("Combo").Item("UseHammerE").GetValue<bool>();
            bool useHammerRFlag = configMenu.SubMenu("Combo").Item("UseHammerR").GetValue<bool>();

            bool isHammerForm = getSpellData.SData.Name.Equals("jayceshockblast") ? false : true;


            if (isHammerForm)
            {
                Obj_AI_Base target = (TargetSelector.GetTarget(hammerQ.Range, TargetSelector.DamageType.Physical)) as Obj_AI_Base;
                
                if (target != null && hammerQ.IsReady())
                {
                    if (player.Distance(target) < hammerQ.Range && hammerQ.IsReady() && useHammerQFlag)
                    {
                        hammerQ.CastOnUnit(target, true);
                        gapMode = "Combo";
                    }
                }
                else if (target != null)
                {

                    if (player.Distance(target) > hammerRange)
                    {
                        if ((player.Distance(target) + 80 <= hammerE.Range && hammerE.IsReady() && useHammerEFlag) && !hammerQ.IsReady())
                        {
                            hammerE.CastOnUnit(target, true);
                        }
                    }
                    else if (player.GetSpellDamage(target, SpellSlot.E) >= target.Health
                            && hammerE.IsReady() && useHammerEFlag && player.Distance(target) <= hammerE.Range)
                    {
                        hammerE.CastOnUnit(target, true);
                    }

                    if (!hammerQ.IsReady() && player.Distance(target) < cannonQ2.Range && useHammerQFlag)
                    {
                        if (hammerW.IsReady() && useHammerWFlag)
                            hammerW.Cast(true);

                        if (R.IsReady() && useHammerRFlag)
                        {
                            if (!hammerE.IsReady() && useHammerEFlag)
                                R.Cast(true);
                            else if (!useHammerEFlag)
                                R.Cast(true);
                        }
                    }
                    else if (!useHammerQFlag)
                    {
                        if (!hammerE.IsReady() && useHammerEFlag)
                            R.Cast(true);
                        else if (!useHammerEFlag)
                            R.Cast(true);
                    }
                }
                else
                    return;
                
            }
            else
            {
                var target = TargetSelector.GetTarget(cannonQ2.Range, TargetSelector.DamageType.Physical);
                var target2 = TargetSelector.GetTarget(cannonQ.Range, TargetSelector.DamageType.Physical);
                var target3 = TargetSelector.GetTarget(hammerQ.Range, TargetSelector.DamageType.Physical);

                if (target != null && cannonQ2.IsReady() && cannonE.IsReady() && useCannonQFlag && useCannonEFlag)
                {
                    var pred = cannonQ2.GetPrediction(target);

                    if (pred.Hitchance >= HitChance.High)
                    {
                        cannonQ2.Cast(pred.CastPosition, true);
                        shotMode = "Combo";
                    }
                }

                else if (target2 != null && cannonQ.IsReady() && useCannonQFlag)
                {
                    var pred = cannonQ.GetPrediction(target2);

                    if (pred.Hitchance >= HitChance.High)
                        cannonQ.Cast(pred.CastPosition, true);
                    
                }


                if (target3 != null && useCannonRFlag)
                {
                    if (!cannonQ.IsReady() && useCannonQFlag)
                    {
                        if (!cannonW.IsReady() && useCannonWFlag)
                            R.Cast(true);
                        else if (!useCannonWFlag)
                            R.Cast(true);
                    }
                    else if (!useCannonQFlag)
                    {
                        if (!cannonW.IsReady() && useCannonWFlag)
                            R.Cast(true);
                        else if (!useCannonWFlag)
                            R.Cast(true);
                    }
                }
            }
        }

        private static void Harass()
        {
            bool useCannonQFlag = configMenu.SubMenu("Harass").Item("UseCannonQ").GetValue<bool>();
            bool useCannonEFlag = configMenu.SubMenu("Harass").Item("UseCannonE").GetValue<bool>();

            var target = TargetSelector.GetTarget(cannonQ2.Range, TargetSelector.DamageType.Physical);
            var target2 = TargetSelector.GetTarget(cannonQ.Range, TargetSelector.DamageType.Physical);

            if (target != null && cannonQ2.IsReady() && cannonE.IsReady() && useCannonQFlag && useCannonEFlag)
            {
                var pred = cannonQ2.GetPrediction(target);

                if (pred.Hitchance >= HitChance.High)
                {
                    cannonQ2.Cast(pred.CastPosition, true);
                    shotMode = "Harass";
                }
            }
            else if (target2 != null && cannonQ.IsReady() && useCannonQFlag)
            {
                var pred = cannonQ.GetPrediction(target2);

                if (pred.Hitchance >= HitChance.High)
                    cannonQ.Cast(pred.CastPosition, true);

            }
        }
        #endregion
    }
}
