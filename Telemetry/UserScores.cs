using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Telemetry
{
    
    public class UserScores
    {
        public int _testID, user_ID, _score, _totalQuestions;
        public string? _startTime, _endTime, _startDate;
        TimeSpan? _elapsed;
        public List<string> questionsAsked = new();
        public List<string> questionsAnswered = new();
        public List<string> correctOrIncorrect = new();
        string firstLine = "0,0,Test Questions,Test Answers, Correct or Incorrect,Test Completed,Score,Total Questions,Date,Time,Time Elapsed";

        public UserScores()
        {
            _score = 0;
        }
        public bool CSVNewScoreFile(string scoreFilePath)
        {
            if (!File.Exists(scoreFilePath))
            {
                using (StreamWriter sw = new(scoreFilePath, false))
                {
                    sw.WriteLine(firstLine);
                    return true;
                }
            }
            else if (File.Exists(scoreFilePath))
            {
                string [] lines = File.ReadAllLines(scoreFilePath);
                if (lines.Length == 0)
                {
                    using (StreamWriter sw = new(scoreFilePath, false))
                    {
                        sw.WriteLine(firstLine);
                    }
                    return true;
                }
            }
            return false;
        }
        public int TestID(string scoreFilePath)
        {
            string[] lines = File.ReadAllLines(scoreFilePath);
            _testID = lines.Length;
            return _testID;
        }
        public void CSVTestQuestion(string scoreFilePath, bool firstQuestion, bool lastQuestion)
        {
            switch (firstQuestion, lastQuestion)
            {
                case (true, false):
                    using (StreamWriter sw = new(scoreFilePath, true))
                    {
                        sw.Write($"{_testID},{user_ID},");
                    }
                    break;
                case (false, true):
                    using (StreamWriter sw = new(scoreFilePath, true))
                    {
                        string theQuestionsAsked = string.Join(".", questionsAsked);
                        string theQuestionsAnswered = string.Join(".", questionsAnswered);
                        string correctOrIncorrectAnswers = string.Join(".", correctOrIncorrect);
                        sw.Write($"{theQuestionsAsked},{theQuestionsAnswered},{correctOrIncorrectAnswers},true,{_score},{_totalQuestions},");
                    }
                    break;
                case (false, false):
                    using (StreamWriter sw = new(scoreFilePath, true))
                    {
                        string theQuestionsAsked = string.Join(".", questionsAsked);
                        string theQuestionsAnswered = string.Join(".", questionsAnswered);
                        string correctOrIncorrectAnswers = string.Join(".", correctOrIncorrect);
                        sw.Write($"{theQuestionsAsked},{theQuestionsAnswered},{correctOrIncorrectAnswers},false,{_score},{_totalQuestions},");
                    }
                    break;
            }
        }
        public void CSVTestScore(bool answer)
        {
            switch (answer)
            {
                case true:
                    _score++;
                    correctOrIncorrect.Add("Correct");
                    break;
                case false:
                    correctOrIncorrect.Add("Incorrect");
                    break;
            }
        }
        public void CSVDateTimeElapsed(string scoreFilePath)
        {
            if (_endTime != null && _startTime != null)
            {
                _elapsed = DateTime.ParseExact(_endTime, "yyyy'-'MM'-'dd HH':'mm':'ss'Z'", null).Subtract(DateTime.ParseExact(_startTime, "yyyy'-'MM'-'dd HH':'mm':'ss'Z'", null));
            }
            else
            {
                _startTime = "The start time and date could not be recorded.,";
                string nullElapsed = "The total time of the test could not be recorded.";
                using (StreamWriter sw = new(scoreFilePath, true))
                {
                    sw.Write($"{_startDate},"); sw.Write($"{_startTime},"); sw.WriteLine(nullElapsed);
                }
            }
            using (StreamWriter sw = new(scoreFilePath, true))
            {
                sw.Write($"{_startDate},"); sw.Write($"{_startTime},"); sw.WriteLine(_elapsed);
            }
        }
    }
}
