using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry
{
    public class Program
    {
        public static void Main()
        {
            string OS = Environment.OSVersion.ToString();
            User user = new User();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "UserSaveData.csv");
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
                Dictionary<int, string> users = user.userIDs.Zip(names, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
                Console.WriteLine("Please choose a user by entering the corresponding number, or enter 0 to create a new user:\n");
                for (int i = 1; i < users.Count; i++)
                {
                    Console.WriteLine("[" + i + "]" + users[i]);
                }
                string? input = Console.ReadLine();
                while (int.TryParse(input, out int n) == false)
                {
                    Console.WriteLine("Please choose a user by entering the corresponding number, or enter 0 to create a new user.");
                    input = Console.ReadLine();
                }
                if (Convert.ToInt32(input) == 0)
                {
                    user.CSVWriterNew(filePath);
                    if (user._name != null)
                    {
                        users.Add(user._userID, user._name);
                    }
                    else
                    {
                        while (user._name == null)
                        {
                            user._name = $"{user._firstName} {user._lastName}";
                            break;
                        }
                        users.Add(user._userID, user._name);
                    }

                }
                else if (int.TryParse(input, out int intput) == true && Convert.ToInt32(input) != 0)
                {
                    user.UserProperties(intput);
                }
            }
            string scoreFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{user._userID}_{user._firstName}_{user._lastName}.csv");
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
                    UserScores userScores = new();
                    bool newFile = userScores.CSVNewScoreFile(scoreFilePath);
                    if (newFile == true)
                    {
                        userScores._testID = 1;
                    }
                    else if (newFile == false)
                    {
                        userScores.TestID(scoreFilePath);
                    }
                    Console.Clear();
                    Console.WriteLine("For the following questions you will be presented with an image representing a telemetry waveform.\n\nThe images equate to a 10 second history of telemety monitoring.\n\nTo view the image in its entirety you will need to utilize the window scroll bar located at the bottom of the\napplication window.\n");
                    Console.WriteLine("Warning: Adjusting the window height or length will cause the images to distort. Following the prompts\nto restart the test will resolve the distortion.\n");
                    Console.WriteLine("(Press any key to continue...)");
                    Console.ReadKey();
                    if (OperatingSystem.IsWindows())
                    {
                        Console.SetBufferSize(240, 66);
                    }
                    userScores.user_ID = user._userID;
                    userScores.CSVTestQuestion(scoreFilePath, true, false);
                    string startTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt" +",");
                    userScores._startTime = startTime;

                    //List of Keys from waves Dictionary--the ASCII waveforms
                    List<string> randomKey = new(waves.Keys);
                    Random _random = new Random();

                    //The above Keys in randomized order
                    IOrderedEnumerable<string> randomKeyIEnumerable = randomKey.OrderBy(item => _random.Next());

                    //Brought into an array because the above cannot be accessed by index
                    string[] questionKey = randomKeyIEnumerable.ToArray();
                    int totalQuestions = questionKey.Length;
                    userScores._totalQuestions = totalQuestions;
                    for (int i = 0; i < questionKey.Length; i++)
                    {
                        Console.Clear();
                        string? answer = waves[questionKey[i]];
                        userScores.questionsAsked.Add(answer);
                        Console.WriteLine(questionKey[i]);
                        Console.WriteLine();
                        Console.WriteLine("What rhythm does this represent?");
                        string? userAnswer = Console.ReadLine();

                        if (String.IsNullOrEmpty(userAnswer) && i == (questionKey.Length - 1))
                        {
                            userScores.questionsAnswered.Add("You did not give an answer");
                            Console.WriteLine($"That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nYou have completed the last question, congratulations!\n\n");
                            Console.WriteLine("Enter any key to return to the main menu.");
                            Console.WriteLine();
                            Console.ReadKey();
                            userScores.CSVTestScore(false);
                            userScores.CSVTestQuestion(scoreFilePath, false, true);
                            Console.Clear();
                            break;
                        }
                        else if (String.IsNullOrEmpty(userAnswer))
                        {
                            userScores.questionsAnswered.Add("You did not give an answer");
                            Console.WriteLine("That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nEnter any key to continue or press 2 to to return to the main menu.");
                            userScores.CSVTestScore(false);
                            if (Console.ReadLine() == "2")
                            {
                                userScores.CSVTestQuestion(scoreFilePath, false, false);
                                Console.Clear();
                                break;
                            }
                        }
                        else if (i == (questionKey.Length - 1) && userAnswer.ToUpper() == answer.ToUpper())
                        {
                            userScores.questionsAnswered.Add(userAnswer);
                            Console.WriteLine("Correct!\n\nYou have completed the last question, congratulations!\n\n");
                            Console.WriteLine("Enter any key to return to the main menu.");
                            Console.WriteLine();
                            Console.ReadKey();
                            userScores.CSVTestScore(true);
                            userScores.CSVTestQuestion(scoreFilePath, false, true);
                            Console.Clear();
                            break;
                        }
                        else if (i == (questionKey.Length - 1) && userAnswer.ToUpper() != answer.ToUpper())
                        {
                            userScores.questionsAnswered.Add(userAnswer);
                            Console.WriteLine("That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nYou have completed the last question, congratulations!\n\n");
                            Console.WriteLine("Enter any key to return to the main menu.");
                            Console.WriteLine();
                            Console.ReadKey();
                            userScores.CSVTestScore(false);
                            userScores.CSVTestQuestion(scoreFilePath, false, true);
                            Console.Clear();
                            break;
                        }
                        else if (userAnswer.ToUpper() == answer.ToUpper())
                        {
                            userScores.questionsAnswered.Add(userAnswer);
                            Console.WriteLine("Correct!");
                            Console.WriteLine("Enter any key to continue or press 2 to quit.");
                            userScores.CSVTestScore(true);
                            if (Console.ReadLine() == "2")
                            {
                                userScores.CSVTestQuestion(scoreFilePath, false, false);
                                Console.Clear();
                                break;
                            }
                        }
                        else if (userAnswer.ToUpper() != answer.ToUpper())
                        {
                            userScores.questionsAnswered.Add(userAnswer);
                            Console.WriteLine("That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nEnter any key to continue or press 2 to to return to the main menu.");
                            userScores.CSVTestScore(false);
                            if (Console.ReadLine() == "2")
                            {
                                userScores.CSVTestQuestion(scoreFilePath, false, false);
                                Console.Clear();
                                break;
                            }
                        }
                        else
                        {
                            userScores.questionsAnswered.Add("An error has occured.");
                            Console.WriteLine("An error as occured. At this time your score will be deducted one point. Enter any key to continue or press 2 to to return to the main menu.");
                            userScores.CSVTestScore(false);
                            if (Console.ReadLine() == "2")
                            {
                                userScores.CSVTestQuestion(scoreFilePath, false, false);
                                Console.Clear();
                                break;
                            }
                        }
                    }
                    string endTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
                    userScores._endTime = endTime;
                    userScores.CSVDateTimeElapsed(scoreFilePath);
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
                    bool viewScores = false;
                    Console.WriteLine($"Hello, {user._firstName}! Would you like to view your past results? (Yes/No)");
                    string? viewScoresBool = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(viewScoresBool) == true)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid input\n\nWould you like to view your past results? (Yes/No)\n");
                        viewScoresBool = Console.ReadLine();
                    }
                    while (viewScores == false && viewScoresBool != null)
                    {
                        if (viewScoresBool.ToUpper() == "YES")
                        {
                            viewScores = true;
                            Console.Clear();
                        }
                        else if (viewScoresBool.ToUpper() == "NO")
                        {
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid input\n\nWould you like to view your past results? (Yes/No)\n");
                            viewScoresBool = Console.ReadLine();
                        }
                    }
                    while (viewScores == true)
                    {
                        ScoreLinq scoreLinq = new ScoreLinq();
                        scoreLinq.AscendingDateLinq(scoreLinq.CSVReadLine(scoreFilePath));
                        Console.ReadKey();
                        Console.Clear();
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