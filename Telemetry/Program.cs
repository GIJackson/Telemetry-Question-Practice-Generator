using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;

namespace Waves
{
    public class Program
    {
        public static void Main()
        {
            Dictionary<string, string> waves = new()
            {
                { "Sinus Bradycardia", @"              x                                          x                                          x                                          x                                          x
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
                { "Normal Sinus Rhythm", @"              x                                x                               x                               x                                x                                x                               x
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
                { "Sinus Tachycardia", @"               x                     x                     x                     x                     x                     x                     x                     x                     x                     x
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
                    x                     x                     x                     x                     x                     x                     x                     x                     x" },
                { "Atrial Fibrillation", @"              x                                            x                                       x                  x                 x                                        x                                     x
             x x                                          x x                                     x x                x x               x x                                      x x                                   x x
             x x                                          x x                                     x x                x x               x x                                      x x                                   x x
             x x                                          x x                                     x x                x x               x x                                      x x                                   x x
             x x                                          x x                                     x x                x x               x x                                      x x                                   x x
            x   x                                        x   x                                   x   x              x   x             x   x                                    x   x                                 x   x
    x       x   x                                        x   x                     x             x   x              x   x             x   x                                    x   x                  x              x   x
   xxx      x   x                  x          x          x   x          x         xxx            x   x              x   x             x   x           x          x             x   x               xxx x    xx       x   x
  xx xx     x   x                 xxx        x x         x   x         x x       xx  x      x    x   x              x   x             x   x         xx xx    x x  x   x xx     x   x          x   x    x   x  x      x   x
xxx   xxx   x   x        x   x   xx xx    xxx  x     x   x   x        x   x   xxxx     xx  xx  x x   x       x  x   x   x         x   x   x        x     xx x x     xx    x    x   x         x x x      xxx    xx    x   x
─────────x x─────x──────x x x──x──────xxxx──────x───x x x─────x──────x──────x────────────xx x x┼x─────x─────x──x──xx─────x──────xx xxx─────x──────x────────x───────────────xxxx─────x───────x───x────────────────xxxx─────x──
          x      x     x   x                     x x   x      x     x                        x        x    x             x     x           x     x                                  x     xx                              x
                 x    x                           x           x    x                                  x    x             x    x            x    x                                   x    x                                x
                  x   x                                        x   x                                   x   x              x   x             x   x                                    x   x                                 x
                  x   x                                        x   x                                   x   x              x   x             x   x                                    x   x                                 x
                  x   x                                        x   x                                   x   x              x   x             x   x                                    x   x                                 x
                   x x                                          x x                                     x x                x x               x x                                      x x                                   x
                    x                                            x                                       x                  x                 x                                        x
                    x                                            x                                       x                  x                 x                                        x"},
                { "1st degree block", "1st degree block"},
                { "2nd degree block", "2nd degree block"},
                { "vtach", "vtach"},
                { "Asystole", @"

xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
"},
                { "Ventricular Fibrillation", @"                                                                            xxxxx
                                                                           xx   xx                                                     xx         x xx
                   xxxxx                                           xxx     x     x                        xxx             xxxx       x  xx       x   x                                             xxxxx
                   x   xx                                         x  xx    x     x                       xx xxx          xx  x      x     x    xx    xx                            xx             xx   x
         xxxxxx    x    x                   xxxxxxxx    xxxx xx  xx   xx   x     xxxxxxxxxx       xxxx   x    xxxxxxx  xx    x    x       x   xx      x    xxxxxxxx    xxxxx  xxx xx x       xxxxxx    xxxxxx         xxxxxxx
       xxx    xx   x     xx xxxxxxxxxxx   xxx      xx  xx  xx xxx      x  xx              xxxxxxxxx   xx x           xxx     xxxxxx       xxxxx       xxxxxx      xxxxxx   xxxx  xx   xxxxxxxx              xxxxxxxxxxx     x
 xxxxxx        xxxxx      xxx         xxxx          xxxx               xxxx                            xx
─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
"}
            };

            Console.WriteLine("Welcome to Telemetry Question Generator! \n\nPress any key to continue.");
            Console.ReadKey();
            Console.WriteLine();
            bool continueQuiz = true;
            while (continueQuiz == true)
            {
                Console.WriteLine("Would you like to begin your test? \n\nEnter 1 to begin, enter 2 to quit.");
                string? begin = Console.ReadLine();

                if (begin == "1")
                {
                    try
                    {
                        Console.SetWindowSize(220, 40);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        //create error log here-ish
                        Console.WriteLine("It appears your monitor is not ultrawide.");
                        throw;
                    }
                    

                    List<string> randomKey = new(waves.Keys);

                    var _random = new Random();
                    var randomKeyList = randomKey.OrderBy(item => _random.Next());
                    var questionKey = randomKeyList.ToArray();
                    for (int i = 0; i < questionKey.Length; i++)
                    {
                        //var questionKeyLength = questionKey.Length - 1;
                        Console.WriteLine(waves[questionKey[i]]);
                        Console.WriteLine("What rhythm does this represent?");
                        var answer = questionKey[i];
                        var userAnswer = Console.ReadLine();
                        if (String.IsNullOrEmpty(userAnswer))
                        {
                            Console.WriteLine("How did you do that?");
                            break;
                        }
                        else if (i == (questionKey.Length - 1) && userAnswer.ToUpper() == answer.ToUpper())
                        {
                            Console.WriteLine("Correct!\n\nYou have completed the last question, congratulations!\n\n");
                            Console.WriteLine("Enter any key to return to the main menu.");
                            break;
                        }
                        
                        else if (i == (questionKey.Length - 1) && userAnswer.ToUpper() != answer.ToUpper())
                        {
                            Console.WriteLine("That was not correct.\n\nYou have completed the last question, congratulations!\n\n");
                            Console.WriteLine("Enter any key to return to the main menu.");
                            break;
                        }
                        else if (userAnswer.ToUpper() == answer.ToUpper())
                        {
                            Console.WriteLine("Correct!");
                            Console.WriteLine("Enter any key to continue or press 2 to quit.");
                            if (Console.ReadLine() == "2")
                            {
                                break;
                            }
                        }
                        else if (userAnswer.ToUpper() != answer.ToUpper())
                        {
                            Console.WriteLine("That was not correct. Enter any key to continue or press 2 to to return to the main menu.");
                            if (Console.ReadLine() == "2")
                            {
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("That was not correct. Enter any key to continue or press 2 to to return to the main menu.");
                            if (Console.ReadLine() == "2")
                            {
                                break;
                            }
                        }
                    }
                }
                else if (begin == "2")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("That is not an option, please press enter to continue");
                    Console.ReadKey();
                }
            }
        }
    }
}