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
            Console.WriteLine("Are you a new user? YES/NO");
            string? newUser = Console.ReadLine();
            var confirmNewUser = "yes";
            var notNewUser = "no";
            bool user = false;
            int counter = 0;
            string file;
            string thatsMe;
        NEWUSER: while (user == false)
            {
                if (newUser.ToUpper() == notNewUser.ToUpper())
                {
                    user = true;
                }
                else if (newUser.ToUpper() == confirmNewUser.ToUpper())
                {
                    if (!File.Exists($@"UserSaveData.txt"))
                    {
                        string filePath = $@"UserSaveData.txt";
                        using (StreamWriter sw = new StreamWriter(filePath, true))
                        {
                            sw.WriteLine("A new user is born!");
                        }
                            counter++;
                    }
                    else if (File.Exists($@"UserSaveData.txt") && counter == 0)
                    {
                        using (StreamReader sr = new StreamReader($@"UserSaveData.txt"))
                        {
                            var userSaveData = sr.ReadToEnd();
                            string[] bornFnameLname = userSaveData.Split('\n');
                            if (bornFnameLname.Length == 4)
                            {
                                Console.WriteLine("Saved user data detected!");
                                user = true;
                                counter++;
                            }
                            else if (bornFnameLname.Length == 2)
                            {
                                if (bornFnameLname[0] == "A new user is born!\r")
                                {
                                    counter++;
                                }
                                else if (bornFnameLname[0] != "A new user is born!\r")
                                {
                                    goto SW;
                                }
                            }
                            else if (bornFnameLname.Length <= 1)
                            {
                                goto SW2;
                            }
                            continue;
                        }
                    SW: File.WriteAllText($@"UserSaveData.txt", string.Empty);
                    SW2: File.WriteAllText($@"UserSaveData.txt", "A new user is born!\n");
                    }
                    else if ((File.Exists($@"UserSaveData.txt") && counter == 1))
                    {
                        file = Path.Combine(Directory.GetCurrentDirectory(), $@"UserSaveData.txt");
                        Console.WriteLine("Please type in your first name and hit enter.");
                        var userFirstName = Console.ReadLine();
                        Console.WriteLine("Please type in your last name and hit enter.");
                        var userLaseName = Console.ReadLine();
                        using (var sw = File.AppendText(file))
                        {
                            sw.WriteLine(userFirstName);
                            sw.WriteLine(userLaseName);
                        }
                        Console.Clear();
                        using (StreamReader sr = new StreamReader(file))
                        {
                            var userSaveData = sr.ReadToEnd();
                            string[] bornFnameLname = userSaveData.Split('\n');
                            string firstName = bornFnameLname[1].Replace("\r", "");
                            string lastName = bornFnameLname[2].Replace("\r", "");
                            Console.WriteLine($"You entered {firstName} {lastName}. Is this correct?");
                        }
                        var userConfirm = Console.ReadLine();
                        if (userConfirm.ToUpper() == confirmNewUser.ToUpper())
                        {
                            counter++;
                            user = true;
                            continue;
                        }
                        else if (userConfirm.ToUpper() != confirmNewUser.ToUpper())
                        {
                            using (StreamWriter sw = new StreamWriter(file))
                            {
                                sw.WriteLine("A new user is born!\r");
                            }
                        }
                    }
                }
                else if (newUser == null || newUser == "")
                {
                    Console.WriteLine("No input detected. Please answer with Yes or No");
                    newUser = Console.ReadLine();
                    Console.Clear();

                }
                else
                {
                    Console.WriteLine("Input not recognized. Please answer with Yes or No");
                    newUser = Console.ReadLine();
                    Console.Clear();
                }
            }
            while (user == true)
            {
                if (!File.Exists($@"UserSaveData.txt"))
                {
                    Console.WriteLine("There is no user data saved.");
                    user = false;
                    newUser = confirmNewUser.ToUpper();
                    goto NEWUSER;
                }
                else if (File.Exists($@"UserSaveData.txt") && counter == 0)
                {
                    file = Path.Combine(Directory.GetCurrentDirectory(), $@"UserSaveData.txt");
                    using (StreamReader sr = new StreamReader(file))
                    {
                        string userBorn = "A new user is born!\r";
                        var userSaveData = sr.ReadToEnd();
                        string[] bornFnameLname = userSaveData.Split('\n');
                        string firstName;
                        string lastName;
                        if (bornFnameLname[0] != userBorn || bornFnameLname.Length != 4)
                        {
                            Console.WriteLine("The saved user data appears to be corrupted.");
                            if (bornFnameLname.Length == 3 || bornFnameLname.Length == 2 || bornFnameLname.Length == 1)
                            {
                                sr.Close();
                                using (StreamWriter sw = new StreamWriter(file))
                                {
                                    sw.WriteLine("A new user is born!\r");
                                }
                                user = false;
                                newUser = confirmNewUser.ToUpper();
                                counter++;
                                goto NEWUSER;
                            }
                        }
                        else if (bornFnameLname[0] == userBorn && bornFnameLname.Length == 4)
                        {
                            try
                            {
                                firstName = bornFnameLname[1].Replace("\r", "");
                                lastName = bornFnameLname[2].Replace("\r", "");
                            }
                            catch (System.IndexOutOfRangeException)
                            {
                                Console.WriteLine("The saved user data appears to be corrupted.");
                                sr.Close();
                                using (StreamWriter sw = new StreamWriter(file))
                                {
                                    sw.WriteLine("A new user is born!\r");
                                }
                                user = false;
                                newUser = confirmNewUser.ToUpper();
                                counter++;
                                goto NEWUSER;
                            }
                            firstName = bornFnameLname[1].Replace("\r", "");
                            lastName = bornFnameLname[2].Replace("\r", "");
                            string welcomeBack = "Welcome back " + firstName + "!";
                            Console.WriteLine($"The current user on file is {firstName} {lastName}. Is this correct?");
                            var userConfirm = Console.ReadLine();
                                if (userConfirm.ToUpper() == confirmNewUser.ToUpper())
                                {
                                    //Ideally this will cement the user name as the user and proceed to the main program
                                    Console.WriteLine(welcomeBack);
                                    counter++;
                                    break;
                                }
                                else if (userConfirm.ToUpper() != confirmNewUser.ToUpper())
                                {
                                    Console.Clear();
                                    Console.WriteLine("Apologies, but to continue as another user, previous user data must be deleted. \n\nDo you wish to continue?");
                                    var doIt = Console.ReadLine();
                                    if (doIt.ToUpper() == confirmNewUser.ToUpper())
                                    {
                                        sr.Close();
                                        using (StreamWriter sw = new StreamWriter(file))
                                        {
                                            sw.WriteLine("A new user is born!\r");
                                        }
                                        user = false;
                                        counter++;
                                        goto NEWUSER;
                                    }
                                    else if (doIt.ToUpper() == notNewUser.ToUpper())
                                    {
                                        Console.WriteLine($"Continue as {firstName} {lastName}?");
                                        thatsMe = Console.ReadLine();
                                        if (thatsMe.ToUpper() == confirmNewUser.ToUpper())
                                        {   //Ideally this will cement the user name as the user and proceed to the main program
                                        Console.WriteLine(welcomeBack);
                                        counter++;

                                        break;
                                        }
                                        else if (thatsMe.ToUpper() == notNewUser.ToUpper())
                                        {
                                            Console.WriteLine($"To continue, you must choose to overwrite user data or continue as {firstName} {lastName}.\n");
                                            Thread.Sleep(3000);
                                            goto JUSTCHOOSELOOP;
                                        }
                                    }
                                    JUSTCHOOSELOOP: for (doIt = notNewUser; ;)
                                        {
                                            Console.WriteLine($"Continue as {firstName} {lastName}?");
                                            var justChoose = Console.ReadLine();
                                            if (justChoose.ToUpper() == confirmNewUser.ToUpper())
                                            {
                                                user = false;
                                                goto NEWUSER;
                                            }
                                            else if (justChoose.ToUpper() == notNewUser.ToUpper())
                                            {
                                                Console.WriteLine("Do you wish to overwrite previous user data?");
                                                var justChoosePlease = Console.ReadLine();
                                                if (justChoosePlease.ToUpper() == confirmNewUser.ToUpper())
                                                {
                                                    using (StreamWriter sw = new StreamWriter(file))
                                                    {
                                                        sw.WriteLine("A new user is born!\n");
                                                    }
                                                    user = false;
                                                    goto NEWUSER;
                                                }
                                                else if (justChoosePlease.ToUpper() == notNewUser.ToUpper())
                                                {
                                                    Console.WriteLine($"To continue, you must choose to overwrite user data or continue as {firstName} {lastName}.\n");
                                                    Thread.Sleep(3000);
                                                    continue;
                                                }

                                            }
                                        }
                                    }
                                }
                        }


                }
                else if (File.Exists($@"UserSaveData.txt") && counter == 1)
                {
                    file = Path.Combine(Directory.GetCurrentDirectory(), $@"UserSaveData.txt");
                    using (StreamReader sr = new StreamReader(file))
                    {
                        string userBorn = "A new user is born!\r";
                        var userSaveData = sr.ReadToEnd();
                        string[] bornFnameLname = userSaveData.Split('\n');
                        string firstName;
                        string lastName;
                        if (bornFnameLname[0] != userBorn || bornFnameLname.Length != 4)
                        {
                            Console.WriteLine("The saved user data appears to be corrupted.");
                            user = false;
                            goto NEWUSER;
                        }
                        else if ((bornFnameLname[0] == userBorn && bornFnameLname.Length == 4))
                        {
                            user = true;
                            counter--;

                        }

                    }
                }
                else if (File.Exists($@"UserSaveData.txt") && counter == 2)
                {
                    file = Path.Combine(Directory.GetCurrentDirectory(), $@"UserSaveData.txt");
                    using (StreamReader sr = new StreamReader(file))
                    {
                        string userBorn = "A new user is born!\r";
                        var userSaveData = sr.ReadToEnd();
                        string[] bornFnameLname = userSaveData.Split('\n');
                        if (bornFnameLname[0] != userBorn && bornFnameLname.Length != 4)
                        {
                            Console.WriteLine("The saved user data appears to be corrupted.");
                            user = false;
                            goto NEWUSER;
                        }
                        string firstName = bornFnameLname[1].Replace("\r", "");
                        string welcome = "Welcome " + firstName + "!";
                        Console.WriteLine(welcome);
                        counter++;
                        continue;
                    }
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("The end... for now...");
            Console.ReadKey();
        }
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


        //bool continueQuiz = true;
        //while (continueQuiz == true)
        //{
        //    TitleMenu.WriteLogo();
        //    Console.WriteLine("Would you like to begin your test? \n\nEnter 1 to begin, enter 2 to quit, enter 3 to see answer key.");
        //    string? begin = Console.ReadLine();

        //    if (begin == "1")
        //    {
        //        Console.Clear();
        //        Console.WriteLine("For the following questions you will be presented with an image representing a telemetry waveform.\n\nThe images equate to a 10 second history of telemety monitoring.\n\nTo view the image in its entirety you will need to utilize the window scroll bar located at the bottom of the\napplication window.");
        //        Console.WriteLine();
        //        Console.WriteLine("Warning: Adjusting the window height or length will cause the images to distort. Following the prompts\nto restart the test will resolve the distortion.");
        //        Console.WriteLine();
        //        Console.WriteLine("(Press any key to continue...)");
        //        Console.ReadKey();
        //        Console.SetBufferSize(240, 66);
        //        List<string> randomKey = new(waves.Keys);

        //        var _random = new Random();
        //        var randomKeyList = randomKey.OrderBy(item => _random.Next());
        //        var questionKey = randomKeyList.ToArray();
        //        for (int i = 0; i < questionKey.Length; i++)
        //        {
        //            Console.Clear();
        //            Console.WriteLine(questionKey[i]);
        //            Console.WriteLine();
        //            Console.WriteLine("What rhythm does this represent?");
        //            var answer = waves[questionKey[i]];
        //            var userAnswer = Console.ReadLine();
        //            if (String.IsNullOrEmpty(userAnswer) && i == (questionKey.Length - 1))
        //            {
        //                Console.WriteLine($"That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nYou have completed the last question, congratulations!\n\n");
        //                Console.WriteLine("Enter any key to return to the main menu.");
        //                Console.WriteLine();
        //                Console.ReadKey();
        //                Console.Clear();
        //                break;
        //            }
        //            else if (String.IsNullOrEmpty(userAnswer))
        //            {
        //                Console.WriteLine("That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nEnter any key to continue or press 2 to to return to the main menu.");
        //                if (Console.ReadLine() == "2")
        //                {
        //                    Console.Clear();
        //                    break;
        //                }
        //            }
        //            else if (i == (questionKey.Length - 1) && userAnswer.ToUpper() == answer.ToUpper())
        //            {
        //                Console.WriteLine("Correct!\n\nYou have completed the last question, congratulations!\n\n");
        //                Console.WriteLine("Enter any key to return to the main menu.");
        //                Console.WriteLine();
        //                Console.ReadKey();
        //                Console.Clear();
        //                break;
        //            }

        //            else if (i == (questionKey.Length - 1) && userAnswer.ToUpper() != answer.ToUpper())
        //            {
        //                Console.WriteLine("That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nYou have completed the last question, congratulations!\n\n");
        //                Console.WriteLine("Enter any key to return to the main menu.");
        //                Console.WriteLine();
        //                Console.ReadKey();
        //                Console.Clear();
        //                break;
        //            }
        //            else if (userAnswer.ToUpper() == answer.ToUpper())
        //            {
        //                Console.WriteLine("Correct!");
        //                Console.WriteLine("Enter any key to continue or press 2 to quit.");
        //                if (Console.ReadLine() == "2")
        //                {
        //                    Console.Clear();
        //                    break;
        //                }
        //            }
        //            else if (userAnswer.ToUpper() != answer.ToUpper())
        //            {
        //                Console.WriteLine("That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nEnter any key to continue or press 2 to to return to the main menu.");
        //                if (Console.ReadLine() == "2")
        //                {
        //                    Console.Clear();
        //                    break;
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("That was not correct. Enter any key to continue or press 2 to to return to the main menu.");
        //                if (Console.ReadLine() == "2")
        //                {
        //                    Console.Clear();
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    else if (begin == "3")
        //    {
        //        Console.CursorVisible = false;
        //        Console.Clear();
        //        Console.WriteLine("Below are the expected answers to the waveforms you will be asked to name during the test.\n\nPlease utilize the scroll bars to the top and right of your window to view the waveforms.\n\n[Warning]: Adjusting the window height or width will distort the images.\nReturning to the title menu will resolve the distortions.\n ");
        //        Console.WriteLine("(Press any key to return to the Title Menu...)\n");
        //        foreach (KeyValuePair<string, string> kvp in waves)
        //        {
        //            Console.SetBufferSize(240, 350);
        //            Console.WriteLine($"Waveform: "+ kvp.Value);
        //            Console.WriteLine(kvp.Key);

        //        };
        //        Console.WriteLine("(Press any key to return to the Title Menu...)");
        //        Console.SetCursorPosition(0,0);
        //        Console.Read();
        //        Console.Clear();
        //        Console.CursorVisible = true;

        //    }
        //    else if (begin == "2")
        //    {
        //        Environment.Exit(0);
        //    }
        //    else
        //    {
        //        Console.WriteLine("That is not an option, please press enter to continue");
        //        Console.ReadKey();
        //        Console.Clear();
        //    }
        //}
    }
}
