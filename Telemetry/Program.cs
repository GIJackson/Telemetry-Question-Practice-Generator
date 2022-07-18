using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Windows;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;

namespace Telemetry
{
    public class Program
    {
        public static void Main()
        {
            string OS = Environment.OSVersion.ToString();
            User user = new User();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "UserSaveData.csv");
            string scoreFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{user._firstName}_{user._firstName}_{user._lastName}.csv");
            bool validSave = user.CSVSaveReader(filePath);
            bool continueQuiz = true;

            Console.Title = "Telemetry Testing";
            TitleMenu titleMenu = new();
            TitleMenu.WriteLogo();
            if (validSave == false)
            {
                user.CSVWriterNew(filePath);
            }
            else if (validSave == true)
            {
                IEnumerable<string> names = user.firstNames.Zip(user.lastNames, (first, second) => first + " " + second);
                Dictionary<int, string> users = user.IDs.Zip(names, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
                Console.WriteLine("Please choose a user, or enter 0 to create a new user:\n");
                for (int i = 1; i < users.Count; i++)
                {
                    Console.WriteLine("[" + i + "]" + users[i]);
                }
                string? input = Console.ReadLine();
                while (int.TryParse(input, out int n) == false)
                {
                    Console.WriteLine("Please choose a user, or enter 0 to create a new user.");
                    input = Console.ReadLine();
                }
                if (Convert.ToInt32(input) == 0)
                {
                    user.CSVWriterNew(filePath);
                    if (user._name != null)
                    {
                        users.Add(user._ID, user._name);
                    }
                    else
                    {
                        while (user._name == null)
                        {
                            user._name = $"{user._firstName} {user._lastName}";
                        }
                        users.Add(user._ID, user._name);
                    }

                }
                else if (int.TryParse(input, out int intput) == true && Convert.ToInt32(input) != 0)
                {
                    user.UserProperties(intput);
                }
            }
            Console.Clear();
            Dictionary<string, string> waves = new Dictionary<string, string>()
            {
                {@"              x                                          x                                          x                                          x                                          x
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
                   x                                          x                                          x                                          x                                          x", "Sinus Bradycardia" },
                {@"              x                                x                               x                               x                                x                                x                               x
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
                   x                                x                               x                               x                                x                                x                               x", "Normal Sinus Rhythm" },
                {@"               x                     x                     x                     x                     x                     x                     x                     x                     x                     x
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
                    x                     x                     x                     x                     x                     x                     x                     x                     x", "Sinus Tachycardia" },
                {@"              x                                            x                                       x                  x                 x                                        x                                     x
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
                    x                                            x                                       x                  x                 x                                        x", "Atrial Fibrillation"},
                {@"     x                           x                           x                           x                           x                           x                           x                           x
     x                           x                           x                           x                           x                           x                           x                           x
     x                           x                           x                           x                           x                           x                           x                           x
    x x                         x x                         x x                         x x                         x x                         x x                         x x                         x x
    x x                         x x                         x x                         x x                         x x                         x x                         x x                         x x
    x x                         x x                         x x                         x x                         x x                         x x                         x x                         x x
    x x                         x x                         x x                         x x                         x x                         x x                         x x                         x x
    x  x      x     x     x     x  x      x     x     x     x  x      x     x     x     x  x      x     x     x     x  x      x     x     x     x  x      x     x     x     x  x      x     x     x     x  x      x     x     x
    x  x     xxx   xxx   xxx    x  x     xxx   xxx   xxx    x  x     xxx   xxx   xxx    x  x     xxx   xxx   xxx    x  x     xxx   xxx   xxx    x  x     xxx   xxx   xxx    x  x     xxx   xxx   xxx    x  x     xxx   xxx   xxx
    x  x     x x   x x   x x    x  x     x x   x x   x x    x  x     x x   x x   x x    x  x     x x   x x   x x    x  x     x x   x x   x x    x  x     x x   x x   x x    x  x     x x   x x   x x    x  x     x x   x x   x x
x───x───x───x───x-x───x-x───x───x───x───x───x-x───x-x───x───x───x───x───x-x───x-x───x───x───x───x───x-x───x-x───x───x───x───x───x-x───x-x───x───x───x───x───x-x───x-x───x───x───x───x───x-x───x-x───x───x───x───x───x-x───x-x───
x   x   x   x    x     x    x   x   x   x    x     x    x   x   x   x    x     x    x   x   x   x    x     x    x   x   x   x    x     x    x   x   x   x    x     x    x   x   x   x    x     x    x   x   x   x    x     x
x   x   x   x               x   x   x   x               x   x   x   x               x   x   x   x               x   x   x   x               x   x   x   x               x   x   x   x               x   x   x   x
 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x
 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x
 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x                 x x     x x
  x       x                   x       x                   x       x                   x       x                   x       x                   x       x                   x       x                   x       x
          x                           x                           x                           x                           x                           x                           x                           x
          x                           x                           x                           x                           x                           x                           x                           x", "Atrial Flutter" },
                {@"     xxxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx           xxx
    x    x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x
   x      x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x
   x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x      x
   x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x
   x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x
   x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x       x     x
  x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x         x   x
 x           x x           x x           x x           x x           x x           x x           x x           x x           x x           x x           x x           x x           x x           x x           x x
x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x─────────────x──────────", "Ventricular Tachycardia"},
                {@"             xxx                                             x xx         x x          xxx                                     x                                     xxx            xxx                 xxx
    x       x    x                  xx                      x    x       x   x        x   x                                   x x                      x           xx   xx         x   x      xx        x xx       xxx
   x x     x      x                x   x                     x   x      x    x       x     x    xxx                          x   x          xx        x x         x       x       x     x     x xx      x  x      xx x
  x  x    x         x              x    x        xxx         x   x      x     x      x     x   x   x                         x   x         x  x      x   x       x         x      x     x     x   x     x  x      x  x
  x  x    x          x      x      x    x      xx   x        x   x      x     x     x      x   x   xx    xxx        xx       x    x       x    x     x   x      x           x     x     x     x   x     x   x     x  x
  x  x    x          x     x xx    x    x     x      x       x   x      x      x   x       x   x    x   x   xx    xx  x      x     x      x    x     x    x     x           x     x     x     x   x     x   x     x  x     xx
  x  x   x            x    x   x  x     x    x        x     x     x    x       x   x       x   x    x  x      xxxx     x    x      x      x    x     x    x    x            x     x     x     x    x    x    x    x  x     x
 x   x   x             x  x    x x      x   x          x   x       x   x       x  x        x  x      xx                 xxxx       x     x     x     x    x    x            x    x      xx   x     x    x    x   x   x     x
 x   x   x              xx      x        xxx            xxx         xx          xx          xx                                      x   x      x    x      x   x            x    x       x  x      xxxxx     x   x   x    x
 x    xxxx                                                                                                                           xxx        xxxx        xxx             xx xx         xx                 xxxx     x  xx
x─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────x───────────────────────────────────────xxx────", "Ventricular Tachycardia"},
                {@"                                                                                                                                                                                                               x           x
                                                                         xx         xx       xx         xxx                                                                                         xx         xx        xx x
                                                                xx     xx  x        x x     x  xx       x  x         xx                                                                            x  x        x x      x
                                                           x   x  xx  x     x      x  x    x    x       x  x        x  x                                                                     xx    x   x      x   x     x
                                                          x x  x   x  x     x      x  x    x    x      x    x      x    x                                                                   x x    x   x      x   x    x
                                      x     x     x       x x  x   x  x     x     x    x   x    x     x     x      x    x        xx         x                                               x x    x   x      x   x    x
                              xx     x x   x x   x  x    x  x  x   x  x     x     x    x   x    x     x     x      x    x        x x       x x            xx                               x  x     x  x      x    x   x
                             x   x  x   x x   x  x  x    x  x  x   x  x     x    x    x    x    x     x     x     x     x        x  x      x  x          x  x     xx     x        xx       x   x    x   x    x     x   x
              x      xxx     x   x  x   x x   x  x  x   x   x  x   x  x     x    x    x    x    x     x     x     x     x        x   x    x     x      x     x   x  x   x x      x  x      x   x    x    x   x     x   x
      x      x x    x   x   x    x  x   x x   x  x  x   x   x  x   x  x     x   x     x    x    x     x     x     x     x       x    x   x       x    x       x  x  x  x  x     x    x    x    x    x    x   x     x   x
     x x    x   x  x    x   x    x x   x x    x x    x x    x x    x  x     x   x     x   x     x    x       x    x      x    xx     x  x       x    x        x  x  x  x   x    x    x    x     x   x    x   x     x   x
    x   x  x    x x      xxx      x     x      x      x      x      xx       x x       x  x     x    x       x   x       x   x      x   x       x    x        x  x  x  x    x   x     x  x      x   x    x  x      xx x
xxxx     xx      x                                                            x         xx       xxxx         xxx         xxx        xxx         xxxx          xx    xx      xxx       xxx       xx       xx         x
─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────", "Ventricular Tachycardia"},
                {@"          x                            x                            x                            x                            x                            x                            x                            x
         x x                          x x                          x x                          x x                          x x                          x x                          x x                          x x
         x x                          x x                          x x                          x x                          x x                          x x                          x x                          x x
         x  x                         x  x                         x  x                         x  x                         x  x                         x  x                         x  x                         x  x
         x   x                        x   x                        x   x                        x   x                        x   x                        x   x                        x   x                        x   x
         x   x                        x   x                        x   x                        x   x                        x   x                        x   x                        x   x                        x   x
         x   x                        x   x                        x   x                        x   x                        x   x                        x   x                        x   x                        x   x
        x    x          xxx          x    x          xxx          x    x          xxx          x    x          xxx          x    x          xxx          x    x          xxx          x    x          xxx          x    x
        x     x        x   x         x     x        x   x         x     x        x   x         x     x        x   x         x     x        x   x         x     x        x   x         x     x        x   x         x     x
xxxxx   x     x       x     xxxxxx   x     x       x     xxxxxx   x     x       x     xxxxxx   x     x       x     xxxxxx   x     x       x     xxxxxx   x     x       x     xxxxxx   x     x       x     xxxxxx   x     x
─────x──x─────x───────x───────────x──x─────x───────x───────────x──x─────x───────x───────────x──x─────x───────x───────────x──x─────x───────x───────────x──x─────x───────x───────────x──x─────x───────x───────────x──x─────x───
      x x     x   xx  x            x x     x   xx  x            x x     x   xx  x            x x     x   xx  x            x x     x   xx  x            x x     x   xx  x            x x     x   xx  x            x x     x
       x       x x x  x             x       x x x  x             x       x x x  x             x       x x x  x             x       x x x  x             x       x x x  x             x       x x x  x             x       x x
                x   xx                       x   xx                       x   xx                       x   xx                       x   xx                       x   xx                       x   xx                       x", "Junctional Rhythm" },
                {@"               x                                x                                x               x               x               x               x               x               x               x               x
               xx                               xx                               x               x               x               x               x               x               x               x               x
               x x                              x x                              x               x               x               x               x               x               x               x               x
               x  x                             x  x                            x x             x x             x x             x x             x x             x x             x x             x x             x x
               x  x                             x  x                            x x             x x             x x             x x             x x             x x             x x             x x             x x
               x  x                             x  x                            x x             x x             x x             x x             x x             x x             x x             x x             x x
               x  x         xx                  x  x         xx                 x x             x x             x x             x x             x x             x x             x x             x x             x x
    xx         x  x        x  x      xx         x  x        x  x     xx         x  x      x     x  x      x     x  x      x     x  x      x     x  x      x     x  x      x     x  x      x     x  x      x     x  x      x
   x  x        x   x      x    x    x  x        x   x      x    x   x  x        x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x
 xx    xxxxx   x   x     x      xxxx    xxxxx   x   x     x      xxx    xxxx    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x    x  x     x x
───────────xx──x───x────x───────────────────xx──x───x────x──────────────────x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x───x
            x  x   xx  x                     x  x   xx  x                   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x
            x xx    x xx                     x x     x xx                   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x   x
             xx     x x                       xx     x x                     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x
             x      x x                       x      x x                     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x
                    xx                               xx                      x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x     x x
                    xx                               xx                       x       x       x       x       x       x       x       x       x       x       x       x       x       x       x       x       x       x
                    x                                x                                x               x               x               x               x               x               x               x               x
                                                                                      x               x               x               x               x               x               x               x               x", "Paroxysmal Supraventricular Tachycardia" },
                {@"               x                                                                         x                                x                               x                               x
               xx                              x                                         xx                               xx                              xx                              xx
               x x                            x x                                        x x                              x x                             x x                             x x
               x  x                           x x                                        x  x                             x  x                            x  x                            x  x
               x  x                          x  x                                        x  x                             x  x                            x  x                            x  x
               x  x                          x  x                                        x  x                             x  x                            x  x                            x  x
               x  x         xx               x  x                                        x  x         xx                  x  x         xx                 x  x         xx                 x  x         xx
    xx         x  x        x  x      xx     x    x        xx                  xx         x  x        x  x      xx         x  x        x  x     xx         x  x        x  x     xx         x  x        x  x      xx
   x  x        x   x      x    x    x  x   x     x       x  xx               x  x        x   x      x    x    x  x        x   x      x    x   x  x        x   x      x    x   x  x        x   x      x    x    x  x
 xx    xxxxx   x   x     x      xxxx    xxx      x      x     xxxxxxxxxxxxxxx    xxxxx   x   x     x      xxxx    xxxxx   x   x     x      xxx    xxxxx   x   x     x      xxx    xxxxx   x   x     x      xxxx    xxxxx
───────────xx──x───x────x────────────────────────x──────x────────────────────────────xx──x───x────x───────────────────xx──x───x────x──────────────────xx──x───x────x──────────────────xx──x───x────x────────────────────
            x  x   xx  x                         x      x                             x  x   xx  x                     x  x   xx  x                    x  x   xx  x                    x  x   xx  x
            x xx    x xx                          x     x                             x x     x xx                     x x     x xx                    x x     x xx                    x x     x xx
             xx     x x                           x     x                              xx     x x                       xx     x x                      xx     x x                      xx     x x
             x      x x                            x   x                               x      x x                       x      x x                      x      x x                      x      x x
                    xx                             x   x                                      xx                               xx                              xx                              xx
                    xx                             x   x                                      xx                               xx                              xx                              xx
                    x                              x   x                                      x                                x                               x                               x
                                                    x xx
                                                    x x
                                                     x", "Premature Ventricular Contraction" },
                {@"               xxx                                        xxx                                        xxx                                        xxx                                         xxx
              x   x                                      x   x                                      x   x                                      x   x                                       x   x
              x   x                                      x   x                                      x   x                                      x   x                                       x   x
             x     x                                    x     x                                    x     x                                    x     x                                     x     x
             x     x                                    x     x                                    x     x                                    x     x                                     x     x
            x       x                                  x       x                                  x       x                                  x       x                                   x       x
            x       x                                  x       x                                  x       x                                  x       x                                   x       x
            x        x                                 x        x                                 x        x                                 x        x                                  x        x
           x         x                                x         x                                x         x                                x         x                                 x         x
xxxxxxxxxxxx         x                 xxxxxxxxxxxxxxxx         x                 xxxxxxxxxxxxxxxx         x                 xxxxxxxxxxxxxxxx         x                 xxxxxxxxxxxxxxxxx         x                 xxxxxxxxx
──────────────────────x───────────────x──────────────────────────x───────────────x──────────────────────────x───────────────x──────────────────────────x───────────────x───────────────────────────x───────────────x─────────
                      x             xx                           x             xx                           x             xx                           x             xx                            x             xx
                       x    xx     x                              x    xx     x                              x    xx     x                              x    xx     x                               x    xx     x
                        x  x  x   x                                x  x  x   x                                x  x  x   x                                x  x  x   x                                 x  x  x   x
                         xx    xxx                                  xx    xxx                                  xx    xxx                                  xx    xxx                                   xx    xxx", "Idioventricular Rhythm" },
                {@"                                   x                                                            x                                                             x                                                            x
                                   xx                                                           xx                                                            xx                                                           xx
                                   x x                                                          x x                                                           x x                                                          x
                                   x  x                                                         x  x                                                          x  x                                                         x
                                   x  x                                                         x  x                                                          x  x                                                         x
                                   x  x                                                         x  x                                                          x  x                                                         x
                                   x  x                                                         x  x                                                          x  x                                                         x
                                   x  x                                                         x  x                                                          x  x                                                         x
                                   x   x                                                        x   x                                                         x   x                                                        x
        xx                         x   x                             xx                         x   x                              xx                         x   x                             xx                         x
      xx  xx                       x   x                 xx        xx  xx                       x   x                 xx         xx  xx                       x   x                 xx        xx  xx                       x
     x      x                      x   xx              xx  xx     x      x                      x   xx              xx  xx      x      x                      x   xx              xx  xx     x      x                      x
xxxxx        xxxxxxxxxxxxxxxxxxxxxxx    x             x      xxxxx        xxxxxxxxxxxxxxxxxxxxxxx    x             x      xxxxxx        xxxxxxxxxxxxxxxxxxxxxxx    x             x      xxxxx        xxxxxxxxxxxxxxxxxxxxxxx
─────────────────────────────────────────x───────────x────────────────────────────────────────────────x───────────x─────────────────────────────────────────────────x───────────x────────────────────────────────────────────
                                          xx        x                                                  xx        x                                                   xx        x
                                            xx    xx                                                     xx    xx                                                      xx    xx
                                              xxxx                                                         xxxx                                                          xxxx", "First Degree AV Block" },
                {@"               x                                                 x                                                 x                                                            x
               xx                                                xx                                                xx                                                           xx
               x x                                               x x                                               x x                                                          x x
               x  x                                              x  x                                              x  x                                                         x  x
               x  x                                              x  x                                              x  x                                                         x  x
               x  x                                              x  x                                              x  x                                                         x  x
               x  x         xx                                   x  x         xx                                   x  x         xx                                              x  x         xx
    xx         x  x        x  x                   xx             x  x        x  x              xx                  x  x        x  x        xx                        xx         x  x        x  x                       xx
   x  x        x   x      x    x                 x  x            x   x      x    x            x  x                 x   x      x    x      x  x                      x  x        x   x      x    x                     x  x
 xx    xxxxx   x   x     x      xxxxxxxxxxxxxxxxx    xxxxxxxxx   x   x     x      xxxxxxxxxxxx    xxxxxxxxxxxxxx   x   x     x      xxxxxx    xxxxxxxxxxxxxxxxxxxxxx    xxxxx   x   x     x      xxxxxxxxxxxxxxxxxxxxx    xxx
───────────xx──x───x────x────────────────────────────────────xx──x───x────x────────────────────────────────────xx──x───x────x───────────────────────────────────────────────xx──x───x────x───────────────────────────────────
            x  x   xx  x                                      x  x   xx  x                                      x  x   xx  x                                                 x  x   xx  x
            x xx    x xx                                      x xx    x xx                                      x xx    x xx                                                 x xx    x xx
             xx     x x                                        xx     x x                                        xx     x x                                                   xx     x x
             x      x x                                        x      x x                                        x      x x                                                   x      x x
                    xx                                                xx                                                xx                                                           xx
                    xx                                                xx                                                xx                                                           xx
                    x                                                 x                                                 x                                                            x", "Second Degree AV Block"},
                {@"            x                xx                                  x                xx                                                                                        x                xx                  x
           x x              x  x                                x x              x  x                                                                                      x x              x  x                x x
    xx     x x            xx    xx                       xx     x x            xx    xx                       xx                       xx                           xx     x x            xx    xx       xx     x x
   x  x   x   x          x        x                     x  x   x   x          x        x                     x  x                     x  x                         x  x   x   x          x        x     x  x   x   x
xxx    xxx     x        x          xxxxxxxxxxxxxxxxxxxxx    xxx     x        x          xxxxxxxxxxxxxxxxxxxxx    xxxxxxxxxxxxxxxxxxxxx    xxxxxxxxxxxxxxxxxxxxxxxxx    xxx     x        x          xxxxx    xxx     x
───────────────x────────x───────────────────────────────────────────x────────x─────────────────────────────────────────────────────────────────────────────────────────────────x────────x───────────────────────────x────────
               x       x                                            x       x                                                                                                  x       x                            x       x
                x      x                                             x      x                                                                                                   x      x                             x      x
                x      x                                             x      x                                                                                                   x      x                             x      x
                 x    x                                               x    x                                                                                                     x    x                               x    x
                  x   x                                                x   x                                                                                                      x   x                                x   x
                   x  x                                                 x  x                                                                                                       x  x                                 x  x
                    x x                                                  x x                                                                                                        x x                                  x x
                     x                                                    x                                                                                                          x                                    x", "Second Degree AV Block"},
                {@"                                                    xxx
    xx            xx               xx          x   x   x         xx            xx               xx            xx                 xx            xx               xx            xx                 xx            xx
   x  x          x  x     xx      x  x        x x x     x       x  x          x  x     xx      x  x          x  x            xx x  x          x  x             x  x          x  x   xx          x  x          x  x
x  x  x        x     x   x  x     x  x        x  x       x      x  x        x     x   x  x     x  x        x     x          x  xx  x        x     x            x  x        x     x x  x         x  x        x     x
 x x  x        x      xxx    xxxx x  x       x           xxxxxx x  x        x      xxx    xxxx x  x        x      xxxxxxxxxxx   x  x        x      xxxxxxxxxxx x  x        x      x    xxxxxxxx x  x        x      xxxxxxxxxx
──x────x──────x──────────────────x────x──────x─────────────────x────x──────x──────────────────x────x──────x─────────────────────────x──────x──────────────────x────x──────x────────────────────x────x──────x─────────────────
       x     x                        x     x                       x     x                        x     x                          x     x                        x     x                          x     x
       x    x                         x    x                        x    x                         x    x                           x    x                         x    x                           x    x
       x    x                         x    x                        x    x                         x    x                           x    x                         x    x                           x    x
       x   x                          x   x                         x   x                          x   x                            x   x                          x   x                            x   x
        x x                            x x                           x x                            x x                              x x                            x x                              x x
        xx                             xx                            xx                             xx                               xx                             xx                               xx
        x                              x                             x                              x                                x                              x                                x", "Third Degree AV Block" },
                {@"

xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
", "Asystole"},
                {@"                                                                            xxxxx
                                                                           xx   xx                                                     xx         x xx
                   xxxxx                                           xxx     x     x                        xxx             xxxx       x  xx       x   x                                             xxxxx
                   x   xx                                         x  xx    x     x                       xx xxx          xx  x      x     x    xx    xx                            xx             xx   x
         xxxxxx    x    x                   xxxxxxxx    xxxx xx  xx   xx   x     xxxxxxxxxx       xxxx   x    xxxxxxx  xx    x    x       x   xx      x    xxxxxxxx    xxxxx  xxx xx x       xxxxxx    xxxxxx         xxxxxxx
       xxx    xx   x     xx xxxxxxxxxxx   xxx      xx  xx  xx xxx      x  xx              xxxxxxxxx   xx x           xxx     xxxxxx       xxxxx       xxxxxx      xxxxxx   xxxx  xx   xxxxxxxx              xxxxxxxxxxx     x
 xxxxxx        xxxxx      xxx         xxxx          xxxx               xxxx                            xx
─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
", "Ventricular Fibrillation"},
                {@"                                                                                                                                                                     xx
               x    x                                                                                                   x                           xx    xx         x x
               xx   x                xx                                   x                          xxxx               x x                        x  x  x  x         x xxx            xx         xxx
              x  x x x              x  x           x    x                xx                   xxx    x  x              x  x                        x   xx   x      xxxx   x           x  x       x   x
      xx     x    x   x             x   x xx      x x  xx       xxx     x x      x           x   x   x   x xx    xxx  x    x        xx            x         xx    x       x xx       x    x     x    x
     x  xx  x         x      xx     x    x  x     x x  x x      x xx x x  x     xx           x   x  x     x  x  x   xx     x       x  x          x           x   x        xx x       x    x     x    x    xxx  xxxxx
    x    x  x         x     x x    x        x    x  x  x xx    xx   x x  x     x  x   x     x    x  x         xx            x     x   x         x            x   x            x     x      x    x    x   x   xx     x
   x     x x          x     x x   x         x   x   x x   x    x         x    x   x  x x   x     x  x                       x     x    x       x             x   x            x    x        x  x     x   x           xxxxx
   x     x x          x    x  x   x         x  x    x x   x    x         x   x    x  x x  x      x  x                        x   x     x      x              x   x            x   x          x x      xxx                 x
  x      x x          x  x    x  x          x x     x x   xx  xx         x xx      xx   xx        xx                          xxxx      x    x               x  xx            xxxx            x                           xxx
 x       xx             x      xx           xx      x       xx            x                                                              xxxx                 xx
─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────", "Ventricular Fibrillation"}
            };
            while (continueQuiz == true)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                TitleMenu.WriteLogo();
                Console.WriteLine($"Hey, {user._firstName}!\n\nWould you like to begin your test? \n\nEnter 1 to begin, enter 2 to quit, enter 3 to see answer key.");
                string? begin = Console.ReadLine();
                if (begin == "1")
                {
                    Console.Clear();
                    Console.WriteLine("For the following questions you will be presented with an image representing a telemetry waveform.\n\nThe images equate to a 10 second history of telemety monitoring.\n\nTo view the image in its entirety you will need to utilize the window scroll bar located at the bottom of the\napplication window.\n");
                    Console.WriteLine("Warning: Adjusting the window height or length will cause the images to distort. Following the prompts\nto restart the test will resolve the distortion.\n");
                    Console.WriteLine("(Press any key to continue...)");
                    Console.ReadKey();
                    if (OperatingSystem.IsWindows())
                    {
                        Console.SetBufferSize(240, 66);
                    }
                    List<string> randomKey = new(waves.Keys);
                    var startTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
                    int score = 19;
                    var _random = new Random();
                    var randomKeyList = randomKey.OrderBy(item => _random.Next());
                    var questionKey = randomKeyList.ToArray();
                    for (int i = 0; i < questionKey.Length; i++)
                    {
                        Console.Clear();
                        Console.WriteLine(questionKey[i]);
                        Console.WriteLine();
                        Console.WriteLine("What rhythm does this represent?");
                        var answer = waves[questionKey[i]];
                        var userAnswer = Console.ReadLine();
                        if (String.IsNullOrEmpty(userAnswer) && i == (questionKey.Length - 1))
                        {
                            Console.WriteLine($"That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nYou have completed the last question, congratulations!\n\n");
                            Console.WriteLine("Enter any key to return to the main menu.");
                            Console.WriteLine();
                            Console.ReadKey();
                            Console.Clear();
                            score--;
                            using (StreamWriter sw = new StreamWriter(scoreFilePath, true))
                            {
                                sw.Write($"{i + 1}X:"); sw.Write($" {answer}. "); sw.Write($"You did not give an answer.\r\n");
                                sw.Write($"You scored {score}/19. ");
                            };
                            break;
                        }
                        else if (String.IsNullOrEmpty(userAnswer))
                        {
                            Console.WriteLine("That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nEnter any key to continue or press 2 to to return to the main menu.");
                            score--;
                            using (StreamWriter sw = new StreamWriter(scoreFilePath, true))
                            {
                                sw.Write($"{i + 1}X:"); sw.Write($" {answer}. "); sw.Write($"You did not give an answer.\r\n");
                            };
                            if (Console.ReadLine() == "2")
                            {
                                Console.Clear();
                                int quitScore = (18 - i);
                                score -= quitScore;
                                using (StreamWriter sw = new StreamWriter(scoreFilePath, true))
                                {
                                    sw.Write($"You quit early. You scored {score}/19. ");
                                }
                                break;
                            }
                        }
                        else if (i == (questionKey.Length - 1) && userAnswer.ToUpper() == answer.ToUpper())
                        {
                            Console.WriteLine("Correct!\n\nYou have completed the last question, congratulations!\n\n");
                            Console.WriteLine("Enter any key to return to the main menu.");
                            Console.WriteLine();
                            Console.ReadKey();
                            Console.Clear();
                            using (StreamWriter sw = new StreamWriter(scoreFilePath, true))
                            {
                                sw.Write($"{i + 1}✓:"); sw.Write($" {answer}. "); sw.Write($"You answered: {userAnswer}\r\n");
                                sw.Write($"You scored {score}/19. ");
                            };
                            break;
                        }

                        else if (i == (questionKey.Length - 1) && userAnswer.ToUpper() != answer.ToUpper())
                        {
                            Console.WriteLine("That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nYou have completed the last question, congratulations!\n\n");
                            Console.WriteLine("Enter any key to return to the main menu.");
                            Console.WriteLine();
                            Console.ReadKey();
                            Console.Clear();
                            score--;
                            using (StreamWriter sw = new StreamWriter(scoreFilePath, true))
                            {
                                sw.Write($"{i + 1}X:"); sw.Write($" {answer}. "); sw.Write($"You answered: {userAnswer}\r\n");
                                sw.Write($"You scored {score}/19. ");
                            };
                            break;
                        }
                        else if (userAnswer.ToUpper() == answer.ToUpper())
                        {
                            Console.WriteLine("Correct!");
                            Console.WriteLine("Enter any key to continue or press 2 to quit.");
                            using (StreamWriter sw = new StreamWriter(scoreFilePath, true))
                            {
                                sw.Write($"{i + 1}✓:"); sw.Write($" {answer}. "); sw.Write($"You answered: {userAnswer}\r\n");
                            };
                            if (Console.ReadLine() == "2")
                            {
                                Console.Clear();
                                int quitScore = (18 - i);
                                score -= quitScore;
                                using (StreamWriter sw = new StreamWriter(scoreFilePath, true))
                                {
                                    sw.Write($"You quit early. You scored {score}/19. ");
                                };
                                break;
                            }
                        }
                        else if (userAnswer.ToUpper() != answer.ToUpper())
                        {
                            Console.WriteLine("That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nEnter any key to continue or press 2 to to return to the main menu.");
                            score--;
                            using (StreamWriter sw = new StreamWriter(scoreFilePath, true))
                            {
                                sw.Write($"{i + 1}X:"); sw.Write($" {answer}. "); sw.Write($"You answered: {userAnswer}\r\n");
                            };
                            if (Console.ReadLine() == "2")
                            {
                                Console.Clear();
                                int quitScore = (18 - i);
                                score -= quitScore;
                                using (StreamWriter sw = new StreamWriter(scoreFilePath, true))
                                {
                                    sw.Write($"You quit early. You scored {score}/19. ");
                                }
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("That was not correct. Enter any key to continue or press 2 to to return to the main menu.");
                            score--;
                            using (StreamWriter sw = new StreamWriter(scoreFilePath, true))
                            {
                                sw.Write($"{i + 1}X:"); sw.Write($" {answer}. "); sw.Write($"You answered: {userAnswer}\r\n");
                            };
                            if (Console.ReadLine() == "2")
                            {
                                Console.Clear();
                                int quitScore = (18 - i);
                                score -= quitScore;
                                using (StreamWriter sw = new StreamWriter(scoreFilePath, true))
                                {
                                    sw.Write($"You quit early. You scored {score}/19. ");
                                }
                                break;
                            }
                        }
                    }
                    var endTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
                    TimeSpan elapsed = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
                    using (StreamWriter sw = new StreamWriter(scoreFilePath, true))
                    {
                        sw.WriteLine($"Time elapsed: {elapsed}");
                    };
                }
                else if (begin == "3")
                {
                    Console.CursorVisible = false;
                    Console.Clear();
                    if (OperatingSystem.IsWindows())
                    {
                        Console.SetBufferSize(240, 350);
                    }
                    Console.WriteLine("Below are the expected answers to the waveforms you will be asked to name during the test.\n\nPlease utilize the scroll bars to the top and right of your window to view the waveforms.\n\n[Warning]: Adjusting the window height or width will distort the images.\nReturning to the title menu will resolve the distortions.\n ");
                    Console.WriteLine("(Press any key to return to the Title Menu...)\n");
                    foreach (KeyValuePair<string, string> kvp in waves)
                    {
                        Console.WriteLine($"Waveform: " + kvp.Value);
                        Console.WriteLine(kvp.Key);

                    };
                    Console.WriteLine("(Press any key to return to the Title Menu...)");
                    Thread.Sleep(1);
                    Console.SetCursorPosition(0, 0);
                    Console.ReadKey();
                    Console.Clear();
                    Console.CursorVisible = true;

                } 
                else if (begin == "2")
                {
                    Environment.Exit(0);
                }
                else if (begin == "4")
                {
                    Console.Clear();
                    string[] bornWithFirstAndLastName;
                    bool viewScores = false;
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        var userSaveData = sr.ReadToEnd();
                        bornWithFirstAndLastName = userSaveData.Split('\r', '\n');
                    }
                    Console.WriteLine($"Hello, {bornWithFirstAndLastName[2]}! Would you like to view your past results? (Yes/No)\n");
                    string? viewScoresBool = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(viewScoresBool) == true)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid input\n\nWould you like to view your past results? (Yes/No)\n");
                        viewScoresBool = Console.ReadLine();
                    }
                    while (string.IsNullOrWhiteSpace(viewScoresBool) == false)
                    {
                        if (viewScoresBool.ToUpper() == "YES")
                        {
                            viewScores = true;
                        }
                        else if (viewScoresBool.ToUpper() == "NO")
                        {
                            Console.Clear();
                            continue;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid input\n\nWould you like to view your past results? (Yes/No)\n");
                            viewScoresBool = Console.ReadLine();
                        }
                    }
                    if (viewScores == true)
                    {
                        string[] scoreDataArray;
                        string scoreFile = $"{bornWithFirstAndLastName[2]}_{bornWithFirstAndLastName[4]}_Scores.txt";
                        var datePattern = new Regex(@"(0[1-9]|1[012])[- \/.](0[1-9]|[12][0-9]|3[01])[- \/.](19|20)\d\d\s[a-zA-Z]+\s(0?[1-9]|1[0-2]):([0-5]\d)\s?((?:[Aa]|[Pp])\.?[Mm]\.?)");
                        var scoresPattern = new Regex(@"You\sscored\s[0-9]+\/[0-9]+\.\sTime\selapsed:\s\d\d:\d\d:\d\d");
                        Console.WriteLine("Please type in the number next to the date and time you want to see the score for.\n");
                        using (StreamReader sr = new StreamReader(scoreFile))
                        {
                            //this is the list of all dates a test was taken
                            List<Match> scoreDates = new List<Match>();
                            List<Match> theScores = new List<Match>();
                            //this is a single string of the entire file
                            var scoreDataRead = sr.ReadToEnd();
                            //this is an array of strings that represent each line of the file
                            scoreDataArray = scoreDataRead.Split('\r').Select(scoreDataRead => scoreDataRead.Trim('\n')).ToArray();
                            //this uses regex to find each date and time a test was taken
                            MatchCollection dates = datePattern.Matches(scoreDataRead);
                            MatchCollection scores = scoresPattern.Matches(scoreDataRead);
                            //this adds those date and times to a list
                            foreach (Match date in dates)
                            {
                                scoreDates.Add(date);
                            }
                            //this adds the scores found below each date to a list
                            foreach (Match score in scores)
                            {
                                theScores.Add(score);
                            }
                            List<string> stringScoreDates = scoreDates.ConvertAll(x => x.ToString());
                            List<string> stringTheScores = theScores.ConvertAll(x => x.ToString());
                            //creates a dictionary using the dates as keys and the scores as values
                            var scoreDateDic = stringScoreDates.Zip(stringTheScores, (k, v) => new { k, v })
                            .ToDictionary(x => x.k, x => x.v);
                            //linq query syntax
                            var dateQuery =
                                from sDate in scoreDates
                                select sDate;

                            int theNumberThatComesBeforeTheDate = 1;
                            foreach (Match sDate in dateQuery)
                            {
                                Console.Write($"[{theNumberThatComesBeforeTheDate}]");
                                Console.Write(sDate.Value);
                                Console.Write("\n");
                                theNumberThatComesBeforeTheDate++;
                            };
                            ////method syntax foreach (var sDate in scoreDates.Select((value, index) => new { value, index }))
                            //    //Console.WriteLine($"{sDate.value}");
                            int stringScoreDatesCount = stringScoreDates.Count;
                            string? input = Console.ReadLine();
                            while(int.TryParse(input, out int n) == false)
                            {
                                Console.WriteLine("Please type in the number next to the date and time you want to see the score for.\n");
                                input = Console.ReadLine();
                            }
                            int whatsTheScore = Int32.Parse(input);
                            if (whatsTheScore <= stringScoreDatesCount && whatsTheScore > -1)
                            {
                                Console.WriteLine(scoreDateDic[(stringScoreDates[whatsTheScore - 1])]);
                            }
                            Console.WriteLine("Press any key to return to the main menu.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("That is not an option, please press enter to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}