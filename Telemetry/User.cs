using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry
{
    public class User
    {
        public int _ID;
        public string? _firstName, _lastName, _name;
        public List<int> IDs = new();
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
                            _ID = IDs.Count;
                            _firstName = input1;
                            _lastName = input2;
                            _name = $"{input1} {input2}";
                            sw.WriteLine(_ID + "," + _firstName + "," + _lastName);
                            CSVAddNewUserToLists(_ID, _firstName, _lastName);
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
            IDs.Add(newID);
            firstNames.Add(newFirstName);
            lastNames.Add(newLastName);
        }
        public void UserProperties(int intput)
        {
            _firstName = firstNames[intput];
            _lastName = lastNames[intput];
            _name = $"{_firstName} {_lastName}";
        }
        public bool CSVSaveReader(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                List<string> data = new List<string>();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    data.Add(line);
                }
                if (data[0] == "")
                {
                    sr.Close();
                    File.Delete(filePath);
                    return false;
                }
                else
                {
                    foreach (string item in data)
                    {
                        string[] split = item.Split(',');
                        IDs.Add(Convert.ToInt32(split[0]));
                        firstNames.Add(split[1]);
                        lastNames.Add(split[2]);
                    }
                }
            }
            return true;
        }
    }
}
