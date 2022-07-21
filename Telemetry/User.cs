using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry
{
    public class User
    {
        public int _userID;
        public string? _firstName, _lastName, _name;
        public List<int> userIDs = new();
        public List<string> firstNames = new();
        public List<string> lastNames = new();

        public void CSVWriterNew(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = new(filePath, true))
                {
                    sw.WriteLine("0" + "," + "First Name" + "," + "Last Name");
                }
            }
            using (StreamWriter sw = new(filePath, true))
            {
                bool newUser = true;
                while (newUser == true)
                {
                    Console.WriteLine("Please type your first name and press enter.");
                    string? input1 = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(input1) != false)
                    {
                        Console.WriteLine("Your first name must be a combination of numbers or letters.");
                        Console.WriteLine("Please type your first name and press enter.");
                        input1 = Console.ReadLine();
                    }
                    Console.WriteLine("Please type your last name and press enter.");
                    string? input2 = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(input2) != false)
                    {
                        Console.WriteLine("Your last name must be a combination of numbers or letters.");
                        Console.WriteLine("Please type your first name and press enter.");
                        input2 = Console.ReadLine();
                    }
                    Console.WriteLine("You entered: " + input1 + " " + input2 + ". Is this correct?");
                    Console.WriteLine("Please type Yes or No.");
                    string? yes = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(yes) != false)
                    {
                        Console.WriteLine("Please type Yes or No.");
                        yes = Console.ReadLine();
                    }
                    while (string.IsNullOrWhiteSpace(yes) == false)
                    {
                        if (yes.ToUpper() == "YES")
                        {
                            _userID = userIDs.Count;
                            _firstName = input1;
                            _lastName = input2;
                            _name = $"{input1} {input2}";
                            sw.WriteLine(_userID + "," + _firstName + "," + _lastName);
                            CSVAddNewUserToLists(_userID, _firstName, _lastName);
                            newUser = false;
                            return;
                        }
                        else if (yes.ToUpper() == "NO")
                        {
                            newUser = true;
                            yes = "";
                            continue;
                        }
                        else if (yes.ToUpper() == "OR")
                        {
                            Console.WriteLine("Don't be cheeky.");
                            Console.WriteLine("Please type Yes or No.");
                            yes = Console.ReadLine();
                        }
                        else if (yes.ToUpper() == "YES OR NO")
                        {
                            Console.WriteLine("C'mon now, which one is it?");
                            Console.WriteLine("Please type Yes or No.");
                            yes = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Please type Yes or No.");
                            yes = Console.ReadLine();
                        }
                    }
                }
            }
            
        }
        public void CSVAddNewUserToLists(int newID, string newFirstName, string newLastName)
        {
            userIDs.Add(newID);
            firstNames.Add(newFirstName);
            lastNames.Add(newLastName);
        }
        public void UserProperties(int intput)
        {
            _userID = userIDs[intput];
            _firstName = firstNames[intput];
            _lastName = lastNames[intput];
            _name = $"{_firstName} {_lastName}";
        }
        public bool CSVSaveReader(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }
            using (StreamReader sr = new StreamReader(filePath))
            {
                List<string> data = new();
                string onlyLine = "0,First Name,Last Name";
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    data.Add(line);
                }
                if (data[0] == "" || data[0] != onlyLine)
                {
                    sr.Close();
                    File.Delete(filePath);
                    return false;
                }
                if (data[0] == onlyLine && data.Count == 1)
                {
                    foreach (string item in data)
                    {
                        string[] split = item.Split(',');
                        userIDs.Add(Convert.ToInt32(split[0]));
                        firstNames.Add(split[1]);
                        lastNames.Add(split[2]);
                    }
                    return false;
                }
                else
                {
                    foreach (string item in data)
                    {
                        string[] split = item.Split(',');
                        userIDs.Add(Convert.ToInt32(split[0]));
                        firstNames.Add(split[1]);
                        lastNames.Add(split[2]);
                    }
                }
            }
            return true;
        }
    }
}
