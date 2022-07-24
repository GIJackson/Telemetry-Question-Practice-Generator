﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Telemetry
{
    public class ScoreLinq
    {
        public List<ViewScores> _viewScoresList = new List<ViewScores>();
        public bool CSVNullFileChecker(string scoreFilePath)
        {
            string[] nullScoreLines = File.ReadAllLines(scoreFilePath);
            while (nullScoreLines.Length == 0)
            {
                return true;
            }
            return false;
        }
        public List<ViewScores> CSVReadLine(string scoreFilePath)
        {
            string[] scoreLines = File.ReadAllLines(scoreFilePath);
            List<ViewScores> viewScoresList = new();
            foreach (string line in scoreLines.Skip(1))
            {
                string[] lineArray = line.Split(',');
                if (lineArray.Length != 11)
                {
                    continue;
                }
                DateTime lineArray8Date = DateTime.ParseExact(lineArray[8], "yyyy'-'MM'-'dd HH':'mm':'ss'Z'", null);
                DateTime lineArray8Time = DateTime.ParseExact(lineArray[8], "yyyy'-'MM'-'dd HH':'mm':'ss'Z'", null);
                string stringLineArray8 = lineArray8Date.ToString("M/d/yyyy");
                string stringLineArray9 = lineArray8Time.ToString("hh:mm tt");
                
                viewScoresList.Add(new ViewScores(Convert.ToInt32(lineArray[0]), Convert.ToInt32(lineArray[1]),
                                               lineArray[2].Split('.'), lineArray[3].Split('.'), lineArray[4].Split('.'),
                                               Convert.ToBoolean(lineArray[5].ToLower()), Convert.ToInt32(lineArray[6]),
                                               Convert.ToInt32(lineArray[7]), stringLineArray8, stringLineArray9, lineArray[10]));
            }
            _viewScoresList = viewScoresList;
            return _viewScoresList;
        }
        public void AscendingDateLinq(List<ViewScores> viewScoresList)
        {
            
            var ordered = from v in viewScoresList
                          orderby v.TestID ascending
                          select v;

            Console.WriteLine("Test#" + "\t" + "Date      Time" + "\t\t" + "Time Spent" + "\t" + "Score");
            foreach (ViewScores v in ordered)
            {

                if (v.TestCompleted == false)
                {
                    Console.WriteLine(v.TestID + "\t" + v.Date + " " + v.Time + "\t" + v.TimeElapsed + "\t\t" + v.Score + "/" + v.TotalQuestions + "\t" + "You did not finish this test.");
                }
                else if (v.TestCompleted == true)
                {
                    Console.WriteLine(v.TestID + "\t" + v.Date + " " + v.Time + "\t" + v.TimeElapsed + "\t\t" + v.Score + "/" + v.TotalQuestions + "\t" + "You answered all of the questions.");
                }
            }
        }
        public void DescendingDateLinq(List<ViewScores> viewScoresList)
        {
            var ordered = from v in viewScoresList
                          orderby v.TestID descending
                          select v;
            Console.WriteLine("Test#" + "\t" + "Date      Time" + "\t\t" + "Time Spent" + "\t" + "Score");
            foreach (ViewScores v in ordered)
            {

                if (v.TestCompleted == false)
                {
                    Console.WriteLine(v.TestID + "\t" + v.Date + " " + v.Time + "\t" + v.TimeElapsed + "\t\t" + v.Score + "/" + v.TotalQuestions + "\t" + "You did not finish this test.");
                }
                else if (v.TestCompleted == true)
                {
                    Console.WriteLine(v.TestID + "\t" + v.Date + " " + v.Time + "\t" + v.TimeElapsed + "\t\t" + v.Score + "/" + v.TotalQuestions + "\t" + "You answered all of the questions.");
                }
            }
        }
        public void AscendingScoreLinq(List<ViewScores> viewScoresList)
        {
            var ordered = from v in viewScoresList
                          orderby v.Score ascending
                          select v;
            Console.WriteLine("Test#" + "\t" + "Date      Time" + "\t\t" + "Time Spent" + "\t" + "Score");
            foreach (ViewScores v in ordered)
            {

                if (v.TestCompleted == false)
                {
                    Console.WriteLine(v.TestID + "\t" + v.Date + " " + v.Time + "\t" + v.TimeElapsed + "\t\t" + v.Score + "/" + v.TotalQuestions + "\t" + "You did not finish this test.");
                }
                else if (v.TestCompleted == true)
                {
                    Console.WriteLine(v.TestID + "\t" + v.Date + " " + v.Time + "\t" + v.TimeElapsed + "\t\t" + v.Score + "/" + v.TotalQuestions + "\t" + "You answered all of the questions.");
                }
            }
        }
        public void DescendingScoreLinq(List<ViewScores> viewScoresList)
        {
            var ordered = from v in viewScoresList
                          orderby v.Score descending
                          select v;
            Console.WriteLine("Test#" + "\t" + "Date      Time" + "\t\t" + "Time Spent" + "\t" + "Score");
            foreach (ViewScores v in ordered)
            {

                if (v.TestCompleted == false)
                {
                    Console.WriteLine(v.TestID + "\t" + v.Date + " " + v.Time + "\t" + v.TimeElapsed + "\t\t" + v.Score + "/" + v.TotalQuestions + "\t" + "You did not finish this test.");
                }
                else if (v.TestCompleted == true)
                {
                    Console.WriteLine(v.TestID + "\t" + v.Date + " " + v.Time + "\t" + v.TimeElapsed + "\t\t" + v.Score + "/" + v.TotalQuestions + "\t" + "You answered all of the questions.");
                }
            }
        }
        public void AscendingTimeLinq(List<ViewScores> viewScoresList)
        {
            var ordered = from v in viewScoresList
                          orderby v.TimeElapsed ascending
                          select v;
            Console.WriteLine("Test#" + "\t" + "Date      Time" + "\t\t" + "Time Spent" + "\t" + "Score");
            foreach (ViewScores v in ordered)
            {

                if (v.TestCompleted == false)
                {
                    Console.WriteLine(v.TestID + "\t" + v.Date + " " + v.Time + "\t" + v.TimeElapsed + "\t\t" + v.Score + "/" + v.TotalQuestions + "\t" + "You did not finish this test.");
                }
                else if (v.TestCompleted == true)
                {
                    Console.WriteLine(v.TestID + "\t" + v.Date + " " + v.Time + "\t" + v.TimeElapsed + "\t\t" + v.Score + "/" + v.TotalQuestions + "\t" + "You answered all of the questions.");
                }
            }
        }
        public void DescendingTimeLinq(List<ViewScores> viewScoresList)
        {
            var ordered = from v in viewScoresList
                          orderby v.TimeElapsed descending
                          select v;
            Console.WriteLine("Test#" + "\t" + "Date      Time" + "\t\t" + "Time Spent" + "\t" + "Score");
            foreach (ViewScores v in ordered)
            {

                if (v.TestCompleted == false)
                {
                    Console.WriteLine(v.TestID + "\t" + v.Date + " " + v.Time + "\t" + v.TimeElapsed + "\t\t" + v.Score + "/" + v.TotalQuestions + "\t" + "You did not finish this test.");
                }
                else if (v.TestCompleted == true)
                {
                    Console.WriteLine(v.TestID + "\t" + v.Date + " " + v.Time + "\t" + v.TimeElapsed + "\t\t" + v.Score + "/" + v.TotalQuestions + "\t" + "You answered all of the questions.");
                }
            }
        }
        public void AscendingScoreWithQuestionsLinq(List<ViewScores> viewScoresList)
        {
            var ordered = from v in viewScoresList
                          orderby v.Score ascending
                          select v;
            
            Console.WriteLine($"{"Test",-10}{"Questions",-40}{"Your Answers"}");
            foreach (ViewScores v in ordered)
            {
                Console.WriteLine($"{v.TestID,-3}\nScore:{v.Score}/{v.TotalQuestions}");
                for (int i = 0; i < v.TestAnswers.Length; i++)
                {
                    int testQuestionLength = v.TestQuestions[i].Length;
                    Console.WriteLine($"{"",-10}{v.TestQuestions[i],-40}\"{v.TestAnswers[i]}\"  {v.CorrectOrIncorrect[i]}.");
                }
                Console.WriteLine();
            }
        }
        public void DescendingScoreWithQuestionsLinq(List<ViewScores> viewScoresList) 
        {
            var ordered = from v in viewScoresList
                          orderby v.Score descending
                          select v;

            Console.WriteLine($"{"Test",-10}{"Questions",-40}{"Your Answers"}");
            foreach (ViewScores v in ordered)
            {
                Console.WriteLine($"{v.TestID,-3}\nScore:{v.Score}/{v.TotalQuestions}");
                for (int i = 0; i < v.TestAnswers.Length; i++)
                {
                    int testQuestionLength = v.TestQuestions[i].Length;
                    Console.WriteLine($"{"",-10}{v.TestQuestions[i],-40}\"{v.TestAnswers[i]}\"  {v.CorrectOrIncorrect[i]}.");
                }
                Console.WriteLine();
            }
        }
        public void AllOfItLinq(List<ViewScores> viewScoresList, User user)
        {
            Console.WriteLine("TestID" + "\t" + "UserID" + "\t\t" + "User Name" + "\t" + "Date and Time Taken" + "\t"+ "Time Elapsed" + "\t" + "Score out of Total Questions" + "\n");
            foreach (ViewScores v in viewScoresList)
            {
                Console.WriteLine(v.TestID + "\t" + v.UserID + "\t" + user._name + "\t" + v.Date + " " + v.Time + " \t" + v.TimeElapsed + "\t\t" + v.Score + "/" + v.TotalQuestions + "\nQuestions Asked\t\t\t\t\tUser Answers\n");
                for (int i = 0; i < v.TestQuestions.Length; i++)
                {
                    Console.WriteLine($"{v.TestQuestions[i],-48}\"{v.TestAnswers[i]}\" {v.CorrectOrIncorrect[i]}.");
                }
                Console.WriteLine();
            }
        }
    }
}
