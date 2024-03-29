﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry
{
    /// <summary>
    /// Class that is substantiated and given values then added to a list to be sorted through to display to the user their past test scores.
    /// </summary>
    public class ViewScores
    {
        //This class is used to create an object that has the properties of a particular line from userscores, representing
        //a list of scores taken by a certain individual at a certain date and time containing information including but not limited to 
        //every question they answered for that particular test, every answer they input in relation to said question, their score and
        //and how long they took on the test.

        public ViewScores(int testID, int userID, string[] testQuestions, string[] testAnswers, string[] correctOrIncorrect,
                            bool testCompleted, int score, int totalQuestions, string date, string time, string timeElapsed, DateTime sortableDateTime )
        {
            this.TestID = testID;
            this.UserID = userID;
            this.TestQuestions = testQuestions;
            this.TestAnswers = testAnswers;
            this.CorrectOrIncorrect = correctOrIncorrect;
            this.TestCompleted = testCompleted;
            this.Score = score;
            this.TotalQuestions = totalQuestions;
            this.Date = date;
            this.Time = time;
            this.TimeElapsed = timeElapsed;
            this.SortableDateTime = sortableDateTime;
        }
        public int TestID { get; set; }
        public int UserID { get; set; }
        public string[] TestQuestions { get; set; }
        public string[] TestAnswers { get; set; }
        public string[] CorrectOrIncorrect { get; set; }
        public bool TestCompleted { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string TimeElapsed { get; set; }
        public DateTime SortableDateTime { get; set; }


    }
}
