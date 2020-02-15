using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConsoleApp1
{
    class Program
    {
        static int Acertos = 0;

        static Question[] questions = new Question[]
        {
            new Question("Qual a capital da Austrália?", new List<Option>(3)
            {
                new Option("Canberra", true),
                new Option("Sydney", false),
                new Option("Perth", false)
            }),
            new Question("Qual o nomenclatura química do vinagre?", new List<Option>(3)
            {
                new Option("Ácido clorídrico", false),
                new Option("Ácido propanóico", false),
                new Option("Ácido etanóico", true)
            }),
            new Question("Qual o maior país do mundo?", new List<Option>(3)
            {
                new Option("China", false),
                new Option("Estados Unidos", false),
                new Option("Rússia", true)
            })
        };

        private delegate Question GenQuestionDelegate(int i);

        static GenQuestionDelegate questionDelegate = GenerateQuestion;

        static void Main(string[] args)
        {
            for (int i = 0; i < 3; i++)
            {
                (int resultNum, Question resultQuestion) = ShowQuestion(questionDelegate, i);
                if (resultQuestion.Options[resultNum].Answer)
                {
                    Acertos++;
                }
                Console.Clear();
            }
            if (Acertos == 3)
            {
                Console.WriteLine($"Parabéns! Acertos: {Acertos}");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Que pena... Acertos: {Acertos}");
                Console.ReadLine();
            }
        }

        static private (int, Question) ShowQuestion(GenQuestionDelegate genQuestion, int index)
        {
            Question question = genQuestion(index);

            Console.WriteLine(question.QuestionText);

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(i + " - " + question.Options[i].OptionText);
            }

            int result = CheckNum();

            return (result, question);
 
        }

        static private Question GenerateQuestion(int i)
        {
            Question result = new Question();
            result.QuestionText = questions[i].QuestionText;

            int rng = new Random().Next(2);
            result.Options.Add(questions[i].Options[rng]);
            questions[i].Options.Remove(questions[i].Options[rng]);

            int rng2 = new Random().Next(1);
            result.Options.Add(questions[i].Options[rng2]);
            questions[i].Options.Remove(questions[i].Options[rng2]);

            result.Options.Add(questions[i].Options[0]);
            questions[i].Options.Remove(questions[i].Options[0]);

            return result;
        }

        private static int CheckNum()
        {
            bool ok = false;
            int id = 0;
            do
            {
                try
                {
                    id = Convert.ToInt32(Console.ReadLine());
                    ok = true;
                }
                catch
                {
                    Console.WriteLine("Por favor, somente números!");
                }
            } while (!ok);
            return id;
        }
    }

    class Question
    {
        public List<Option> Options { get; set; }
        public string QuestionText { get; set; }

        public Question(string text, List<Option> options)
        {
            Options = options;
            QuestionText = text;
        }
        
        public Question()
        {
            Options = new List<Option>();
        }
        
    }

    class Option
    {
        public string OptionText { get; set; }
        public bool Answer { get; set; }

        public Option(string text, bool hit)
        {
            OptionText = text;
            Answer = hit;
        }

        public Option()
        {

        }
    }
}
