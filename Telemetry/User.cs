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
        public string _firstName, _lastName;
        public string _name;

        public void CSVWriterNew(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = new(filePath, true))
                {
                    sw.WriteLine("ID" + "," + "First Name" + "," + "Last Name");
                }
            }
            using (StreamWriter sw = new(filePath, true))
            {
                sw.WriteLine(_ID + "," + _firstName + "," + _lastName);

            }
        }
        public List<string> CSVSaveReader(List<string> userData, int rowDataPosition, string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                userData = new List<string>();
                string? line = sr.ReadLine();
                while (line != null)
                {
                    string[] lineArray = line.Split(',');
                    for (int i = 0; i < line.Split(',').Count(); i++)
                    {
                        userData.Add(lineArray[rowDataPosition]);
                    }
                }
                return userData;
            }
        }
    }
}
