using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using hAram.Champions;
using System.Reflection;

namespace hAram
{
    internal class Program
    {
        #region 멤버, 변수
        private static Spell Q;
        private static Spell W;
        private static Spell E;
        private static Spell R;

        //private static Vector3[] buffs = { new Vector3(8922, 10, 7868), new Vector3(7473, 10, 6617), new Vector3(5929, 10, 5190), new Vector3(4751, 10, 3901)};
        private static Obj_AI_Hero Player = ObjectHandler.Player;
        #endregion

        #region 초기화
        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {

            #region Load Campions
            switch (Player.ChampionName.ToLowerInvariant())
            {
                default:
                    new annie();
                    break;
                case "aatrox":
                    new aatrox();
                    break;
                case "ahri":
                    new ahri();
                    break;
                case "akali":
                    new akali();
                    break;
                case "alistar":
                    new alistar();
                    break;
                case "amumu":
                    new amumu();
                    break;
                case "anivia":
                    new anivia();
                    break;
                case "annie":
                    new annie();
                    break;
                case "ashe":
                    new ashe();
                    break;
                case "bard":
                    new bard();
                    break;
                case "blitzcrank":
                    new blitzcrank();
                    break;
                case "brand":
                    new brand();
                    break;
                case "braum":
                    new braum();
                    break;
                case "caitlyn":
                    new caitlyn();
                    break;
                case "cassiopeia":
                    new cassiopeia();
                    break;
                case "chogath":
                    new chogath();
                    break;
                case "corki":
                    new corki();
                    break;
                case "darius":
                    new darius();
                    break;
                case "diana":
                    new diana();
                    break;
                case "draven":
                    new draven();
                    break;
                case "drmundo":
                    new drmundo();
                    break;
                case "elise":
                    new elise();
                    break;
                case "evelynn":
                    new evelynn();
                    break;
                case "ezreal":
                    new ezreal();
                    break;
                case "fiddlesticks":
                    new fiddlesticks();
                    break;
                case "fiora":
                    new fiora();
                    break;
                case "fizz":
                    new galio();
                    break;
                case "gangplank":
                    new gangplank();
                    break;
                case "garen":
                    new garen();
                    break;
                case "gnar":
                    new gnar();
                    break;
                case "gragas":
                    new gragas();
                    break;
                case "graves":
                    new graves();
                    break;
                case "hecarim":
                    new hecarim();
                    break;
                case "heimerdinger":
                    new heimerdinger();
                    break;
                case "irelia":
                    new irelia();
                    break;
                case "janna":
                    new janna();
                    break;
                case "jarvaniv":
                    new jarvaniv();
                    break;
                case "jax":
                    new jax();
                    break;
                case "jayce":
                    new jayce();
                    break;
                case "jinx":
                    new jinx();
                    break;
                case "kalista":
                    new kalista();
                    break;
                case "karma":
                    new karma();
                    break;
                case "karthus":
                    new karthus();
                    break;
                case "kassadin":
                    new kassadin();
                    break;
                case "katarina":
                    new katarina();
                    break;
                case "kayle":
                    new kennen();
                    break;
                case "khazix":
                    new khazix();
                    break;
                case "kogmaw":
                    new kogmaw();
                    break;
                case "leblanc":
                    new leblanc();
                    break;
                case "leesin":
                    new leesin();
                    break;
                case "leona":
                    new leona();
                    break;
                case "lissandra":
                    new lissandra();
                    break;
                case "lucian":
                    new lucian();
                    break;
                case "lux":
                    new lux();
                    break;
                case "malphite":
                    new malphite();
                    break;
                case "malzahar":
                    new malzahar();
                    break;
                case "maokai":
                    new maokai();
                    break;
                case "masteryi":
                    new masteryi();
                    break;
                case "missfortune":
                    new missfortune();
                    break;
                case "mordekaiser":
                    new mordekaiser();
                    break;
                case "morgana":
                    new morgana();
                    break;
                case "nami":
                    new nami();
                    break;
                case "nasus":
                    new nasus();
                    break;
                case "nautilus":
                    new nautilus();
                    break;
                case "nidalee":
                    new nidalee();
                    break;
                case "nocturne":
                    new nocturne();
                    break;
                case "nunu":
                    new nunu();
                    break;
                case "olaf":
                    new olaf();
                    break;
                case "orianna":
                    new orianna();
                    break;
                case "pantheon":
                    new pantheon();
                    break;
                case "poppy":
                    new poppy();
                    break;
                case "quinn":
                    new quinn();
                    break;
                case "rammus":
                    new rammus();
                    break;
                case "reksai":
                    new reksai();
                    break;
                case "renekton":
                    new renekton();
                    break;
                case "rengar":
                    new rengar();
                    break;
                case "riven":
                    new riven();
                    break;
                case "rumble":
                    new rumble();
                    break;
                case "ryze":
                    new ryze();
                    break;
                case "sejuani":
                    new sejuani();
                    break;
                case "shaco":
                    new shaco();
                    break;
                case "shen":
                    new shen();
                    break;
                case "shyvana":
                    new shyvana();
                    break;
                case "singed":
                    new singed();
                    break;
                case "sion":
                    new sion();
                    break;
                case "sivir":
                    new sivir();
                    break;
                case "skarner":
                    new skarner();
                    break;
                case "sona":
                    new sona();
                    break;
                case "soraka":
                    new soraka();
                    break;
                case "swain":
                    new swain();
                    break;
                case "syndra":
                    new syndra();
                    break;
                case "talon":
                    new talon();
                    break;
                case "taric":
                    new taric();
                    break;
                case "teemo":
                    new teemo();
                    break;
                case "thresh":
                    new thresh();
                    break;
                case "trundle":
                    new trundle();
                    break;
                case "tryndamere":
                    new tryndamere();
                    break;
                case "twistedfate":
                    new twistedfate();
                    break;
                case "twitch":
                    new twitch();
                    break;
                case "udyr":
                    new udyr();
                    break;
                case "urgot":
                    new urgot();
                    break;
                case "varus":
                    new varus();
                    break;
                case "vayne":
                    new vayne();
                    break;
                case "veigar":
                    new veigar();
                    break;
                case "velkoz":
                    new velkoz();
                    break;
                case "vi":
                    new vi();
                    break;
                case "viktor":
                    new viktor();
                    break;
                case "vladimir":
                    new vladimir();
                    break;
                case "volibear":
                    new volibear();
                    break;
                case "warwick":
                    new warwick();
                    break;
                case "wukong":
                    new wukong();
                    break;
                case "xerath":
                    new xerath();
                    break;
                case "xinzhao":
                    new xinzhao();
                    break;
                case "yasuo":
                    new yasuo();
                    break;
                case "yorick":
                    new yorick();
                    break;
                case "zac":
                    new zac();
                    break;
                case "zed":
                    new zed();
                    break;
                case "ziggs":
                    new ziggs();
                    break;
                case "zilean":
                    new zilean();
                    break;
                case "zyra":
                    new zyra();
                    break;
            }
            #endregion

        }
        #endregion
        
    }
}

