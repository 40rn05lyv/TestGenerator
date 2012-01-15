using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace TestsGenerator
{
    class TestGenerator
    {
        private List<TestQuestion> coreQuestions = new List<TestQuestion>();
        private VariantGenerator variantGenerator;

        public string InputFileName;
        public string OutputFileName;

        public TestGenerator(string inputFileName, string outputFileName)
        {
            this.InputFileName = inputFileName;
            this.OutputFileName = outputFileName;
        }

        public void GenerateTest(int _numberVariants, int _numberQuestions, bool _mixAnswers = false)
        {
            if (_numberQuestions <= 0)
                throw new ArgumentOutOfRangeException("_numberQuestions", "Number of questions can't be less or equal to zero");
            if (_numberVariants <= 0)
                throw new ArgumentOutOfRangeException("_numberVariants", "Number of variants can't be less or equal to zero");


            Encoding asciiEncoding = Encoding.Default;
            string[] sampleText = asciiEncoding.GetString(File.ReadAllBytes(InputFileName)).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            TestQuestion _question = null;
            foreach (var s in sampleText)
            {
                if (s[0] == Separators.NEWQUESTION)
                {
                    if (_question != null)
                        coreQuestions.Add(_question);
                    _question = new TestQuestion(s.Substring(1).Trim());
                }
                else if (s[0] == Separators.CORRECTANSWER)
                    _question.addVariant(s.Substring(1).Trim(), true);
                else if (s[0] == Separators.WRONGANSWER)
                    _question.addVariant(s.Substring(1).Trim(), false);
            }
            _question = null;

            if (_numberQuestions > coreQuestions.Count)
                throw new ArgumentOutOfRangeException("_numberQuestions", "Number of questions in the test can't be larger than number of given questions");

            variantGenerator = new VariantGenerator(coreQuestions, _numberQuestions, _mixAnswers);
            List<Variant> VariantsSet = new List<Variant>(_numberVariants);

            for (int i = 0; i < _numberVariants; ++i)
            {
                VariantsSet.Add(variantGenerator.GetVariant());
            }

            //WriteToFile(VariantsSet);
            WriteToSeparateFile(VariantsSet);
        }

        public void WriteToFile(List<Variant> VariantsSet)
        {
            StringBuilder anyQuestion = new StringBuilder();
            using (FileStream s = File.Open(OutputFileName, FileMode.Create))
            {
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                foreach (Variant v in VariantsSet)
                {
                    anyQuestion.Clear();
                    anyQuestion.AppendLine(v.ToString());
                    byte[] arr = encoding.GetBytes(anyQuestion.ToString());
                    s.Write(arr, 0, arr.Length);
                }
            }
        }

        public void WriteToSeparateFile(List<Variant> VariantsSet)
        {
            StringBuilder anyQuestion = new StringBuilder();
            for (int i = 0; i < VariantsSet.Count; ++i)
            {
                string fileName = OutputFileName + i + ".txt";
                using (FileStream s = File.Open(fileName, FileMode.Create))
                {
                    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                    anyQuestion.Clear();
                    anyQuestion.Append(VariantsSet.ElementAt(i).ToString());
                    byte[] arr = encoding.GetBytes(anyQuestion.ToString());
                    s.Write(arr, 0, arr.Length);
                }
                Process.Start(fileName);
            }
        }

    }
}