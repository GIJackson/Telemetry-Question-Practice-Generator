using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry
{
    /// <summary>
    /// Object representing the user to reference for the purpose of reading and writing 
    /// data to related files.
    /// </summary>
    public class User
    {
        public int _userID;
        public string? _firstName, _lastName, _name;
        public List<int> userIDs = new();
        public List<string> firstNames = new();
        public List<string> lastNames = new();
        /// <summary>
        /// Method called if CSVSaveReader returns false. If false because
        /// the string representing the file path is not found, creates it.
        /// Otherwise called to create a new user if the user indicates they
        /// are new.
        /// </summary>
        /// <param name="filePath">String representing the file path</param>
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
        /// <summary>
        /// Method called after the user provides and confirms input for their first and last name
        /// and adds these values to the appropriate fields.
        /// </summary>
        /// <param name="newID">int value generated based off how many previous userID values have been created</param>
        /// <param name="newFirstName">string value representing the user's first name</param>
        /// <param name="newLastName">string value representing the user's last name</param>
        public void CSVAddNewUserToLists(int newID, string newFirstName, string newLastName)
        {
            userIDs.Add(newID);
            firstNames.Add(newFirstName);
            lastNames.Add(newLastName);
        }
        /// <summary>
        /// If the user is a returning user this method uses
        /// the user input int to select their user from the
        /// displayed users and applies their fields appropriately.
        /// </summary>
        /// <param name="intput">int input from user that correlates to the index of related lists
        /// to apply appropriate values to the specified fields.</param>
        public void UserProperties(int intput)
        {
            _userID = userIDs[intput];
            _firstName = firstNames[intput];
            _lastName = lastNames[intput];
            _name = $"{_firstName} {_lastName}";
        }

        /// <summary>
        /// Reads the indicated .csv at the specified string file path. If file
        /// does not exist, creates it. If file is in invalid format, deletes
        /// and creates a new file at indicated file path. Otherwise, adds all previous
        /// users the User class list fields.
        /// </summary>
        /// <param name="filePath">String representing the file path</param>
        /// <returns>True if valid save, false if new or invalid save</returns>
        /// 
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
