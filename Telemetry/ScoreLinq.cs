using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Telemetry
{
    public class ScoreLinq
    {
        public List<ViewScores> _viewScoresList = new List<ViewScores>();
        public List<ViewScores> CSVReadLine(string scoreFilePath)
        {
            string dateTimeRegex1 = @"0\d\/\d\d\/\d\d\d\d\s0\d:\d\d:\d\d\s[a-zA-Z][a-zA-Z]$";
            string dateTimeRegex2 = @"1\d\/\d\d\/\d\d\d\d\s1\d:\d\d:\d\d\s[a-zA-Z][a-zA-Z]$";
            string[] scoreLines = File.ReadAllLines(scoreFilePath);
            List<ViewScores> viewScoresList = new List<ViewScores>();
            foreach (string line in scoreLines.Skip(1))
            {
                string[] lineArray = line.Split(',');
                List<string> lineArray8 = new();
                bool dateTimeLine1 = Regex.IsMatch(lineArray[8], dateTimeRegex1);
                bool dateTimeLine2 = Regex.IsMatch(lineArray[8], dateTimeRegex2);
                if (dateTimeLine1 == true && dateTimeLine2 == false)
                {
                    lineArray8.Add(lineArray[8].Remove(0,1).Remove(10, 1).Remove(14, 3));
                }
                else if (dateTimeLine1 == false && dateTimeLine2 == true)
                {
                    lineArray8.Add(lineArray[8].Remove(16, 3));
                }
                else
                {
                    //If there is some error where the date and time does not match the above regex's, this is where I would
                    //need to do some error-handling
                    lineArray8.Add(lineArray[8]);
                }
                viewScoresList.Add(new ViewScores(Convert.ToInt32(lineArray[0]), Convert.ToInt32(lineArray[1]),
                                               lineArray[2].Split('.'), lineArray[3].Split('.'), lineArray[4].Split('.'),
                                               Convert.ToBoolean(lineArray[5].ToLower()), Convert.ToInt32(lineArray[6]),
                                               Convert.ToInt32(lineArray[7]), lineArray8[0], lineArray[9]));
            }
            _viewScoresList = viewScoresList;
            return _viewScoresList;
        }
        public void AscendingDateLinq(List<ViewScores> viewScoresList)
        {
            var ordered = from v in viewScoresList 
                          orderby v.DateAndTime ascending 
                          select v;
            Console.WriteLine("Test#" + "\t" + "Date      Time" + "\t\t" + "Time Spent" + "\t" + "Score");
            foreach (ViewScores v in ordered)
            {
                
                if (v.TestCompleted == false)
                {
                    Console.WriteLine(v.TestID + "\t" + v.DateAndTime + "\t" + v.TimeElapsed + "\t" + v.Score + "/" + v.TotalQuestions + "\t" + "You did not finish this test.");
                }
                else if (v.TestCompleted == true)
                {
                    Console.WriteLine(v.TestID + "\t" + v.DateAndTime + "\t" + v.TimeElapsed + "\t" + v.Score + "/" + v.TotalQuestions + "\t" + "You answered all of the questions.");
                }
            }
        }
    }
}
