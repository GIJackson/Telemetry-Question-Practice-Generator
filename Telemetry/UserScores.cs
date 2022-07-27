using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Telemetry
{
    /// <summary>
    /// Class used during testing portion of the program to create an object
    /// with values that are written to files correlating to the user. Should
    /// only be instantiated during the test process.
    /// </summary>
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
        /// <summary>
        /// Method to check for a .csv containing the current user's previous
        /// scores. Creates a new file if not found.
        /// </summary>
        /// <param name="scoreFilePath">string file path that represents the
        /// location of the user's scores, typically created after the user 
        /// class is instantiated.</param>
        /// <returns>True if file has to be created or is read and only contains
        /// the string field firstLine. False if neither are true.</returns>
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
        /// <summary>
        /// Reads all lines of the file located at the string representing 
        /// the file's path.
        /// </summary>
        /// <param name="scoreFilePath">string representing the file's path</param>
        /// <returns>field int _testID which is the length the string array made when
        /// the file is read.</returns>
        public int TestID(string scoreFilePath)
        {
            string[] lines = File.ReadAllLines(scoreFilePath);
            _testID = lines.Length;
            return _testID;
        }
        /// <summary>
        /// Writes to the indicated string file path depending on the required
        /// boolean values. 
        /// </summary>
        /// <param name="scoreFilePath">string presenting the file path where the test
        /// values are to be written</param>
        /// <param name="firstQuestion">boolean indicating that the test has just 
        /// started and to begin a new line in the appropriate file</param>
        /// <param name="lastQuestion">boolean indicated if the user is on the last 
        /// question of the test or if they quit early</param>
        public void CSVTestQuestion(string scoreFilePath, bool firstQuestion, bool lastQuestion)
        {
            switch (firstQuestion, lastQuestion)
            {
                //This means that the test has just started, and writes a new line without a carriage return
                //to begin the .csv line representing the values of this specific test.
                //TODO: Error handling
                case (true, false):
                    using (StreamWriter sw = new(scoreFilePath, true))
                    {
                        sw.Write($"{_testID},{user_ID},");
                    }
                    break;
                //This means that the user answered all questions and thus creates the appropriate values to
                //be written to the .csv line representing this specific test.
                case (false, true):
                    using (StreamWriter sw = new(scoreFilePath, true))
                    {
                        string theQuestionsAsked = string.Join(".", questionsAsked);
                        string theQuestionsAnswered = string.Join(".", questionsAnswered);
                        string correctOrIncorrectAnswers = string.Join(".", correctOrIncorrect);
                        sw.Write($"{theQuestionsAsked},{theQuestionsAnswered},{correctOrIncorrectAnswers},true,{_score},{_totalQuestions},");
                    }
                    break;

                //This means that the user did not answer all questions and thus creates the appropriate values to
                //be written to the .csv line representing this specific test.
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
        /// <summary>
        /// Method for every time the user answers correctly, increment _score by 1 and adds "Correct" to a list
        /// whose index correlates to the question asked. Otherwise, only adds "Incorrect" if user answers are incorrect
        /// </summary>
        /// <param name="answer">Boolean that is true or false if user input matches the answer of the question gievn
        /// during the testing portion of the program.</param>
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
        /// <summary>
        /// Parses the string variables containing the date and times of the beginning of the test and ending of the test
        /// and subtracts them to calculate the total time elapsed and writes to the .csv line representing this specific
        /// test.
        /// </summary>
        /// <param name="scoreFilePath">string file path that represents the .csv for this specific user.</param>
        public void CSVDateTimeElapsed(string scoreFilePath)
        {
            if (_endTime != null && _startTime != null)
            {
                string endTime = DateTime.ParseExact(_endTime, "yyyy'-'MM'-'dd HH':'mm':'ss'Z'", null).ToString("HH:mm:ss");
                string startTime = DateTime.ParseExact(_startTime, "yyyy'-'MM'-'dd HH':'mm':'ss'Z'", null).ToString("HH:mm:ss");
                DateTime endTimeParse = DateTime.ParseExact(endTime, "HH:mm:ss", null);
                DateTime startTimeParse = DateTime.ParseExact(startTime, "HH:mm:ss", null);
                _elapsed = endTimeParse.Subtract(startTimeParse);
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
