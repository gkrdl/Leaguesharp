using LeagueSharp.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace hAram.Libraries
{
    public class ChampSpellData
    {       
        public static List<String> ChampionNames = new List<string>();
        public static List<String> SpellSlots = new List<string>();
        public static List<String> Types = new List<string>();
        public static List<String> Delays = new List<string>();
        public static List<String> Ranges = new List<string>();
        public static List<String> Widths = new List<string>();
        public static List<String> Speeds = new List<string>();
        public static List<Info> Infos = new List<Info>();
        public static string appversion = "";
        public class Info
        {
            public string Name;
            public string SlotName, Type, Delay, Range, Width, Speed;
        }

        public ChampSpellData()
        {
            WebClient wc = new WebClient();
            string downPath = Path.GetTempPath() + Environment.TickCount + ".txt";
            
            //var db = wc.OpenRead("https://raw.githubusercontent.com/gkrdl/LeagueSharp/master/misiledb.txt");
            wc.DownloadFile("https://raw.githubusercontent.com/gkrdl/LeagueSharp/master/misiledb.txt", downPath);

            var db = File.ReadLines(downPath);

            foreach (var t in db.Where(tx => tx.StartsWith("ChampionName")))
            {
                ChampionNames.Add(t.Replace("ChampionName_", ""));
            }
            foreach (var t in db.Where(tx => tx.StartsWith("Slot_SpellSlot.")))
            {
                SpellSlots.Add(t.Replace("Slot_SpellSlot.", ""));
            }
            foreach (var t in db.Where(tx => tx.StartsWith("Type_")))
            {
                Types.Add(t.Replace("Type_SkillShotType.", ""));
            }
            foreach (var t in db.Where(tx => tx.StartsWith("Delay_")))
            {
                Delays.Add(t.Replace("Delay_", ""));
            }
            foreach (var t in db.Where(tx => tx.StartsWith("Range_")))
            {
                Ranges.Add(t.Replace("Range_", ""));
            }
            foreach (var t in db.Where(tx => tx.StartsWith("Radius_")))
            {
                Widths.Add(t.Replace("Radius_", ""));
            }
            foreach (var t in db.Where(tx => tx.StartsWith("MissileSpeed_")))
            {
                Speeds.Add(t.Replace("MissileSpeed_", ""));
            }
            foreach (var t in db.Where(tx => tx.StartsWith("AppVersion_")))
            {
                appversion = t.Replace("AppVersion_", "");
            }


            for (int i = 0; true; i++)
            {
                try
                {
                    Info temp = new Info();
                    temp.Name = ChampionNames[i];
                    if (i >= 1 && ChampionNames[i - 1] == ChampionNames[i] && SpellSlots[i] == SpellSlots[i - 1])
                    {
                        temp.SlotName = SpellSlots[i] + "2";
                    }
                    else
                    {
                        temp.SlotName = SpellSlots[i];
                    }
                    temp.Type = Types[i];
                    temp.Delay = Delays[i];
                    temp.Range = Ranges[i];
                    temp.Width = Widths[i];
                    temp.Speed = Speeds[i];
                    Infos.Add(temp);
                }
                catch
                {
                    break;
                }
            }
        }

        public List<Spell> GetSpellData(string champName, LeagueSharp.SpellSlot spellSlot, string slotName)
        {
            List<Spell> lstSpell = new List<Spell>();
            foreach (var t in Infos)
            {
                if (t.Name.ToLower() == champName.ToLower()
                    && t.SlotName.ToLower() == slotName.ToLower())
                {
                    Spell tempSpell = new Spell(spellSlot, float.Parse(t.Range));
                    tempSpell.Delay = float.Parse(t.Delay);
                    tempSpell.Width = float.Parse(t.Width);
                    tempSpell.Speed = float.Parse(t.Speed);

                    lstSpell.Add(tempSpell);
                    break;
                }
            }

            return lstSpell;

        }
    }
}
