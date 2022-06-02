using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;

namespace Waves
{
    public class Waves
    {
        public string WaveName { get; set; }
        public string Ascii { get; set; }

    }
        public class Program
    {
        public static void Main()
        {
            var wave = new List<Waves>()
            {


                        new Waves(){ WaveName = "Sinus Bradycardia", Ascii = @"              x                                          x                                          x                                          x                                          x
              xx                                         xx                                         xx                                         xx                                         xx
              x x                                        x x                                        x x                                        x x                                        x x
              x  x                                       x  x                                       x  x                                       x  x                                       x  x
              x  x                                       x  x                                       x  x                                       x  x                                       x  x
              x  x                                       x  x                                       x  x                                       x  x                                       x  x
              x  x         xx                            x  x         xx                            x  x         xx                            x  x         xx                            x  x         xx
   xx         x  x        x  x                xx         x  x        x  x                xx         x  x        x  x                xx         x  x        x  x                xx         x  x        x  x
  x  x        x   x      x    x              x  x        x   x      x    x              x  x        x   x      x    x              x  x        x   x      x    x              x  x        x   x      x    x
xx    xxxxx   x   x     x      xxxxxxxxxxxxxx    xxxxx   x   x     x      xxxxxxxxxxxxxx    xxxxx   x   x     x      xxxxxxxxxxxxxx    xxxxx   x   x     x      xxxxxxxxxxxxxx    xxxxx   x   x     x      xxxxxxxxxxxxx
──────────xx──x───x────x─────────────────────────────xx──x───x────x─────────────────────────────xx──x───x────x─────────────────────────────xx──x───x────x─────────────────────────────xx──x───x────x────────────────────
           x  x   xx  x                               x  x   xx  x                               x  x   xx  x                               x  x   xx  x                               x  x   xx  x
           x x     x xx                               x x     x xx                               x x     x xx                               x x     x xx                               x x     x xx
            xx     x x                                 xx     x x                                 xx     x x                                 xx     x x                                 xx     x x
            x      x x                                 x      x x                                 x      x x                                 x      x x                                 x      x x
                   xx                                         xx                                         xx                                         xx                                         xx
                   xx                                         xx                                         xx                                         xx                                         xx
                   x                                          x                                          x                                          x                                          x" },
                        new Waves(){ WaveName = "Normal Sinus Rhythm", Ascii = @"              x                                x                               x                               x                                x                                x                               x
              xx                               xx                              xx                              xx                               xx                               xx                              xx
              x x                              x x                             x x                             x x                              x x                              x x                             x x
              x  x                             x  x                            x  x                            x  x                             x  x                             x  x                            x  x
              x  x                             x  x                            x  x                            x  x                             x  x                             x  x                            x  x
              x  x                             x  x                            x  x                            x  x                             x  x                             x  x                            x  x
              x  x         xx                  x  x         xx                 x  x         xx                 x  x         xx                  x  x         xx                  x  x         xx                 x  x
   xx         x  x        x  x      xx         x  x        x  x     xx         x  x        x  x     xx         x  x        x  x      xx         x  x        x  x      xx         x  x        x  x     xx         x  x
  x  x        x   x      x    x    x  x        x   x      x    x   x  x        x   x      x    x   x  x        x   x      x    x    x  x        x   x      x    x    x  x        x   x      x    x   x  x        x   x
xx    xxxxx   x   x     x      xxxx    xxxxx   x   x     x      xxx    xxxxx   x   x     x      xxx    xxxxx   x   x     x      xxxx    xxxxx   x   x     x      xxxx    xxxxx   x   x     x      xxx    xxxxx   x   x
──────────xx──x───x────x───────────────────xx──x───x────x──────────────────xx──x───x────x──────────────────xx──x───x────x───────────────────xx──x───x────x───────────────────xx──x───x────x──────────────────xx──x───x──
           x  x   xx  x                     x  x   xx  x                    x  x   xx  x                    x  x   xx  x                     x  x   xx  x                     x  x   xx  x                    x  x   xx
           x x     x xx                     x x     x xx                    x x     x xx                    x x     x xx                     x x     x xx                     x x     x xx                    x x     x
            xx     x x                       xx     x x                      xx     x x                      xx     x x                       xx     x x                       xx     x x                      xx     x
            x      x x                       x      x x                      x      x x                      x      x x                       x      x x                       x      x x                      x      x
                   xx                               xx                              xx                              xx                               xx                               xx                              xx
                   xx                               xx                              xx                              xx                               xx                               xx                              xx
                   x                                x                               x                               x                                x                                x                               x" },
                        new Waves(){ WaveName = "Sinus Tachycardia", Ascii = @"               x                     x                     x                     x                     x                     x                     x                     x                     x                     x
               x                     x                     x                     x                     x                     x                     x                     x                     x                     x
               x                     x                     x                     x                     x                     x                     x                     x                     x                     x
              x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x
              x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x
              x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x
              x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x
  x     x     x  x      x     x     x  x      x     x     x  x      x     x     x  x      x     x     x  x      x     x     x  x      x     x     x  x      x     x     x  x      x     x     x  x      x     x     x  x
 xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x
 x x   x x    x  x     x x   x x    x  x     x x   x x    x  x     x x   x x    x  x     x x   x x    x  x     x x   x x    x  x     x x   x x    x  x     x x   x x    x  x     x x   x x    x  x     x x   x x    x  x
x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───
     x    x   x   x   x    x    x   x   x   x    x    x   x   x   x    x    x   x   x   x    x    x   x   x   x    x    x   x   x   x    x    x   x   x   x    x    x   x   x   x    x    x   x   x   x    x    x   x
          x   x   x   x         x   x   x   x         x   x   x   x         x   x   x   x         x   x   x   x         x   x   x   x         x   x   x   x         x   x   x   x         x   x   x   x         x   x
           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x
           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x
           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x
            x       x             x       x             x       x             x       x             x       x             x       x             x       x             x       x             x       x             x
                    x             x       x                     x                     x                     x                     x                     x                     x                     x
                    x                     x                     x                     x                     x                     x                     x                     x                     x" }
            };


            Console.WriteLine("Welcome to Telemetry Question Generator! Press enter to continue.");
            Console.ReadLine();
            Console.WriteLine("Would you like to begin your test? Enter 1 to begin, enter 2 for more options (it just makes you quit right now).");

            int origWidth, width;
            int origHeight, height;

            origWidth = Console.WindowWidth;
            origHeight = Console.WindowHeight;

            width = origWidth + 100;
            height = origHeight;

            string begin;
            while ((begin = Console.ReadLine()) != "2")
            {
                Console.WriteLine(begin);
                if (begin == "1")
                {
                    Console.SetWindowSize(width, height);

                    var result = from w in wave where w.WaveName == "Sinus Bradycardia" select w;
                    foreach (var w in result) Console.WriteLine(w.Ascii);

                    Console.WriteLine("What rhythm does this represent?");

                    var answer = Console.ReadLine();
                    if (answer == "Sinus Bradycardia")
                    {
                        Console.WriteLine("That is correct! Please 2 to exit");
                    }
                    else
                    {
                        Console.WriteLine("That was incorrect. Please 1 to try again.");
                    }

                }
                else if (begin == "2")
                {
                    Environment.Exit(0);
                }

                else
                {
                    Console.WriteLine("That is not an option, please press enter to continue");
                    Console.ReadLine();
                    Console.WriteLine("Would you like to begin your test? Enter 1 to begin, enter 2 for more options (it just makes you quit right now).");
                }
            }
        }
    }
}