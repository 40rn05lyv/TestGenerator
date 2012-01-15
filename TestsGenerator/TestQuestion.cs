using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestsGenerator
{
    class TestQuestion
    {
        private string question;
        private List<string> _givenAnswers = new List<string>();
        private List<bool> _correctAnswers = new List<bool>();

        public int NumberOfAnswers() {
            return _givenAnswers.Count;
        }

        public void addVariant(string someAnswer, bool correct)
        {
            _givenAnswers.Add(someAnswer);
            _correctAnswers.Add(correct);
        }

        public int numberOfAnswers()
        {
            return _givenAnswers.Count;
        }

        public TestQuestion(string question)
        {
            this.question = question;
        }

        public string ToString(int[] answersOrder)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(this.question);
            for (int i=0; i<NumberOfAnswers(); ++i){
                result.Append(i+". ");
                result.AppendLine(_givenAnswers.ElementAt(answersOrder[i]));
            }
            return result.ToString();
        }
    }
}