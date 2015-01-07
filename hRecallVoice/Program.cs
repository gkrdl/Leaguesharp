using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using System.Speech;
using System.Speech.Synthesis;
using System.Threading;

namespace hRecallVoice
{
    class Program
    {
        private readonly IDictionary<Obj_AI_Hero, RecallInfo> _recallInfo = new Dictionary<Obj_AI_Hero, RecallInfo>();
        private static Program _instance;
        private static SpeechSynthesizer tts = new SpeechSynthesizer();
        private static string speakMsg = string.Empty;

        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            tts.Volume = 100;
            int i = 0;
            foreach (Obj_AI_Hero hero in
    ObjectManager.Get<Obj_AI_Hero>().Where(hero => hero.Team != ObjectManager.Player.Team))
            {
                RecallInfo recallInfo = new RecallInfo(hero, i++);
                Program.Instance()._recallInfo[hero] = recallInfo;
            }
            //_recallInfo[ObjectManager.Player] = new RecallInfo(ObjectManager.Player,i);
            Program.Instance().Print("Recall Voice Loaded!");
        }

        

        public class RecallInfo
        {
            public const int GapTextBar = 10;


            private readonly Obj_AI_Hero _hero;
            private int _duration;
            private float _begin;
            private bool _active;
            private readonly int _index;
            public RecallInfo(Obj_AI_Hero hero, int index)
            {
                _hero = hero;
                _index = index;
                Obj_AI_Base.OnTeleport += Obj_AI_Base_OnTeleport;
            }

            private void Obj_AI_Base_OnTeleport(GameObject sender, GameObjectTeleportEventArgs args)
            {

                Packet.S2C.Teleport.Struct decoded = Packet.S2C.Teleport.Decoded(sender, args);
                if (decoded.UnitNetworkId == _hero.NetworkId && decoded.Type == Packet.S2C.Teleport.Type.Recall)
                {
                    switch (decoded.Status)
                    {
                        case Packet.S2C.Teleport.Status.Start:
                            _begin = Game.ClockTime;
                            _duration = decoded.Duration;
                            _active = true;
                            break;
                        case Packet.S2C.Teleport.Status.Finish:
                            speakMsg = _hero.ChampionName + " has recalled.";
                            Thread tr = new Thread(new ThreadStart(TSSpeak));
                            tr.Start();
                            Program.Instance().Notify(_hero.ChampionName + " has recalled.");
                            _active = false;
                            break;
                        case Packet.S2C.Teleport.Status.Abort:
                            _active = false;
                            break;
                        case Packet.S2C.Teleport.Status.Unknown:
                            Program.Instance()
                                .Notify(
                                    _hero.ChampionName + " is <font color='#ff3232'>unknown</font> (" +
                                    _hero.Spellbook.GetSpell(SpellSlot.Recall).Name + ")");
                            _active = false;
                            break;
                    }
                }
            }

            private void TSSpeak()
            {
                if (tts.State == SynthesizerState.Ready)
                    tts.Speak(speakMsg);
            }
        }

        public static Program Instance()
        {
            if (_instance == null)
            {
                return new Program();
            }
            return _instance;
        }

        public void Notify(string msg)
        {
            Print(msg);
        }


        public void Print(string msg)
        {
            msg = "<font color='#FFFFFF'>" + msg + "</font>";
            Game.PrintChat(msg);
        }
    }
}
