using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace hJayce
{
    class Program
    {
        #region Member
        private const string champName = "Jayce";
        private static Obj_AI_Hero player = ObjectManager.Player;

        private static Menu configMenu;
        private static bool cancelMovt = false;

        private static Spell E;

        private static int blockCount = 0;
        #endregion

        #region Initialize
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            if (!player.ChampionName.ToUpper().Equals(champName.ToUpper()))
            {
                Game.PrintChat("hJayce Error. Character name is " + player.ChampionName);
                return;
            }
            else
            {
                InitDefault();
                InitEvent();
                Game.PrintChat("hJayce Loaded."); 
            }
              
        }

        private static void InitDefault()
        {
            E = new Spell(SpellSlot.E, 200);
        }

        private static void InitEvent()
        {
            Obj_AI_Hero.OnProcessSpellCast += Obj_AI_Hero_OnProcessSpellCast;
            Obj_AI_Base.OnIssueOrder += Obj_AI_Base_OnIssueOrder;
        }
        #endregion

        #region Event
        static void Obj_AI_Hero_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {

            if (sender.IsMe && args.SData.Name.Equals("jayceshockblast"))
            {

                Vector3 ePosition = player.ServerPosition + Vector3.Normalize(args.End - player.ServerPosition) * 50;
                if (E.IsReady())
                {
                    cancelMovt = true;
                    //player.Spellbook.CastSpell(SpellSlot.E, ePosition);
                    E.Cast(ePosition, true);
                }
            }
            else if (sender.IsMe && args.SData.Name.Equals("jayceaccelerationgate"))
            {
                cancelMovt = false;
                blockCount = 0;
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

                    if (blockCount == 3)
                    {
                        cancelMovt = false;
                        blockCount = 0;
                    }
                    else
                        args.Process = false;
                }


            }

            //blockCount++;

            //if (blockCount == 100)
            //{
            //    cancelMovt = false;
            //    blockCount = 0;
            //}
            //else
            //    packet.Block = true;
        }
        #endregion
    }
}
