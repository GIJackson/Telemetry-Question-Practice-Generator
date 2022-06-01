using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                string NSRWave = @"


                                               x
                                              xxx
                                              x x
                                              x x
                                             xx xx
                                             x   x
                                             x   x
                             x              xx   xx            x
                           xxxxx            x     x          xxxxx
                          xx   xx           x     x         xx   xx
                         xx     xx          x     x        xx     xx
         xxxxxxxxxxxxxxxxx       xxxxx     x       x xxxxxxx       xxxxxxxx
                                     x     x       x x
                                     x     x       x x
                                     xx   xx       xxx
                                      x   x         x
                                      x   x
                                      xx xx
                                       x x
                                       xxx
                                        x";
                Console.WriteLine(NSRWave);
                Console.ReadLine();
            }

        }
    }
}