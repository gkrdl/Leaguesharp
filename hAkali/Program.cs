using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using System.Timers;

namespace hAkali
{
    class Program
    {
        #region Member
        private const string champName = "Akali";
        private static Obj_AI_Hero player = ObjectManager.Player;
        private static Spell Q, W, E, R;
        private static Items.Item cutlass;  //3144
        private static Items.Item gunblade; //3146


        private static Menu configMenu;
        private static Orbwalking.Orbwalker Orbwalker;


        private static int remainingTime = 8;
        private static Timer wTimer = new Timer();
        #endregion

        #region Initialize
        public static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += new CustomEvents.Game.OnGameLoaded(Game_OnGameLoad);
        }

        private static void Game_OnGameLoad(EventArgs args)
        {


            if (!player.ChampionName.Equals(champName))
            {
                Game.PrintChat("hAkali Error. Character name is " + player.ChampionName);
                return;
            }
            else
                Game.PrintChat("hAkali Loaded.");

            InitDefault();

            InitEvent();

        }

        private static void InitDefault()
        {
            //Spells Setting
            Q = new Spell(SpellSlot.Q, 600);
            W = new Spell(SpellSlot.W, 700);
            E = new Spell(SpellSlot.E, 325);
            R = new Spell(SpellSlot.R, 800);


            //Items Setting
            cutlass = new Items.Item(3144, 450);
            gunblade = new Items.Item(3146, 700);

            
            //Menu Setting
            configMenu = new Menu("hAkali", "hAkali", true);
            
            configMenu.AddSubMenu(new Menu("Target Selector", "Target Selector"));
            TargetSelector.AddToMenu(configMenu.SubMenu("Target Selector"));

            configMenu.AddSubMenu(new Menu("Orbwalker", "Orbwalker"));
            Orbwalker = new Orbwalking.Orbwalker(configMenu.SubMenu("Orbwalker"));

            configMenu.AddSubMenu(new Menu("Combo", "Combo"));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("Use Q", "Use Q").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("Use W", "Use W").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("Use E", "Use E").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("Use R", "Use R").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("Only Use R", "use R, when Q is available").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("Ignite", "Use Ignite").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("Cutlass", "Use Cutlass").SetValue(true));
            configMenu.SubMenu("Combo").AddItem(new MenuItem("Gunblade", "Use Gunblade").SetValue(true));

            configMenu.AddSubMenu(new Menu("Harass", "Harass"));
            configMenu.SubMenu("Harass").AddItem(new MenuItem("Use Q", "Use Q").SetValue(true));
            configMenu.SubMenu("Harass").AddItem(new MenuItem("Use E", "Use E").SetValue(true));

            configMenu.AddSubMenu(new Menu("Misc", "Misc"));
            configMenu.SubMenu("Misc").AddItem(new MenuItem("PacketCast", "PacketCast").SetValue(true));

            configMenu.AddSubMenu(new Menu("Drawing", "Drawing"));
            configMenu.SubMenu("Drawing").AddItem(new MenuItem("Q Range", "Q Range").SetValue(new Circle(true, System.Drawing.Color.FromArgb(255, 255, 255, 255))));
            configMenu.SubMenu("Drawing").AddItem(new MenuItem("W Range", "W Range").SetValue(new Circle(false, System.Drawing.Color.FromArgb(255, 255, 255, 255))));
            configMenu.SubMenu("Drawing").AddItem(new MenuItem("E Range", "E Range").SetValue(new Circle(true, System.Drawing.Color.FromArgb(255, 255, 255, 255))));
            configMenu.SubMenu("Drawing").AddItem(new MenuItem("R Range", "R Range").SetValue(new Circle(true, System.Drawing.Color.FromArgb(255, 255, 255, 255))));


            configMenu.AddSubMenu(new Menu("KeyBindings", "KeyBindings"));
            configMenu.SubMenu("KeyBindings").AddItem(new MenuItem("ComboKey", "ComboKey").SetValue(new KeyBind(32, KeyBindType.Press, false)));
            configMenu.SubMenu("KeyBindings").AddItem(new MenuItem("HarassKey", "HarassKey").SetValue(new KeyBind(76, KeyBindType.Press, false)));
            configMenu.SubMenu("KeyBindings").AddItem(new MenuItem("HarassToggleKey", "HarassToggleKey").SetValue(new KeyBind("L".ToCharArray()[0], KeyBindType.Toggle, false)));
            

