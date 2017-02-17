using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Console;

namespace ColorfulBackground {
  class Program {

    [DllImport("kernel32.dll", ExactSpelling = true)]

    private static extern IntPtr GetConsoleWindow();

    private static IntPtr ThisConsole = GetConsoleWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int HIDE = 0;
    private const int MAXIMIZE = 3;
    private const int MINIMIZE = 6;
    private const int RESTORE = 9;

    static void Main(string[] args) {
      ShowWindow(ThisConsole, MAXIMIZE);
      BuildSymbolColorPage();

    }

    //private static void OnTimedEvent(object source, ElapsedEventArgs e) {
    //  var currentProcess = Process.GetCurrentProcess();
    //  currentProcess.CloseMainWindow();
    //}

    public static void BuildSymbolColorPage() {
      //System.Timers.Timer aTimer = new System.Timers.Timer();
      //aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
      //aTimer.Interval = 20000;
      //aTimer.Enabled = true;

      Stopwatch myStopWatch = new Stopwatch();
      myStopWatch.Start();

      var randomNumber = new Random();

      //remove Cursor Visibility and create alphanumeric array.
      CursorVisible = false;
      string[] alphaNumeric = {
        "a", "b", "c", "d", "e", "f", "g", "h", "i",
        "j", "k", "l", "m", "n", "o", "p", "q", "r",
        "s", "t", "u", "v", "w", "x", "y", "z", "0",
        "1", "2", "3", "4", "5", "6", "7", "8", "9"
      };

      Dictionary<int, object> myColorList = new Dictionary<int, object>();

      //myColorList.Add(0, ConsoleColor.DarkBlue);
      //myColorList.Add(1, ConsoleColor.DarkCyan);
      //myColorList.Add(2, ConsoleColor.DarkGray);
      //myColorList.Add(3, ConsoleColor.DarkGreen);
      //myColorList.Add(4, ConsoleColor.DarkMagenta);
      //myColorList.Add(5, ConsoleColor.DarkRed);
      //myColorList.Add(6, ConsoleColor.DarkYellow);
      //myColorList.Add(7, ConsoleColor.DarkGreen);
      //myColorList.Add(8, ConsoleColor.Gray);
      //myColorList.Add(13, ConsoleColor.Black);
      myColorList.Add(9, ConsoleColor.White);
      myColorList.Add(10, ConsoleColor.Yellow);
      myColorList.Add(11, ConsoleColor.Magenta);
      myColorList.Add(12, ConsoleColor.Green);
      myColorList.Add(14, ConsoleColor.Blue);
      myColorList.Add(15, ConsoleColor.Cyan);

      var symbolPositionMap = new string[198, 53];

      var currentAlphaNumbericSymbol = string.Empty;

      while (myStopWatch.Elapsed <= TimeSpan.FromSeconds(35)) {
        var randomColor = randomNumber.Next(0, 16);

        var xPos = randomNumber.Next(0, 197);
        var yPos = randomNumber.Next(0, 51);

        SetCursorPosition(xPos, yPos);

        foreach (var item in myColorList) {
          if (item.Key.Equals(randomColor)) {
            var currentForeGroundColor = item.Value;
            ForegroundColor = (ConsoleColor) currentForeGroundColor;
            break;
          }
        }

        currentAlphaNumbericSymbol = alphaNumeric[randomNumber.Next(0, 36)];

        if (symbolPositionMap[xPos, yPos] == null) {
          symbolPositionMap[xPos, yPos] = currentAlphaNumbericSymbol;
          Write(currentAlphaNumbericSymbol);
        }
      }

      myStopWatch.Reset();
      myStopWatch.Start();

      while (myStopWatch.Elapsed <= TimeSpan.FromSeconds(35)) {
        var xPos = randomNumber.Next(0, 197);
        var yPos = randomNumber.Next(0, 51);

        SetCursorPosition(xPos, yPos);

        Write(" ");
      }

      myStopWatch.Reset();
      myStopWatch.Start();

      Clear();

      var xPosi = 0;
      var yPosi = 0;
      
      for (int i = 0; i < 10000; i++) {
        SetCursorPosition(xPosi, yPosi);
        Thread.Sleep(1);

        if (xPosi % 3 == 0) {
          BackgroundColor = ConsoleColor.Blue;
          Write(" ");
        }

        if (xPosi % 2 != 0 && xPosi % 3 != 0) {
          BackgroundColor = ConsoleColor.Green;
          Write(" ");
        }

        if (xPosi % 2 == 0 && xPosi % 3 != 0) {
          BackgroundColor = ConsoleColor.Red;
          Write(" ");
        }

        if (xPosi >= 196) {
          xPosi = 0;
        }

        if (yPosi >= 51) {
          yPosi = 0;
        }
        //if (yPosi == 51) yPosi = 0;
        xPosi += 1;
        yPosi += 1;
      }

      Clear();
      SetCursorPosition(92, 27);
      ForegroundColor = ConsoleColor.White;
      WriteLine("All Done");
      ReadKey(true);

    }
  }
}


