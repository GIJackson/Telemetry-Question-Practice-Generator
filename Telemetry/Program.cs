using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Windows;
using System.Runtime.InteropServices;

namespace Telemetry
{
    public class Program
    {
        public static void Main()
        {
            Console.Title = "Telemetry Testing";
            User user = new User();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "UserSaveData.csv");
            if (user.CSVSaveReader(filePath) == false)
            {
                user.CSVWriterNew(filePath);
            }
            else
            {
                IEnumerable<string> names = user.firstNames.Zip(user.lastNames, (first, second) => first + " " + second);
                Dictionary<int, string> users = user.IDs.Zip(names, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
                for (int i = 1; i < users.Count; i++)
                {
                    Console.WriteLine("[" + i + "]" + users[i]);
                }
                Console.WriteLine("Please choose a user, or enter 0 to create a new user.");
                string? input = Console.ReadLine();
                while (int.TryParse(input, out int n) == false)
                {
                    Console.WriteLine("Please choose a user, or enter 0 to create a new user.");
                    input = Console.ReadLine();
                }
                if (Convert.ToInt32(input) == 0)
                {
                    user.CSVWriterNew(filePath);

                }
                else if (int.TryParse(input, out int intput) == true && Convert.ToInt32(input) != 0)
                {
                    user.UserProperties(intput);
                }
            }
            //These are just to test user properties are remembered.//
            Console.WriteLine(user._firstName);
            Console.WriteLine(user._lastName);
            Console.WriteLine(user._name);
            
//            Dictionary<string, string> waves = new Dictionary<string, string>()
//            {
//                {@"              x                                          x                                          x                                          x                                          x
//              xx                                         xx                                         xx                                         xx                                         xx
//              x x                                        x x                                        x x                                        x x                                        x x
//              x  x                                       x  x                                       x  x                                       x  x                                       x  x
//              x  x                                       x  x                                       x  x                                       x  x                                       x  x
//              x  x                                       x  x                                       x  x                                       x  x                                       x  x
//              x  x         xx                            x  x         xx                            x  x         xx                            x  x         xx                            x  x         xx
//   xx         x  x        x  x                xx         x  x        x  x                xx         x  x        x  x                xx         x  x        x  x                xx         x  x        x  x
//  x  x        x   x      x    x              x  x        x   x      x    x              x  x        x   x      x    x              x  x        x   x      x    x              x  x        x   x      x    x
//xx    xxxxx   x   x     x      xxxxxxxxxxxxxx    xxxxx   x   x     x      xxxxxxxxxxxxxx    xxxxx   x   x     x      xxxxxxxxxxxxxx    xxxxx   x   x     x      xxxxxxxxxxxxxx    xxxxx   x   x     x      xxxxxxxxxxxxx
//──────────xx──x───x────x─────────────────────────────xx──x───x────x─────────────────────────────xx──x───x────x─────────────────────────────xx──x───x────x─────────────────────────────xx──x───x────x────────────────────
//           x  x   xx  x                               x  x   xx  x                               x  x   xx  x                               x  x   xx  x                               x  x   xx  x
//           x x     x xx                               x x     x xx                               x x     x xx                               x x     x xx                               x x     x xx
//            xx     x x                                 xx     x x                                 xx     x x                                 xx     x x                                 xx     x x
//            x      x x                                 x      x x                                 x      x x                                 x      x x                                 x      x x
//                   xx                                         xx                                         xx                                         xx                                         xx
//                   xx                                         xx                                         xx                                         xx                                         xx
//                   x                                          x                                          x                                          x                                          x", "Sinus Bradycardia" },
//                {@"              x                                x                               x                               x                                x                                x                               x
//              xx                               xx                              xx                              xx                               xx                               xx                              xx
//              x x                              x x                             x x                             x x                              x x                              x x                             x x
//              x  x                             x  x                            x  x                            x  x                             x  x                             x  x                            x  x
//              x  x                             x  x                            x  x                            x  x                             x  x                             x  x                            x  x
//              x  x                             x  x                            x  x                            x  x                             x  x                             x  x                            x  x
//              x  x         xx                  x  x         xx                 x  x         xx                 x  x         xx                  x  x         xx                  x  x         xx                 x  x
//   xx         x  x        x  x      xx         x  x        x  x     xx         x  x        x  x     xx         x  x        x  x      xx         x  x        x  x      xx         x  x        x  x     xx         x  x
//  x  x        x   x      x    x    x  x        x   x      x    x   x  x        x   x      x    x   x  x        x   x      x    x    x  x        x   x      x    x    x  x        x   x      x    x   x  x        x   x
//xx    xxxxx   x   x     x      xxxx    xxxxx   x   x     x      xxx    xxxxx   x   x     x      xxx    xxxxx   x   x     x      xxxx    xxxxx   x   x     x      xxxx    xxxxx   x   x     x      xxx    xxxxx   x   x
//──────────xx──x───x────x───────────────────xx──x───x────x──────────────────xx──x───x────x──────────────────xx──x───x────x───────────────────xx──x───x────x───────────────────xx──x───x────x──────────────────xx──x───x──
//           x  x   xx  x                     x  x   xx  x                    x  x   xx  x                    x  x   xx  x                     x  x   xx  x                     x  x   xx  x                    x  x   xx
//           x x     x xx                     x x     x xx                    x x     x xx                    x x     x xx                     x x     x xx                     x x     x xx                    x x     x
//            xx     x x                       xx     x x                      xx     x x                      xx     x x                       xx     x x                       xx     x x                      xx     x
//            x      x x                       x      x x                      x      x x                      x      x x                       x      x x                       x      x x                      x      x
//                   xx                               xx                              xx                              xx                               xx                               xx                              xx
//                   xx                               xx                              xx                              xx                               xx                               xx                              xx
//                   x                                x                               x                               x                                x                                x                               x", "Normal Sinus Rhythm" },
//                {@"               x                     x                     x                     x                     x                     x                     x                     x                     x                     x
//               x                     x                     x                     x                     x                     x                     x                     x                     x                     x
//               x                     x                     x                     x                     x                     x                     x                     x                     x                     x
//              x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x
//              x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x
//              x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x
//              x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x                   x x
//  x     x     x  x      x     x     x  x      x     x     x  x      x     x     x  x      x     x     x  x      x     x     x  x      x     x     x  x      x     x     x  x      x     x     x  x      x     x     x  x
// xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x     xxx   xxx    x  x
// x x   x x    x  x     x x   x x    x  x     x x   x x    x  x     x x   x x    x  x     x x   x x    x  x     x x   x x    x  x     x x   x x    x  x     x x   x x    x  x     x x   x x    x  x     x x   x x    x  x
//x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───x───x───xxx───x───x───
//     x    x   x   x   x    x    x   x   x   x    x    x   x   x   x    x    x   x   x   x    x    x   x   x   x    x    x   x   x   x    x    x   x   x   x    x    x   x   x   x    x    x   x   x   x    x    x   x
//          x   x   x   x         x   x   x   x         x   x   x   x         x   x   x   x         x   x   x   x         x   x   x   x         x   x   x   x         x   x   x   x         x   x   x   x         x   x
//           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x
//           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x
//           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x     x x           x x
//            x       x             x       x             x       x             x       x             x       x             x       x             x       x             x       x             x       x             x
//                    x             x       x                     x                     x                     x                     x                     x                     x                     x
//                    x                     x                     x                     x                     x                     x                     x                     x                     x", "Sinus Tachycardia" },
//                {@"              x                                            x                                       x                  x                 x                                        x                                     x
//             x x                                          x x                                     x x                x x               x x                                      x x                                   x x
//             x x                                          x x                                     x x                x x               x x                                      x x                                   x x
//             x x                                          x x                                     x x                x x               x x                                      x x                                   x x
//             x x                                          x x                                     x x                x x               x x                                      x x                                   x x
//            x   x                                        x   x                                   x   x              x   x             x   x                                    x   x                                 x   x
//    x       x   x                                        x   x                     x             x   x              x   x             x   x                                    x   x                  x              x   x
//   xxx      x   x                  x          x          x   x          x         xxx            x   x              x   x             x   x           x          x             x   x               xxx x    xx       x   x
//  xx xx     x   x                 xxx        x x         x   x         x x       xx  x      x    x   x              x   x             x   x         xx xx    x x  x   x xx     x   x          x   x    x   x  x      x   x
//xxx   xxx   x   x        x   x   xx xx    xxx  x     x   x   x        x   x   xxxx     xx  xx  x x   x       x  x   x   x         x   x   x        x     xx x x     xx    x    x   x         x x x      xxx    xx    x   x
//─────────x x─────x──────x x x──x──────xxxx──────x───x x x─────x──────x──────x────────────xx x x┼x─────x─────x──x──xx─────x──────xx xxx─────x──────x────────x───────────────xxxx─────x───────x───x────────────────xxxx─────x──
//          x      x     x   x                     x x   x      x     x                        x        x    x             x     x           x     x                                  x     xx                              x
//                 x    x                           x           x    x                                  x    x             x    x            x    x                                   x    x                                x
//                  x   x                                        x   x                                   x   x              x   x             x   x                                    x   x                                 x
//                  x   x                                        x   x                                   x   x              x   x             x   x                                    x   x                                 x
//                  x   x                                        x   x                                   x   x              x   x             x   x                                    x   x                                 x
//                   x x                                          x x                                     x x                x x               x x                                      x x                                   x
//                    x                                            x                                       x                  x                 x                                        x
//                    x                                            x                                       x                  x                 x                                        x", "Atrial Fibrillation"},
//                {@"     x                           x                           x                           x                           x                           x                           x                           x
//     x                           x                           x                           x                           x                           x                           x                           x
//     x                           x                           x                           x                           x                           x                           x                           x
//    x x                         x x                         x x                         x x                         x x                         x x                         x x                         x x
//    x x                         x x                         x x                         x x                         x x                         x x                         x x                         x x
//    x x                         x x                         x x                         x x                         x x                         x x                         x x                         x x
//    x x                         x x                         x x                         x x                         x x                         x x                         x x                         x x
//    x  x      x     x     x     x  x      x     x     x     x  x      x     x     x     x  x      x     x     x     x  x      x     x     x     x  x      x     x     x     x  x      x     x     x     x  x      x     x     x
//    x  x     xxx   xxx   xxx    x  x     xxx   xxx   xxx    x  x     xxx   xxx   xxx    x  x     xxx   xxx   xxx    x  x     xxx   xxx   xxx    x  x     xxx   xxx   xxx    x  x     xxx   xxx   xxx    x  x     xxx   xxx   xxx
//    x  x     x x   x x   x x    x  x     x x   x x   x x    x  x     x x   x x   x x    x  x     x x   x x   x x    x  x     x x   x x   x x    x  x     x x   x x   x x    x  x     x x   x x   x x    x  x     x x   x x   x x
//x───x───x───x───x-x───x-x───x───x───x───x───x-x───x-x───x───x───x───x───x-x───x-x───x───x───x───x───x-x───x-x───x───x───x───x───x-x───x-x───x───x───x───x───x-x───x-x───x───x───x───x───x-x───x-x───x───x───x───x───x-x───x-x───
//x   x   x   x    x     x    x   x   x   x    x     x    x   x   x   x    x     x    x   x   x   x    x     x    x   x   x   x    x     x    x   x   x   x    x     x    x   x   x   x    x     x    x   x   x   x    x     x
//x   x   x   x               x   x   x   x               x   x   x   x               x   x   x   x               x   x   x   x               x   x   x   x               x   x   x   x               x   x   x   x
// x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x
// x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x
// x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x
//  x       x                   x       x                   x       x                   x       x                   x       x                   x       x                   x       x                   x       x
//          x                           x                           x                           x                           x                           x                           x                           x
//          x                           x                           x                           x                           x                           x                           x                           x", "Atrial Flutter" },
//                {@"     xxxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx
//    x    x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x
//   x      x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x
//   x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x
//   x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x
//   x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x
//   x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x
//  x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x
// x           x x           x x           x x           x x           x x           x x           x x           x x           x x           x x           x x           x x           x x           x x           x x
//x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x──────────", "Ventricular Tachycardia"},
//                {@"             xxx                                             x xx         x x          xxx                                     x                                     xxx            xxx                 xxx
//    x       x    x                  xx                      x    x       x   x        x   x                                   x x                      x           xx   xx         x   x      xx        x xx       xxx
//   x x     x      x                x   x                     x   x      x    x       x     x    xxx                          x   x          xx        x x         x       x       x     x     x xx      x  x      xx x
//  x  x    x         x              x    x        xxx         x   x      x     x      x     x   x   x                         x   x         x  x      x   x       x         x      x     x     x   x     x  x      x  x
//  x  x    x          x      x      x    x      xx   x        x   x      x     x     x      x   x   xx    xxx        xx       x    x       x    x     x   x      x           x     x     x     x   x     x   x     x  x
//  x  x    x          x     x xx    x    x     x      x       x   x      x      x   x       x   x    x   x   xx    xx  x      x     x      x    x     x    x     x           x     x     x     x   x     x   x     x  x     xx
//  x  x   x            x    x   x  x     x    x        x     x     x    x       x   x       x   x    x  x      xxxx     x    x      x      x    x     x    x    x            x     x     x     x    x    x    x    x  x     x
// x   x   x             x  x    x x      x   x          x   x       x   x       x  x        x  x      xx                 xxxx       x     x     x     x    x    x            x    x      xx   x     x    x    x   x   x     x
// x   x   x              xx      x        xxx            xxx         xx          xx          xx                                      x   x      x    x      x   x            x    x       x  x      xxxxx     x   x   x    x
// x    xxxx                                                                                                                           xxx        xxxx        xxx             xx xx         xx                 xxxx     x  xx
//x─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────x───────────────────────────────────────xxx────", "Ventricular Tachycardia"},
//                {@"                                                                                                                                                                                                               x           x
//                                                                         xx         xx       xx         xxx                                                                                         xx         xx        xx x
//                                                                xx     xx  x        x x     x  xx       x  x         xx                                                                            x  x        x x      x
//                                                           x   x  xx  x     x      x  x    x    x       x  x        x  x                                                                     xx    x   x      x   x     x
//                                                          x x  x   x  x     x      x  x    x    x      x    x      x    x                                                                   x x    x   x      x   x    x
//                                      x     x     x       x x  x   x  x     x     x    x   x    x     x     x      x    x        xx         x                                               x x    x   x      x   x    x
//                              xx     x x   x x   x  x    x  x  x   x  x     x     x    x   x    x     x     x      x    x        x x       x x            xx                               x  x     x  x      x    x   x
//                             x   x  x   x x   x  x  x    x  x  x   x  x     x    x    x    x    x     x     x     x     x        x  x      x  x          x  x     xx     x        xx       x   x    x   x    x     x   x
//              x      xxx     x   x  x   x x   x  x  x   x   x  x   x  x     x    x    x    x    x     x     x     x     x        x   x    x     x      x     x   x  x   x x      x  x      x   x    x    x   x     x   x
//      x      x x    x   x   x    x  x   x x   x  x  x   x   x  x   x  x     x   x     x    x    x     x     x     x     x       x    x   x       x    x       x  x  x  x  x     x    x    x    x    x    x   x     x   x
//     x x    x   x  x    x   x    x x   x x    x x    x x    x x    x  x     x   x     x   x     x    x       x    x      x    xx     x  x       x    x        x  x  x  x   x    x    x    x     x   x    x   x     x   x
//    x   x  x    x x      xxx      x     x      x      x      x      xx       x x       x  x     x    x       x   x       x   x      x   x       x    x        x  x  x  x    x   x     x  x      x   x    x  x      xx x
//xxxx     xx      x                                                            x         xx       xxxx         xxx         xxx        xxx         xxxx          xx    xx      xxx       xxx       xx       xx         x
//─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────", "Ventricular Tachycardia"},
//                {@"          x                            x                            x                            x                            x                            x                            x                            x
//         x x                          x x                          x x                          x x                          x x                          x x                          x x                          x x
//         x x                          x x                          x x                          x x                          x x                          x x                          x x                          x x
//         x  x                         x  x                         x  x                         x  x                         x  x                         x  x                         x  x                         x  x
//         x   x                        x   x                        x   x                        x   x                        x   x                        x   x                        x   x                        x   x
//         x   x                        x   x                        x   x                        x   x                        x   x                        x   x                        x   x                        x   x
//         x   x                        x   x                        x   x                        x   x                        x   x                        x   x                        x   x                        x   x
//        x    x          xxx          x    x          xxx          x    x          xxx          x    x          xxx          x    x          xxx          x    x          xxx          x    x          xxx          x    x
//        x     x        x   x         x     x        x   x         x     x        x   x         x     x        x   x         x     x        x   x         x     x        x   x         x     x        x   x         x     x
//xxxxx   x     x       x     xxxxxx   x     x       x     xxxxxx   x     x       x     xxxxxx   x     x       x     xxxxxx   x     x       x     xxxxxx   x     x       x     xxxxxx   x     x       x     xxxxxx   x     x
//─────x──x─────x───────x───────────x──x─────x───────x───────────x──x─────x───────x───────────x──x─────x───────x───────────x──x─────x───────x───────────x──x─────x───────x───────────x──x─────x───────x───────────x──x─────x───
//      x x     x   xx  x            x x     x   xx  x            x x     x   xx  x            x x     x   xx  x            x x     x   xx  x            x x     x   xx  x            x x     x   xx  x            x x     x
//       x       x x x  x             x       x x x  x             x       x x x  x             x       x x x  x             x       x x x  x             x       x x x  x             x       x x x  x             x       x x
//                x   xx                       x   xx                       x   xx                       x   xx                       x   xx                       x   xx                       x   xx                       x", "Junctional Rhythm" },
//                {@"               x                                x                                x               x               x               x               x               x               x               x               x
//               xx                               xx                               x               x               x               x               x               x               x               x               x
//               x x                              x x                              x               x               x               x               x               x               x               x               x
//               x  x                             x  x                            x x             x x             x x             x x             x x             x x             x x             x x             x x
//               x  x                             x  x                            x x             x x             x x             x x             x x             x x             x x             x x             x x
//               x  x                             x  x                            x x             x x             x x             x x             x x             x x             x x             x x             x x
//               x  x         xx                  x  x         xx                 x x             x x             x x             x x             x x             x x             x x             x x             x x
//    xx         x  x        x  x      xx         x  x        x  x     xx         x  x      x     x  x      x     x  x      x     x  x      x     x  x      x     x  x      x     x  x      x     x  x      x     x  x      x
//   x  x        x   x      x    x    x  x        x   x      x    x   x  x        x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x
// xx    xxxxx   x   x     x      xxxx    xxxxx   x   x     x      xxx    xxxx    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x
//───────────xx──x───x────x───────────────────xx──x───x────x──────────────────x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x
//            x  x   xx  x                     x  x   xx  x                   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x
//            x xx    x xx                     x x     x xx                   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x
//             xx     x x                       xx     x x                     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x
//             x      x x                       x      x x                     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x
//                    xx                               xx                      x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x
//                    xx                               xx                       x       x       x       x       x       x       x       x       x       x       x       x       x       x       x       x       x       x
//                    x                                x                                x               x               x               x               x               x               x               x               x
//                                                                                      x               x               x               x               x               x               x               x               x", "Paroxysmal Supraventricular Tachycardia" },
//                {@"               x                                                                         x                                x                               x                               x
//               xx                              x                                         xx                               xx                              xx                              xx
//               x x                            x x                                        x x                              x x                             x x                             x x
//               x  x                           x x                                        x  x                             x  x                            x  x                            x  x
//               x  x                          x  x                                        x  x                             x  x                            x  x                            x  x
//               x  x                          x  x                                        x  x                             x  x                            x  x                            x  x
//               x  x         xx               x  x                                        x  x         xx                  x  x         xx                 x  x         xx                 x  x         xx
//    xx         x  x        x  x      xx     x    x        xx                  xx         x  x        x  x      xx         x  x        x  x     xx         x  x        x  x     xx         x  x        x  x      xx
//   x  x        x   x      x    x    x  x   x     x       x  xx               x  x        x   x      x    x    x  x        x   x      x    x   x  x        x   x      x    x   x  x        x   x      x    x    x  x
// xx    xxxxx   x   x     x      xxxx    xxx      x      x     xxxxxxxxxxxxxxx    xxxxx   x   x     x      xxxx    xxxxx   x   x     x      xxx    xxxxx   x   x     x      xxx    xxxxx   x   x     x      xxxx    xxxxx
//───────────xx──x───x────x────────────────────────x──────x────────────────────────────xx──x───x────x───────────────────xx──x───x────x──────────────────xx──x───x────x──────────────────xx──x───x────x────────────────────
//            x  x   xx  x                         x      x                             x  x   xx  x                     x  x   xx  x                    x  x   xx  x                    x  x   xx  x
//            x xx    x xx                          x     x                             x x     x xx                     x x     x xx                    x x     x xx                    x x     x xx
//             xx     x x                           x     x                              xx     x x                       xx     x x                      xx     x x                      xx     x x
//             x      x x                            x   x                               x      x x                       x      x x                      x      x x                      x      x x
//                    xx                             x   x                                      xx                               xx                              xx                              xx
//                    xx                             x   x                                      xx                               xx                              xx                              xx
//                    x                              x   x                                      x                                x                               x                               x
//                                                    x xx
//                                                    x x
//                                                     x", "Premature Ventricular Contraction" },
//                {@"               xxx                                        xxx                                        xxx                                        xxx                                         xxx
//              x   x                                      x   x                                      x   x                                      x   x                                       x   x
//              x   x                                      x   x                                      x   x                                      x   x                                       x   x
//             x     x                                    x     x                                    x     x                                    x     x                                     x     x
//             x     x                                    x     x                                    x     x                                    x     x                                     x     x
//            x       x                                  x       x                                  x       x                                  x       x                                   x       x
//            x       x                                  x       x                                  x       x                                  x       x                                   x       x
//            x        x                                 x        x                                 x        x                                 x        x                                  x        x
//           x         x                                x         x                                x         x                                x         x                                 x         x
//xxxxxxxxxxxx         x                 xxxxxxxxxxxxxxxx         x                 xxxxxxxxxxxxxxxx         x                 xxxxxxxxxxxxxxxx         x                 xxxxxxxxxxxxxxxxx         x                 xxxxxxxxx
//──────────────────────x───────────────x──────────────────────────x───────────────x──────────────────────────x───────────────x──────────────────────────x───────────────x───────────────────────────x───────────────x─────────
//                      x             xx                           x             xx                           x             xx                           x             xx                            x             xx
//                       x    xx     x                              x    xx     x                              x    xx     x                              x    xx     x                               x    xx     x
//                        x  x  x   x                                x  x  x   x                                x  x  x   x                                x  x  x   x                                 x  x  x   x
//                         xx    xxx                                  xx    xxx                                  xx    xxx                                  xx    xxx                                   xx    xxx", "Idioventricular Rhythm" },
//                {@"                                   x                                                            x                                                             x                                                            x
//                                   xx                                                           xx                                                            xx                                                           xx
//                                   x x                                                          x x                                                           x x                                                          x
//                                   x  x                                                         x  x                                                          x  x                                                         x
//                                   x  x                                                         x  x                                                          x  x                                                         x
//                                   x  x                                                         x  x                                                          x  x                                                         x
//                                   x  x                                                         x  x                                                          x  x                                                         x
//                                   x  x                                                         x  x                                                          x  x                                                         x
//                                   x   x                                                        x   x                                                         x   x                                                        x
//        xx                         x   x                             xx                         x   x                              xx                         x   x                             xx                         x
//      xx  xx                       x   x                 xx        xx  xx                       x   x                 xx         xx  xx                       x   x                 xx        xx  xx                       x
//     x      x                      x   xx              xx  xx     x      x                      x   xx              xx  xx      x      x                      x   xx              xx  xx     x      x                      x
//xxxxx        xxxxxxxxxxxxxxxxxxxxxxx    x             x      xxxxx        xxxxxxxxxxxxxxxxxxxxxxx    x             x      xxxxxx        xxxxxxxxxxxxxxxxxxxxxxx    x             x      xxxxx        xxxxxxxxxxxxxxxxxxxxxxx
//─────────────────────────────────────────x───────────x────────────────────────────────────────────────x───────────x─────────────────────────────────────────────────x───────────x────────────────────────────────────────────
//                                          xx        x                                                  xx        x                                                   xx        x
//                                            xx    xx                                                     xx    xx                                                      xx    xx
//                                              xxxx                                                         xxxx                                                          xxxx", "First Degree AV Block" },
//                {@"               x                                                 x                                                 x                                                            x
//               xx                                                xx                                                xx                                                           xx
//               x x                                               x x                                               x x                                                          x x
//               x  x                                              x  x                                              x  x                                                         x  x
//               x  x                                              x  x                                              x  x                                                         x  x
//               x  x                                              x  x                                              x  x                                                         x  x
//               x  x         xx                                   x  x         xx                                   x  x         xx                                              x  x         xx
//    xx         x  x        x  x                   xx             x  x        x  x              xx                  x  x        x  x        xx                        xx         x  x        x  x                       xx
//   x  x        x   x      x    x                 x  x            x   x      x    x            x  x                 x   x      x    x      x  x                      x  x        x   x      x    x                     x  x
// xx    xxxxx   x   x     x      xxxxxxxxxxxxxxxxx    xxxxxxxxx   x   x     x      xxxxxxxxxxxx    xxxxxxxxxxxxxx   x   x     x      xxxxxx    xxxxxxxxxxxxxxxxxxxxxx    xxxxx   x   x     x      xxxxxxxxxxxxxxxxxxxxx    xxx
//───────────xx──x───x────x────────────────────────────────────xx──x───x────x────────────────────────────────────xx──x───x────x───────────────────────────────────────────────xx──x───x────x───────────────────────────────────
//            x  x   xx  x                                      x  x   xx  x                                      x  x   xx  x                                                 x  x   xx  x
//            x xx    x xx                                      x xx    x xx                                      x xx    x xx                                                 x xx    x xx
//             xx     x x                                        xx     x x                                        xx     x x                                                   xx     x x
//             x      x x                                        x      x x                                        x      x x                                                   x      x x
//                    xx                                                xx                                                xx                                                           xx
//                    xx                                                xx                                                xx                                                           xx
//                    x                                                 x                                                 x                                                            x", "Second Degree AV Block"},
//                {@"            x                xx                                  x                xx                                                                                        x                xx                  x
//           x x              x  x                                x x              x  x                                                                                      x x              x  x                x x
//    xx     x x            xx    xx                       xx     x x            xx    xx                       xx                       xx                           xx     x x            xx    xx       xx     x x
//   x  x   x   x          x        x                     x  x   x   x          x        x                     x  x                     x  x                         x  x   x   x          x        x     x  x   x   x
//xxx    xxx     x        x          xxxxxxxxxxxxxxxxxxxxx    xxx     x        x          xxxxxxxxxxxxxxxxxxxxx    xxxxxxxxxxxxxxxxxxxxx    xxxxxxxxxxxxxxxxxxxxxxxxx    xxx     x        x          xxxxx    xxx     x
//───────────────x────────x───────────────────────────────────────────x────────x─────────────────────────────────────────────────────────────────────────────────────────────────x────────x───────────────────────────x────────
//               x       x                                            x       x                                                                                                  x       x                            x       x
//                x      x                                             x      x                                                                                                   x      x                             x      x
//                x      x                                             x      x                                                                                                   x      x                             x      x
//                 x    x                                               x    x                                                                                                     x    x                               x    x
//                  x   x                                                x   x                                                                                                      x   x                                x   x
//                   x  x                                                 x  x                                                                                                       x  x                                 x  x
//                    x x                                                  x x                                                                                                        x x                                  x x
//                     x                                                    x                                                                                                          x                                    x", "Second Degree AV Block"},
//                {@"                                                    xxx
//    xx            xx               xx          x   x   x         xx            xx               xx            xx                 xx            xx               xx            xx                 xx            xx
//   x  x          x  x     xx      x  x        x x x     x       x  x          x  x     xx      x  x          x  x            xx x  x          x  x             x  x          x  x   xx          x  x          x  x
//x  x  x        x     x   x  x     x  x        x  x       x      x  x        x     x   x  x     x  x        x     x          x  xx  x        x     x            x  x        x     x x  x         x  x        x     x
// x x  x        x      xxx    xxxx x  x       x           xxxxxx x  x        x      xxx    xxxx x  x        x      xxxxxxxxxxx   x  x        x      xxxxxxxxxxx x  x        x      x    xxxxxxxx x  x        x      xxxxxxxxxx
//──x────x──────x──────────────────x────x──────x─────────────────x────x──────x──────────────────x────x──────x─────────────────────────x──────x──────────────────x────x──────x────────────────────x────x──────x─────────────────
//       x     x                        x     x                       x     x                        x     x                          x     x                        x     x                          x     x
//       x    x                         x    x                        x    x                         x    x                           x    x                         x    x                           x    x
//       x    x                         x    x                        x    x                         x    x                           x    x                         x    x                           x    x
//       x   x                          x   x                         x   x                          x   x                            x   x                          x   x                            x   x
//        x x                            x x                           x x                            x x                              x x                            x x                              x x
//        xx                             xx                            xx                             xx                               xx                             xx                               xx
//        x                              x                             x                              x                                x                              x                                x", "Third Degree AV Block" },
//                {@"

//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

//─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
//", "Asystole"},
//                {@"                                                                            xxxxx
//                                                                           xx   xx                                                     xx         x xx
//                   xxxxx                                           xxx     x     x                        xxx             xxxx       x  xx       x   x                                             xxxxx
//                   x   xx                                         x  xx    x     x                       xx xxx          xx  x      x     x    xx    xx                            xx             xx   x
//         xxxxxx    x    x                   xxxxxxxx    xxxx xx  xx   xx   x     xxxxxxxxxx       xxxx   x    xxxxxxx  xx    x    x       x   xx      x    xxxxxxxx    xxxxx  xxx xx x       xxxxxx    xxxxxx         xxxxxxx
//       xxx    xx   x     xx xxxxxxxxxxx   xxx      xx  xx  xx xxx      x  xx              xxxxxxxxx   xx x           xxx     xxxxxx       xxxxx       xxxxxx      xxxxxx   xxxx  xx   xxxxxxxx              xxxxxxxxxxx     x
// xxxxxx        xxxxx      xxx         xxxx          xxxx               xxxx                            xx
//─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
//", "Ventricular Fibrillation"},
//                {@"                                                                                                                                                                     xx
//               x    x                                                                                                   x                           xx    xx         x x
//               xx   x                xx                                   x                          xxxx               x x                        x  x  x  x         x xxx            xx         xxx
//              x  x x x              x  x           x    x                xx                   xxx    x  x              x  x                        x   xx   x      xxxx   x           x  x       x   x
//      xx     x    x   x             x   x xx      x x  xx       xxx     x x      x           x   x   x   x xx    xxx  x    x        xx            x         xx    x       x xx       x    x     x    x
//     x  xx  x         x      xx     x    x  x     x x  x x      x xx x x  x     xx           x   x  x     x  x  x   xx     x       x  x          x           x   x        xx x       x    x     x    x    xxx  xxxxx
//    x    x  x         x     x x    x        x    x  x  x xx    xx   x x  x     x  x   x     x    x  x         xx            x     x   x         x            x   x            x     x      x    x    x   x   xx     x
//   x     x x          x     x x   x         x   x   x x   x    x         x    x   x  x x   x     x  x                       x     x    x       x             x   x            x    x        x  x     x   x           xxxxx
//   x     x x          x    x  x   x         x  x    x x   x    x         x   x    x  x x  x      x  x                        x   x     x      x              x   x            x   x          x x      xxx                 x
//  x      x x          x  x    x  x          x x     x x   xx  xx         x xx      xx   xx        xx                          xxxx      x    x               x  xx            xxxx            x                           xxx
// x       xx             x      xx           xx      x       xx            x                                                              xxxx                 xx
//─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────", "Ventricular Fibrillation"}
//            };

             
//            bool continueQuiz = true;
//            while (continueQuiz == true)
//            {
//                TitleMenu.WriteLogo();
//                Console.WriteLine("Would you like to begin your test? \n\nEnter 1 to begin, enter 2 to quit, enter 3 to see answer key.");
//                string? begin = Console.ReadLine();

//                if (begin == "1")
//                {
//                    Console.Clear();
//                    Console.WriteLine("For the following questions you will be presented with an image representing a telemetry waveform.\n\nThe images equate to a 10 second history of telemety monitoring.\n\nTo view the image in its entirety you will need to utilize the window scroll bar located at the bottom of the\napplication window.");
//                    Console.WriteLine();
//                    Console.WriteLine("Warning: Adjusting the window height or length will cause the images to distort. Following the prompts\nto restart the test will resolve the distortion.");
//                    Console.WriteLine();
//                    Console.WriteLine("(Press any key to continue...)");
//                    Console.ReadKey();
//                    Console.SetBufferSize(240, 66);
//                    List<string> randomKey = new(waves.Keys);

//                    var _random = new Random();
//                    var randomKeyList = randomKey.OrderBy(item => _random.Next());
//                    var questionKey = randomKeyList.ToArray();
//                    for (int i = 0; i < questionKey.Length; i++)
//                    {
//                        Console.Clear();
//                        Console.WriteLine(questionKey[i]);
//                        Console.WriteLine();
//                        Console.WriteLine("What rhythm does this represent?");
//                        var answer = waves[questionKey[i]];
//                        var userAnswer = Console.ReadLine();
//                        if (String.IsNullOrEmpty(userAnswer) && i == (questionKey.Length - 1))
//                        {
//                            Console.WriteLine($"That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nYou have completed the last question, congratulations!\n\n");
//                            Console.WriteLine("Enter any key to return to the main menu.");
//                            Console.WriteLine();
//                            Console.ReadKey();
//                            Console.Clear();
//                            break;
//                        }
//                        else if (String.IsNullOrEmpty(userAnswer))
//                        {
//                            Console.WriteLine("That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nEnter any key to continue or press 2 to to return to the main menu.");
//                            if (Console.ReadLine() == "2")
//                            {
//                                Console.Clear();
//                                break;
//                            }
//                        }
//                        else if (i == (questionKey.Length - 1) && userAnswer.ToUpper() == answer.ToUpper())
//                        {
//                            Console.WriteLine("Correct!\n\nYou have completed the last question, congratulations!\n\n");
//                            Console.WriteLine("Enter any key to return to the main menu.");
//                            Console.WriteLine();
//                            Console.ReadKey();
//                            Console.Clear();
//                            break;
//                        }

//                        else if (i == (questionKey.Length - 1) && userAnswer.ToUpper() != answer.ToUpper())
//                        {
//                            Console.WriteLine("That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nYou have completed the last question, congratulations!\n\n");
//                            Console.WriteLine("Enter any key to return to the main menu.");
//                            Console.WriteLine();
//                            Console.ReadKey();
//                            Console.Clear();
//                            break;
//                        }
//                        else if (userAnswer.ToUpper() == answer.ToUpper())
//                        {
//                            Console.WriteLine("Correct!");
//                            Console.WriteLine("Enter any key to continue or press 2 to quit.");
//                            if (Console.ReadLine() == "2")
//                            {
//                                Console.Clear();
//                                break;
//                            }
//                        }
//                        else if (userAnswer.ToUpper() != answer.ToUpper())
//                        {
//                            Console.WriteLine("That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nEnter any key to continue or press 2 to to return to the main menu.");
//                            if (Console.ReadLine() == "2")
//                            {
//                                Console.Clear();
//                                break;
//                            }
//                        }
//                        else
//                        {
//                            Console.WriteLine("That was not correct. Enter any key to continue or press 2 to to return to the main menu.");
//                            if (Console.ReadLine() == "2")
//                            {
//                                Console.Clear();
//                                break;
//                            }
//                        }
//                    }
//                }
//                else if (begin == "3")
//                {
//                    Console.CursorVisible = false;
//                    Console.Clear();
//                    Console.WriteLine("Below are the expected answers to the waveforms you will be asked to name during the test.\n\nPlease utilize the scroll bars to the top and right of your window to view the waveforms.\n\n[Warning]: Adjusting the window height or width will distort the images.\nReturning to the title menu will resolve the distortions.\n ");
//                    Console.WriteLine("(Press any key to return to the Title Menu...)\n");
//                    foreach (KeyValuePair<string, string> kvp in waves)
//                    {
//                        Console.SetBufferSize(240, 350);
//                        Console.WriteLine($"Waveform: "+ kvp.Value);
//                        Console.WriteLine(kvp.Key);
//                        //Console.SetCursorPosition(0, 0);
                        
//                    };
//                    Console.WriteLine("(Press any key to return to the Title Menu...)");
//                    Console.SetCursorPosition(0,0);
//                    Console.Read();
//                    Console.Clear();
//                    Console.CursorVisible = true;

//                }
//                else if (begin == "2")
//                {
//                    Environment.Exit(0);
//                }
//                else
//                {
//                    Console.WriteLine("That is not an option, please press enter to continue");
//                    Console.ReadKey();
//                    Console.Clear();
//                }
//            }
        }
    }
}