            configMenu.AddToMainMenu();

            wTimer.Interval = 1;
        }

        private static void InitEvent()
        {
            Drawing.OnDraw += Drawing_OnDraw;
            Game.OnGameUpdate += Game_OnGameUpdate;
            wTimer.Elapsed += wTimer_Elapsed;
        }



        #endregion

        #region Event
        private static void wTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (remainingTime > 0)
                remainingTime--;
            else
            {
                remainingTime = 8;

                wTimer.Stop();
            }

        }

        private static void Drawing_OnDraw(EventArgs args)
        {


            var drawingQ = configMenu.SubMenu("Drawing").Item("Q Range").GetValue<Circle>();
            if (drawingQ.Active) Utility.DrawCircle(player.Position, Q.Range, drawingQ.Color);

            var drawingE = configMenu.SubMenu("Drawing").Item("E Range").GetValue<Circle>();
            if (drawingE.Active) Utility.DrawCircle(player.Position, E.Range, drawingE.Color);

            var drawingR = configMenu.SubMenu("Drawing").Item("R Range").GetValue<Circle>();
            if (drawingR.Active) Utility.DrawCircle(player.Position, R.Range, drawingR.Color);

            if (remainingTime < 8)
                Drawing.DrawText(Drawing.Width * 0.44f, Drawing.Height * 0.7f, System.Drawing.Color.GreenYellow, remainingTime.ToString());
            
            
        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            Orbwalker.SetAttack(true);
            if (configMenu.SubMenu("KeyBindings").Item("ComboKey").GetValue<KeyBind>().Active)
                Combo();
            else if (configMenu.SubMenu("KeyBindings").Item("HarassKey").GetValue<KeyBind>().Active
                || configMenu.SubMenu("KeyBindings").Item("HarassToggleKey").GetValue<KeyBind>().Active)
            {
                Harass();
            }
        }
        #endregion

        #region Method
        private static void Combo()
        {
            Obj_AI_Hero qTarget = TargetSelector.GetTarget(Q.Range, 0);
            Obj_AI_Hero rTarget = TargetSelector.GetTarget(R.Range, 0);


            Orbwalker.SetAttack(true);
            bool gunbladeFlag = configMenu.SubMenu("Combo").Item("Gunblade").GetValue<bool>();
            bool cutlassFlag = configMenu.SubMenu("Combo").Item("Cutlass").GetValue<bool>();
            bool useQFlag = configMenu.SubMenu("Combo").Item("Use Q").GetValue<bool>();
            bool useWFlag = configMenu.SubMenu("Combo").Item("Use W").GetValue<bool>();
            bool useEFlag = configMenu.SubMenu("Combo").Item("Use E").GetValue<bool>();
            bool useRFlag = configMenu.SubMenu("Combo").Item("Use R").GetValue<bool>();
            bool onlyUseRFlag = configMenu.SubMenu("Combo").Item("Only Use R").GetValue<bool>();


            if (!onlyUseRFlag)
            {
                if (qTarget != null)
                {
                    if (Geometry.Distance(player, qTarget) <= 600f)
                    {
                        if (Q.IsReady(0) && useQFlag)
                        {
                            Q.CastOnUnit(qTarget, true);
                        }
                        else if (R.IsReady(0) && Geometry.Distance(player, qTarget) <= 800f && useRFlag)
                        {
                            R.CastOnUnit(qTarget, true);

                            if (Q.IsReady(0) && useQFlag)
                                Q.CastOnUnit(qTarget, true);



                        }

                        if (E.IsReady(0) && Geometry.Distance(player, qTarget) <= 320f && useEFlag)
                            E.CastOnUnit(qTarget, true);
                        else if (R.IsReady(0) && Geometry.Distance(player, qTarget) <= 800f && useRFlag)
                            R.CastOnUnit(qTarget, true);

                        if (qTarget.Health <= qTarget.TotalHeal / 3)
                        {
                            if (cutlassFlag && cutlass.IsReady())
                                cutlass.Cast(qTarget);
                            if (gunbladeFlag && gunblade.IsReady())
                                gunblade.Cast(qTarget);

                        }


                        if (useWFlag && W.IsReady(0))
                        {
                            W.Cast(player.Position);
                            remainingTime = 8;
                            wTimer.Start();
                        }
                    }
                }
                else if (rTarget != null)
                {
                    if (Geometry.Distance(player, rTarget) <= 800f)
                    {
                        if (R.IsReady(0) && Geometry.Distance(player, rTarget) <= 800f && useRFlag)
                        {
                            R.CastOnUnit(rTarget, true);

                            if (Q.IsReady(0) && useQFlag)
                                Q.CastOnUnit(rTarget, true);


                            
                        }

                        if (E.IsReady(0) && Geometry.Distance(player, rTarget) <= 320 && useEFlag)
                            E.CastOnUnit(rTarget, true);
                        else if (R.IsReady(0) && Geometry.Distance(player, rTarget) <= 800f && useRFlag)
                            R.CastOnUnit(rTarget, true);

                        if (rTarget.Health <= rTarget.TotalHeal / 3)
                        {
                            if (cutlassFlag && cutlass.IsReady())
                                cutlass.Cast(rTarget);
                            if (gunbladeFlag && gunblade.IsReady())
                                gunblade.Cast(rTarget);

                        }

                        if (useWFlag && W.IsReady(0))
                        {
                            W.Cast(player.Position);
                            remainingTime = 8;
                            wTimer.Start();
                        }
                    }
                }
            }
            else
            {



                if (qTarget != null)
                {
                    if (Geometry.Distance(player, qTarget) <= 600f)
                    {
                        if (Q.IsReady(0) && useQFlag)
                        {
                            Q.CastOnUnit(qTarget, true);

                            if (R.IsReady(0) && useRFlag)
                                R.CastOnUnit(qTarget, true);

                            if (Q.IsReady(0) && useQFlag)
                                Q.CastOnUnit(qTarget, true);


                        }

                        if (E.IsReady(0) && Geometry.Distance(player, qTarget) <= 320f && useEFlag)
                            E.CastOnUnit(qTarget, true);
                        else if (R.IsReady(0) && Geometry.Distance(player, qTarget) <= 800f && useRFlag)
                            R.CastOnUnit(qTarget, true);

                        if (qTarget.Health <= qTarget.TotalHeal / 2)
                        {
                            if (cutlassFlag && cutlass.IsReady())
                                cutlass.Cast(qTarget);
                            if (gunbladeFlag && gunblade.IsReady())
                                gunblade.Cast(qTarget);

                        }

                        if (useWFlag && W.IsReady(0))
                        { 
                            W.Cast(player.Position);
                            remainingTime = 8;
                            wTimer.Start();
                        }
                            
                    }
                }
                else if (rTarget != null)
                {
                    if (Geometry.Distance(player, rTarget) <= 800f)
                    {
                        if (R.IsReady(0) && Geometry.Distance(player, rTarget) <= 800f && useRFlag)
                        {


                            if (Q.IsReady(0))
                            {
                                R.CastOnUnit(rTarget, true);

                                if (useQFlag)
                                    Q.CastOnUnit(rTarget, true);

                            }
                        }

                        
                        if (E.IsReady(0) && Geometry.Distance(player, rTarget) <= 320 && useEFlag)
                            E.CastOnUnit(rTarget, true);


                        if (rTarget.Health <= rTarget.TotalHeal / 2)
                        {
                            if (cutlassFlag && cutlass.IsReady())
                                cutlass.Cast(rTarget);
                            if (gunbladeFlag && gunblade.IsReady())
                                gunblade.Cast(rTarget);

                        }   

                        if (useWFlag && W.IsReady(0))
                        {
                            W.Cast(player.Position);
                            remainingTime = 8;
                            wTimer.Start();
                        }
                    }
                }
            }
        }

        private static void Harass()
        {
            var target = TargetSelector.GetTarget(Q.Range, 0);
            bool useQFlag = configMenu.SubMenu("Harass").Item("Use Q").GetValue<bool>();
            bool useEFlag = configMenu.SubMenu("Harass").Item("Use E").GetValue<bool>();

            if (target != null && useQFlag)
                Q.CastOnUnit(target);
            if (target != null && useEFlag && Geometry.Distance(player, target) <= 320f && E.IsReady())
                E.CastOnUnit(target);
        }
        #endregion
    }
}
