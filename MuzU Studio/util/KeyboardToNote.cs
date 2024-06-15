using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzU_Studio.util;

public class KeyboardToNote
{
    public static readonly Dictionary<System.Windows.Input.Key, int> KeyMap = new()
    {
        { System.Windows.Input.Key.Q, 0 },
        { System.Windows.Input.Key.W, 1 },
        { System.Windows.Input.Key.E, 2 },
        { System.Windows.Input.Key.R, 3 },
        { System.Windows.Input.Key.T, 4 },
        { System.Windows.Input.Key.Y, 5 },
        { System.Windows.Input.Key.U, 6 },
        { System.Windows.Input.Key.I, 7 },
        { System.Windows.Input.Key.O, 8 },
        { System.Windows.Input.Key.P, 9 },
        { System.Windows.Input.Key.OemOpenBrackets, 10 }, // [
        { System.Windows.Input.Key.OemCloseBrackets, 11 }, // ]
        { System.Windows.Input.Key.A, 12 },
        { System.Windows.Input.Key.S, 13 },
        { System.Windows.Input.Key.D, 14 },
        { System.Windows.Input.Key.F, 15 },
        { System.Windows.Input.Key.G, 16 },
        { System.Windows.Input.Key.H, 17 },
        { System.Windows.Input.Key.J, 18 },
        { System.Windows.Input.Key.K, 19 },
        { System.Windows.Input.Key.L, 20 },
        { System.Windows.Input.Key.OemSemicolon, 21 }, // ;
        { System.Windows.Input.Key.OemQuotes, 22 }, // '
        { System.Windows.Input.Key.Z, 23 },
        { System.Windows.Input.Key.X, 24 },
        { System.Windows.Input.Key.C, 25 },
        { System.Windows.Input.Key.V, 26 },
        { System.Windows.Input.Key.B, 27 },
        { System.Windows.Input.Key.N, 28 },
        { System.Windows.Input.Key.M, 29 },
        { System.Windows.Input.Key.OemComma, 30 }, // ,
        { System.Windows.Input.Key.OemPeriod, 31 }, // .
        { System.Windows.Input.Key.OemBackslash, 32 }, // /
    };

    public static int Convert(System.Windows.Input.Key key)
    {
        return KeyMap.ContainsKey(key) ? KeyMap[key] + 48 : -1;
    }
}
