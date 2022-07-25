using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Telemetry
{
    public class Program
    {
        public static void Main()
        {
            string OS = Environment.OSVersion.ToString();
            User user = new();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "UserSaveData.csv");
            bool validSave = user.CSVSaveReader(filePath);
            bool continueQuiz = true;
            string dateTimeNowu = DateTime.Now.ToString("u");
            DateTime myDate = DateTime.Parse(dateTimeNowu);
            Console.Write(myDate.ToLongDateString() + " "); Console.WriteLine(myDate.ToShortTimeString());
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
                while (int.TryParse(input, out int n) == false || n > users.Count - 1 || n < 0)
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
            WaveDictionary Waves = new WaveDictionary();
            string[] keys = Waves.CSVToBytesToArray();
            string[] values = Waves.CSVWaveNameToArray();
            Dictionary<string, string> waves = Waves.CSVToDictionary();
            while (continueQuiz == true)
            {
                if (!File.Exists(scoreFilePath))
                {
                    File.Create(scoreFilePath).Close();
                }
                Console.Write(myDate.ToLongDateString() + " "); Console.WriteLine(myDate.ToShortTimeString());
                TitleMenu.WriteLogo();
                Console.WriteLine($"Hey, {user._firstName}!\n\nWould you like to begin your test? \n\nEnter 1 to begin, enter 2 to see answer key, enter 3 to view past scores, or enter 0 to quit.");
                string? begin = Console.ReadLine();
                if (begin == "0")
                {
                    Environment.Exit(0);
                }
                else if (begin == "1")
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
                    string startDate = DateTime.Now.ToString("u");
                    string startTime = DateTime.Now.ToString("u");
                    userScores._startDate = startDate;
                    userScores._startTime = startTime;

                    //List of Keys from waves Dictionary--the ASCII waveforms
                    List<string> randomKey = new(waves.Keys);
                    Random _random = new();

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
                            Console.WriteLine("That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nEnter any key to continue or press 0 to return to the main menu.");
                            userScores.CSVTestScore(false);
                            if (Console.ReadLine() == "0")
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
                            Console.WriteLine("Enter any key to continue or press 0 to quit.");
                            userScores.CSVTestScore(true);
                            if (Console.ReadLine() == "0")
                            {
                                userScores.CSVTestQuestion(scoreFilePath, false, false);
                                Console.Clear();
                                break;
                            }
                        }
                        else if (userAnswer.ToUpper() != answer.ToUpper())
                        {
                            userScores.questionsAnswered.Add(userAnswer);
                            Console.WriteLine("That was not correct. The correct answer was " + waves[questionKey[i]] + "\n\nEnter any key to continue or press 0 to return to the main menu.");
                            userScores.CSVTestScore(false);
                            if (Console.ReadLine() == "0")
                            {
                                userScores.CSVTestQuestion(scoreFilePath, false, false);
                                Console.Clear();
                                break;
                            }
                        }
                        else
                        {
                            userScores.questionsAnswered.Add("An error has occured.");
                            Console.WriteLine("An error as occured. At this time your score will be deducted one point. Enter any key to continue or press 0 to return to the main menu.");
                            userScores.CSVTestScore(false);
                            if (Console.ReadLine() == "0")
                            {
                                userScores.CSVTestQuestion(scoreFilePath, false, false);
                                Console.Clear();
                                break;
                            }
                        }
                    }
                    string endTime = DateTime.Now.ToString("u");
                    userScores._endTime = endTime;
                    userScores.CSVDateTimeElapsed(scoreFilePath);
                }
                else if (begin == "2")
                {
                    Console.CursorVisible = false;
                    Console.Clear();
                    if (OperatingSystem.IsWindows())
                    {
                        Console.SetBufferSize(240, 350);
                    }
                    Console.WriteLine("Below are the expected answers to the waveforms you will be asked to name during the test.\n\nPlease utilize the scroll bars at the bottom and right of your window to view the waveforms.\n\n[Warning]: Adjusting the window height or width will distort the images.\nReturning to the title menu will resolve the distortions.\n ");
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
                else if (begin == "3")
                {
                    Console.Clear();
                    bool viewScores = false;
                    if (OperatingSystem.IsWindows())
                    {
                        Console.SetBufferSize(240, 350);
                    }
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
                        bool nullFile = scoreLinq.CSVNullFileChecker(scoreFilePath);
                        if (nullFile == false)
                        {
                            Console.WriteLine("What would you like to view? Type in the corresponding number for your selection.\n[0]Quit\n[1]Tests and Scores\n[2]Tests and Questions\n[3]Everything Possible");
                        }
                        else if (nullFile == true)
                        {
                            Console.WriteLine("Woah! Looks like you need to take a test first!\n\nPress any key to return to the main menu...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                        string? viewChoice = Console.ReadLine();
                        while(int.TryParse(viewChoice, out int s) == false)
                        {
                            Console.WriteLine("Input not recognized, please try again.");
                            viewChoice = Console.ReadLine();
                        }
                        int intViewChoice = Convert.ToInt32(viewChoice);
                        switch (intViewChoice)
                        {
                            case 0:
                                viewScores = false;
                                break;
                            case 1:
                                Console.WriteLine("In what order would you like to view your scores?\n[0]Quit\n[1]Date Ascending\n[2]Date Descnding\n[3]Score Ascending\n[4]Score Descending\n[5]Total Time Ascending\n[6]Total Time Descending");
                                string? linqOrder1 = Console.ReadLine();
                                if (int.TryParse(linqOrder1, out int s1) == false)
                                {
                                    Console.WriteLine("Input not recognized, please try again.");
                                    linqOrder1 = Console.ReadLine();
                                }
                                int intLinqOrder1 = Convert.ToInt32(linqOrder1);
                                switch (intLinqOrder1)
                                {
                                    case 0:
                                        viewScores = false;
                                        break;
                                    case 1:
                                        scoreLinq.AscendingDateLinq(scoreLinq.CSVReadLine(scoreFilePath));
                                        Console.WriteLine("Press any key to return to the previous menu...");
                                        Console.ReadKey();
                                        break;
                                    case 2:
                                        scoreLinq.DescendingDateLinq(scoreLinq.CSVReadLine(scoreFilePath));
                                        Console.WriteLine("Press any key to return to the previous menu...");
                                        Console.ReadKey();
                                        break;
                                    case 3:
                                        scoreLinq.AscendingScoreLinq(scoreLinq.CSVReadLine(scoreFilePath));
                                        Console.WriteLine("Press any key to return to the previous menu...");
                                        Console.ReadKey();
                                        break;
                                    case 4:
                                        scoreLinq.DescendingScoreLinq(scoreLinq.CSVReadLine(scoreFilePath));
                                        Console.WriteLine("Press any key to return to the previous menu...");
                                        Console.ReadKey();
                                        break;
                                    case 5:
                                        scoreLinq.AscendingTimeLinq(scoreLinq.CSVReadLine(scoreFilePath));
                                        Console.WriteLine("Press any key to return to the previous menu...");
                                        Console.ReadKey();
                                        break;
                                    case 6:
                                        scoreLinq.DescendingTimeLinq(scoreLinq.CSVReadLine(scoreFilePath));
                                        Console.WriteLine("Press any key to return to the previous menu...");
                                        Console.ReadKey();
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case 2:
                                Console.WriteLine("In what order would you like to view your tests?\n[0]Quit\n[1]Scores Ascending\n[2]Scores Descending");
                                string? linqOrder2 = Console.ReadLine();
                                if (int.TryParse(linqOrder2, out int s2) == false)
                                {
                                    Console.WriteLine("Input not recognized, please try again.");
                                    linqOrder2 = Console.ReadLine();
                                }
                                int intLinqOrder2 = Convert.ToInt32(linqOrder2);
                                switch (intLinqOrder2)
                                {
                                    case 0:
                                        viewScores = false;
                                        break;
                                    case 1:
                                        scoreLinq.AscendingScoreWithQuestionsLinq(scoreLinq.CSVReadLine(scoreFilePath));
                                        Console.WriteLine("Press any key to return to the previous menu...");
                                        Console.ReadKey();
                                        break;
                                    case 2:
                                        scoreLinq.DescendingScoreWithQuestionsLinq(scoreLinq.CSVReadLine(scoreFilePath));
                                        Console.WriteLine("Press any key to return to the previous menu...");
                                        Console.ReadKey();
                                        break;
                                }
                                break;
                            case 3:
                                Console.WriteLine("Well here you go!\n\nPlease keep in mind this may be long, so utilization of vertical scroll bars may be neccessary.\n");
                                Thread.Sleep(3000);
                                scoreLinq.AllOfItLinq(scoreLinq.CSVReadLine(scoreFilePath), user);
                                Console.WriteLine("Press any key to return to the previous menu...");
                                Console.ReadKey();
                                break;
                            default:
                                break;
                        }
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