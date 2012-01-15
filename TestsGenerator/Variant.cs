using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestsGenerator
{
    class Variant
    {
        private readonly List<TestQuestion> coreQuestions;
        private readonly bool mixAnswers;
        private List<int> questionsIndices;
        private List<int[]> answersIndices;

        public Variant(List<TestQuestion> _coreQuestions)
        {
            this.coreQuestions = _coreQuestions;
            questionsIndices = new List<int>();
            answersIndices = new List<int[]>();
        }

        public Variant(List<TestQuestion> _coreQuestions, bool _mixAnswers = false)
            : this(_coreQuestions)
        {
            this.mixAnswers = _mixAnswers;
        }

        public void AddQuestion(int questionIndex)
        {
            questionsIndices.Add(questionIndex);
            MixAnswers(questionIndex);
        }

        public void AddRangeQuestions(List<int> questionIndices)
        {
            foreach (int i in questionIndices)
                AddQuestion(i);
        }

        private void MixAnswers(int questionNumber)
        {
            if (this.mixAnswers == true)
                answersIndices.Add(Util.RandomizeRange(coreQuestions.ElementAt(questionNumber).numberOfAnswers()));
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < questionsIndices.Count; ++i)
            {
                int questionIndex = questionsIndices.ElementAt(i);
                result.AppendLine(coreQuestions.ElementAt(questionIndex).ToString(answersIndices.ElementAt(i)));
            }
            return result.ToString();
        }
    }

}
