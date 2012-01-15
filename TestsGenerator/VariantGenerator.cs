using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestsGenerator
{
    class VariantGenerator
    {
        List<TestQuestion> questions;
        public int NumberOfQuestions;
        public bool mixAnswers;
        ArrayRandomizer<int> randomizer;
    
        public VariantGenerator(List<TestQuestion> questions, int _numberOfQuestions, bool _mixAnswers)
        {
            this.questions = questions;
            this.NumberOfQuestions = _numberOfQuestions;
            this.mixAnswers = _mixAnswers;
            
            int[] set = new int[questions.Count];
            for (int i = 0; i < questions.Count; ++i)
                set[i] = i;

            randomizer = new ArrayRandomizer<int>(new List<int>(set));
        }

        public Variant GetVariant()
        {
            Variant resVariant = new Variant(questions, mixAnswers);
            resVariant.AddRangeQuestions(randomizer.GetRandomSubsetFisherYates(NumberOfQuestions));
            return resVariant;
        }
    }
}