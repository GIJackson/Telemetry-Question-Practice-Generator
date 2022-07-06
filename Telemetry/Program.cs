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
            Console.WriteLine("Checking for user save data... please standby...");
            Thread.Sleep(3500);
            string notNewUser = "no";
            string confirmNewUser = "yes";
            bool userOnFile = false;
            bool returningUser = false;
            bool newUser = false;
            int counter = 0;
            //counter at 0 means the program just loaded up, used to check for intial save data
            //counter at 1 means there is a useable save file
            //counter at 2 means there is some corruption issues in nead of correcting or save file changing
            while (userOnFile == false)
            {
                while (userOnFile == false && counter == 0)
                {
                    if (!File.Exists("UserSaveData.txt"))
                    {
                        string newFile = "UserSaveData.txt";
                        using (StreamWriter sw = new StreamWriter(newFile, false))
                        {
                            sw.WriteLine("A new user is born!");
                            counter++;
                        }
                    }
                    else if (File.Exists("UserSaveData.txt"))
                    {
                        using (StreamReader sr = new StreamReader("UserSaveData.txt"))
                        {
                            var userSaveData = sr.ReadToEnd();
                            string userBorn = "A new user is born!";
                            string[] bornWithFirstAndLastName = userSaveData.Split('\r', '\n');
                            if (bornWithFirstAndLastName[0] != userBorn)
                            {
                                Console.WriteLine("Current user save data appears corrupted or in use by another application.");
                                counter += 2;
                            }
                            else if (bornWithFirstAndLastName[0] == userBorn && bornWithFirstAndLastName.Length < 4 && string.IsNullOrEmpty(bornWithFirstAndLastName[2]))
                            {
                                counter++;
                            }
                            else if (bornWithFirstAndLastName[0] == userBorn && !string.IsNullOrEmpty(bornWithFirstAndLastName[2]) && !string.IsNullOrEmpty(bornWithFirstAndLastName[4]))
                            {
                                Console.WriteLine($"\nUser Save Data Detected!\n\nAre you {bornWithFirstAndLastName[2]} {bornWithFirstAndLastName[4]}? (Yes/No)");
                                string? yesThisIsMe = Console.ReadLine();
                                //returning user//
                                if (yesThisIsMe.ToUpper() == confirmNewUser.ToUpper())
                                {
                                    returningUser = true;
                                    userOnFile = true;
                                    counter++;
                                }
                                //possible new user//
                                else if (yesThisIsMe.ToUpper() == notNewUser.ToUpper())
                                {
                                    counter += 2;
                                }
                                else if (string.IsNullOrWhiteSpace(yesThisIsMe) || yesThisIsMe.ToUpper() != confirmNewUser.ToUpper() && yesThisIsMe.ToUpper() != notNewUser.ToUpper())
                                {
                                    Console.WriteLine("\nInput invalid.");
                                    for (; ; )
                                    {
                                        Console.WriteLine($"\nAre you {bornWithFirstAndLastName[2]} {bornWithFirstAndLastName[4]} (Yes/No)");
                                        yesThisIsMe = Console.ReadLine();
                                        //returning user//
                                        if (yesThisIsMe.ToUpper() == confirmNewUser.ToUpper())
                                        {
                                            userOnFile = true;
                                            returningUser = true;
                                            counter++;
                                            break;
                                        }
                                        //possible new user//
                                        else if (yesThisIsMe.ToUpper() == notNewUser.ToUpper())
                                        {
                                            Console.WriteLine($"To continue, you must overwrite previous user data or continue as {bornWithFirstAndLastName[2]} {bornWithFirstAndLastName[4]}.");
                                            counter += 2;
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Input invalid.");
                                            Thread.Sleep(1500);
                                            Console.Clear();
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                while (userOnFile == false && counter == 1)
                {
                    var file = Path.Combine(Directory.GetCurrentDirectory(), "UserSaveData.txt");
                    Console.WriteLine("\nNew user detected!");
                    Console.WriteLine("\nPlease type in your first name and press enter.");
                    string? userFirstName = Console.ReadLine();
                    Console.WriteLine("\nPlease type in your last name and press enter.");
                    string? userLaseName = Console.ReadLine();
                    using (var sw = File.AppendText(file))
                    {
                        sw.WriteLine(userFirstName);
                        sw.WriteLine(userLaseName);
                    }
                    using (var sr = new StreamReader(file))
                    {
                        var userSaveData = sr.ReadToEnd();
                        string[] bornWithFirstAndLastName = userSaveData.Split('\r', '\n');
                        if (string.IsNullOrWhiteSpace(userFirstName) || string.IsNullOrWhiteSpace(userLaseName))
                        {
                            Console.WriteLine("Your first and last name must consist of a combination of letters or numbers.");
                            sr.Close();
                            using (StreamWriter sw = new StreamWriter(file, false))
                            {
                                sw.WriteLine("A new user is born!");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"\nYou entered: {bornWithFirstAndLastName[2]} {bornWithFirstAndLastName[4]}\n\nIs this correct? (Yes/No)");
                            string? yesThisIsMe = Console.ReadLine();
                            if (yesThisIsMe.ToUpper() == confirmNewUser.ToUpper())
                            {
                                newUser = true;
                                userOnFile = true;
                            }
                            else if (yesThisIsMe.ToUpper() == notNewUser.ToUpper())
                            {
                                sr.Close();
                                using (StreamWriter sw = new StreamWriter(file, false))
                                {
                                    sw.WriteLine("A new user is born!");
                                }
                            }
                            else if (string.IsNullOrWhiteSpace(yesThisIsMe) || yesThisIsMe.ToUpper() != confirmNewUser.ToUpper() && yesThisIsMe.ToUpper() != notNewUser.ToUpper())
                            {
                                Console.WriteLine("Input invalid.");
                                Thread.Sleep(1500);
                                Console.Clear();
                                for (; ; )
                                {
                                    Console.WriteLine($"\nYou entered: {bornWithFirstAndLastName[2]} {bornWithFirstAndLastName[4]}\n\nIs this correct? (Yes/No)");
                                    yesThisIsMe = Console.ReadLine();
                                    if (yesThisIsMe.ToUpper() == confirmNewUser.ToUpper())
                                    {
                                        Console.WriteLine($"Welcome, {bornWithFirstAndLastName[2]} {bornWithFirstAndLastName[4]}!");
                                        userOnFile = true;
                                        break;
                                    }
                                    else if (yesThisIsMe.ToUpper() == notNewUser.ToUpper())
                                    {
                                        sr.Close();
                                        using (StreamWriter sw = new StreamWriter(file, false))
                                        {
                                            sw.WriteLine("A new user is born!");
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nInput invalid.");
                                        Thread.Sleep(1500);
                                        Console.Clear();
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                while (userOnFile == false && counter == 2)
                {
                    var file = Path.Combine(Directory.GetCurrentDirectory(), "UserSaveData.txt");
                    string? overWriteFile;
                    using (StreamReader sr = new StreamReader(file))
                    {
                        var userSaveData = sr.ReadToEnd();
                        string userBorn = "A new user is born!";
                        string[] bornWithFirstAndLastName = userSaveData.Split('\r', '\n');
                        if (bornWithFirstAndLastName[0] != userBorn)
                        {
                            Console.WriteLine("\nOverwrite current user save file? (Yes/No)");
                            overWriteFile = Console.ReadLine();
                            if (overWriteFile.ToUpper() == confirmNewUser.ToUpper())
                            {
                                sr.Close();
                                using (StreamWriter sw = new StreamWriter(file, false))
                                {
                                    sw.WriteLine("A new user is born!");
                                }
                                Thread.Sleep(1500);
                                Console.Clear();
                                counter--;
                            }
                            else if (overWriteFile.ToUpper() == notNewUser.ToUpper())
                            {
                                Console.WriteLine($"\nTo continue you must overwrite user save file.");
                            }
                            else if (string.IsNullOrWhiteSpace(overWriteFile) || overWriteFile.ToUpper() != confirmNewUser.ToUpper() && overWriteFile.ToUpper() != notNewUser.ToUpper())
                            {
                                Console.WriteLine("\nInput invalid.");
                                Thread.Sleep(1500);
                                Console.Clear();
                            }
                            else
                            {
                                break;
                            }
                        }
                        else if (bornWithFirstAndLastName[0] == userBorn && bornWithFirstAndLastName.Length == 7)
                        {
                            Console.WriteLine($"\nTo continue you must overwrite user save file or continue as {bornWithFirstAndLastName[2]} {bornWithFirstAndLastName[4]}.");
                            Console.WriteLine("\nOverwrite current user save file? (Yes/No)\n");
                            overWriteFile = Console.ReadLine();
                            if (overWriteFile.ToUpper() == confirmNewUser.ToUpper())
                            {
                                sr.Close();
                                using (StreamWriter sw = new StreamWriter(file, false))
                                {
                                    sw.WriteLine("A new user is born!");
                                }
                                Console.Clear();
                                counter--;
                            }
                            else if (overWriteFile.ToUpper() == notNewUser.ToUpper())
                            {
                                string? itsMe;
                                Console.WriteLine($"Continue as {bornWithFirstAndLastName[2]} {bornWithFirstAndLastName[4]}?");
                                itsMe = Console.ReadLine();
                                if (itsMe.ToUpper() == notNewUser.ToUpper())
                                {
                                    continue;
                                }
                                else if (itsMe.ToUpper() == confirmNewUser.ToUpper())
                                {
                                    userOnFile = true;
                                    counter--;
                                }
                                else if (string.IsNullOrWhiteSpace(itsMe) || itsMe.ToUpper() != confirmNewUser.ToUpper() && itsMe.ToUpper() != notNewUser.ToUpper())
                                {
                                    Console.WriteLine("\nInput invalid.");
                                    Thread.Sleep(1500);
                                    Console.Clear();
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else if (string.IsNullOrWhiteSpace(overWriteFile) || overWriteFile.ToUpper() != confirmNewUser.ToUpper() && overWriteFile.ToUpper() != notNewUser.ToUpper())
                            {
                                Console.WriteLine("\nInput invalid.");
                                Thread.Sleep(1500);
                                Console.Clear();
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("Processing...");
            Thread.Sleep(5000);
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
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UserSaveData.txt");
            bool continueQuiz = true;
            while (continueQuiz == true)
            {
                Console.WriteLine(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
                TitleMenu.WriteLogo();
                using (StreamReader sr = new StreamReader(filePath))
                {
                    var userSaveData = sr.ReadToEnd();
                    string[] bornWithFirstAndLastName = userSaveData.Split('\r', '\n');
                    if (newUser == true && returningUser == false)
                    {
                        Console.WriteLine($"\nWelcome, {bornWithFirstAndLastName[2]} {bornWithFirstAndLastName[4]}! Would you like to begin your test? \n\nEnter 1 to begin, enter 2 to quit, enter 3 to see answer key.");
                    }
                    else if (newUser == false && returningUser == true)
                    {
                        Console.WriteLine($"\nWelcome back, {bornWithFirstAndLastName[2]} {bornWithFirstAndLastName[4]}! Would you like to begin your test? \n\nEnter 1 to begin, enter 2 to quit, enter 3 to see answer key.");
                    }
                    else
                    {
                        Console.WriteLine($"\nWelcome! Would you like to begin your test? \n\nEnter 1 to begin, enter 2 to quit, enter 3 to see answer key.");
                    }
                }
                string? begin = Console.ReadLine();
                if (begin == "1")
                {
                    Console.Clear();
                    Console.WriteLine("For the following questions you will be presented with an image representing a telemetry waveform.\n\nThe images equate to a 10 second history of telemety monitoring.\n\nTo view the image in its entirety you will need to utilize the window scroll bar located at the bottom of the\napplication window.\n");
                    Console.WriteLine("Warning: Adjusting the window height or length will cause the images to distort. Following the prompts\nto restart the test will resolve the distortion.\n");
                    Console.WriteLine("(Press any key to continue...)");
                    Console.ReadKey();
                    Console.SetBufferSize(240, 66);
                    string startTime;
                    string endTime;
                    string userSaveData;
                    string[] bornWithFirstAndLastName;
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        userSaveData = sr.ReadToEnd();
                        bornWithFirstAndLastName = userSaveData.Split('\r', '\n');
                    };
                    string scoreFile = $"{bornWithFirstAndLastName[2]}_{bornWithFirstAndLastName[4]}_Scores.txt";
                    var scoreFilePath = Path.Combine(Directory.GetCurrentDirectory(), scoreFile);
                    using (StreamWriter sw = new StreamWriter(scoreFilePath, true))
                    {
                        sw.Write("This test was taken on: "); sw.Write(DateTime.Now.ToString("MM/dd/yyyy"));
                        sw.Write(" at "); sw.Write(DateTime.Now.ToString("hh:mm tt")); sw.Write("\r\n");
                    };
                    int score = 19;
                    List<string> randomKey = new(waves.Keys);
                    var _random = new Random();
                    var randomKeyList = randomKey.OrderBy(item => _random.Next());
                    var questionKey = randomKeyList.ToArray();
                    startTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
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
                            newUser = false;
                            returningUser = true;
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
                                };
                                newUser = false;
                                returningUser = true;
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
                            newUser = false;
                            returningUser = true;
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
                            newUser = false;
                            returningUser = true;
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
                                newUser = false;
                                returningUser = true;
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
                                };
                                newUser = false;
                                returningUser = true;
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
                                };
                                newUser = false;
                                returningUser = true;
                                break;
                            }
                        }
                    }
                    endTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
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
                    Console.WriteLine("Below are the expected answers to the waveforms you will be asked to name during the test.\n\nPlease utilize the scroll bars to the top and right of your window to view the waveforms.\n\n[Warning]: Adjusting the window height or width will distort the images.\nReturning to the title menu will resolve the distortions.\n ");
                    Console.WriteLine("(Press any key to return to the Title Menu...)\n");
                    foreach (KeyValuePair<string, string> kvp in waves)
                    {
                        Console.SetBufferSize(240, 350);
                        Console.WriteLine($"Waveform: " + kvp.Value);
                        Console.WriteLine(kvp.Key);

                    };
                    Console.WriteLine("(Press any key to return to the Title Menu...)");
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
