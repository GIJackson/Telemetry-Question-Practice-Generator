using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Text;
using System.IO;

namespace Waves
{

    public class Waves
    {
        public static void Main()
        {
            Console.WriteLine("Would you like to begin? Answer: Yes, No, Help, Quit");
            var begin = Console.ReadLine();
            Console.WriteLine(begin);
            if (begin == "yes")
                
                {
                    int origWidth, width;
                    int origHeight, height;

                    origWidth = Console.WindowWidth;
                    origHeight = Console.WindowHeight;

                    width = origWidth + 100;
                    height = origHeight;
                    Console.SetWindowSize(width, height);

                }


            {
                string NSRWave = @"                x                                 x                                 x                                 x                                 x                                 x
               xx                                xx                                xx                                xx                                xx                                xx
               xxx                               xxx                               xxx                               xxx                               xxx                               xxx
               x x                               x x                               x x                               x x                               x x                               x x
               x x                               x x                               x x                               x x                               x x                               x x
              xx x         xx                   xx x         xx                   xx x         xx                   xx x         xx                   xx x         xx                   xx x         xx
   xx         x  x        xxxx       xx         x  x        xxxx       xx         x  x        xxxx       xx         x  x        xxxx       xx         x  x        xxxx       xx         x  x        xxxx
  xxxx        x  xx      xx  xx     xxxx        x  xx      xx  xx     xxxx        x  xx      xx  xx     xxxx        x  xx      xx  xx     xxxx        x  xx      xx  xx     xxxx        x  xx      xx  xx
xxx  xxxxxx   x   x    xxxx   xxxxxxx  xxxxxx   x   x    xxxx   xxxxxxx  xxxxxx   x   x    xxxx   xxxxxxx  xxxxxx   x   x    xxxx   xxxxxxx  xxxxxx   x   x    xxxx   xxxxxxx  xxxxxx   x   x    xxxx   xxxx
──────────xx──x───x────x────────────────────xx──x───x────x────────────────────xx──x───x────x────────────────────xx──x───x────x────────────────────xx──x───x────x────────────────────xx──x───x────x──────────
           x  x   xx  x                      x  x   xx  x                      x  x   xx  x                      x  x   xx  x                      x  x   xx  x                      x  x   xx  x
           xxx     x xx                      xxx     x xx                      xxx     x xx                      xxx     x xx                      xxx     x xx                      xxx     x xx
            xx     x x                        xx     x x                        xx     x x                        xx     x x                        xx     x x                        xx     x x
            x      xxx                        x      xxx                        x      xxx                        x      xxx                        x      xxx                        x      xxx
                   xx                                xx                                xx                                xx                                xx                                xx
                   xx                                xx                                xx                                xx                                xx                                xx
                   x                                 x                                 x                                 x                                 x                                 x";
                Console.WriteLine(NSRWave);
                Console.ReadLine();
            }

        }
    }
}