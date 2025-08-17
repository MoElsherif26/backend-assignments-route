using System.Diagnostics;

namespace Exam_02
{
    #region Answer
    public class Answer : ICloneable
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }

        public Answer(int id, string text)
        {
            AnswerId = id;
            AnswerText = text;
        }

        public object Clone() => new Answer(AnswerId, AnswerText);

        public override string ToString() => $"{AnswerId}-{AnswerText}";
    }
    #endregion

    #region Question
    public abstract class Question : ICloneable
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }
        public List<Answer> Answers { get; set; }
        public Answer? RightAnswer { get; set; }

        protected Question(string header, string body, int mark)
        {
            Header = header;
            Body = body;
            Mark = mark;
            Answers = new List<Answer>();
        }

        public abstract void ShowQuestion();

        public object Clone() => MemberwiseClone();

        public override string ToString() => $"{Header} {Body} (Mark {Mark})";
    }
    #endregion

    #region MCQQuestion
    public class MCQQuestion : Question
    {
        public MCQQuestion(string body, int mark) : base("MCQ Question", body, mark) { }

        public override void ShowQuestion()
        {
            Console.WriteLine($"\n{Header}\tMark:{Mark}");
            Console.WriteLine(Body);
            foreach (var ans in Answers)
                Console.WriteLine(ans);
        }
    }
    #endregion

    #region TrueFalseQuestion
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion(string body, int mark) : base("True | False Question", body, mark) { }

        public override void ShowQuestion()
        {
            Console.WriteLine($"\n{Header}\tMark:{Mark}");
            Console.WriteLine(Body);
            foreach (var ans in Answers)
                Console.WriteLine(ans);
        }
    }
    #endregion

    #region Exam
    public abstract class Exam : IComparable
    {
        public int Time { get; set; }
        public int NumberOfQuestions { get; set; }
        public List<Question> Questions { get; set; }

        protected Exam(int time, int numQ)
        {
            Time = time;
            NumberOfQuestions = numQ;
            Questions = new List<Question>();
        }

        public abstract void ShowExam();

        public int CompareTo(object obj)
        {
            if (obj is Exam other)
                return Time.CompareTo(other.Time);
            return 0;
        }

        public override string ToString() => $"Exam: {NumberOfQuestions} Questions, Time: {Time} min";
    }
    #endregion

    #region PracticalExam
    public class PracticalExam : Exam
    {
        public PracticalExam(int time, int numQ) : base(time, numQ) { }

        public override void ShowExam()
        {
            int qNum = 1;
            foreach (var q in Questions)
            {
                q.ShowQuestion();
                Console.Write("Please Enter The answer Id: ");
                int ansId = int.Parse(Console.ReadLine()!);

                Console.WriteLine($"Question {qNum}: {q.Body}");
                Console.WriteLine($"Your Answer => {ansId}");
                Console.WriteLine($"Right Answer => {q?.RightAnswer?.AnswerText}\n");
                qNum++;
            }
        }
    }
    #endregion

    #region FinalExam
    public class FinalExam : Exam
    {
        public FinalExam(int time, int numQ) : base(time, numQ) { }

        public override void ShowExam()
        {
            int grade = 0;
            int total = 0;
            int qNum = 1;

            foreach (var q in Questions)
            {
                q.ShowQuestion();
                Console.Write("Please Enter The answer Id: ");
                int ansId = int.Parse(Console.ReadLine()!);

                var studentAnswer = q.Answers.Find(a => a.AnswerId == ansId);

                Console.WriteLine($"\nQuestion {qNum} : {q.Body}");
                Console.WriteLine($"Your Answer => {studentAnswer?.AnswerText}");
                Console.WriteLine($"Right Answer => {q?.RightAnswer?.AnswerText}\n");

                if (studentAnswer?.AnswerId == q?.RightAnswer?.AnswerId)
                    grade += q.Mark;

                total += q.Mark;
                qNum++;
            }

            Console.WriteLine($"Your Grade is {grade} from {total}");
            Console.WriteLine($"Time = {DateTime.Now - Process.GetCurrentProcess().StartTime}");
        }
    }
    #endregion

    #region Subject
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Exam? Exam { get; set; }

        public Subject(int id, string name)
        {
            SubjectId = id;
            SubjectName = name;
        }

        public void CreateExam()
        {
            Console.Write("Please Enter the type of Exam (1 for Practical | 2 for Final): ");
            int type = int.Parse(Console.ReadLine()!);

            Console.Write("Please Enter the Time For Exam (30–180): ");
            int time = int.Parse(Console.ReadLine()!);

            Console.Write("Please Enter the Number of questions: ");
            int numQ = int.Parse(Console.ReadLine()!);

            Exam = type == 1 ? new PracticalExam(time, numQ) : new FinalExam(time, numQ);

            for (int i = 0; i < numQ; i++)
            {
                Console.Write("Please Enter the Type of Question (1 for MCQ | 2 For True/False): ");
                int qType = int.Parse(Console.ReadLine()!);

                Console.Write("Please Enter Question Body: ");
                string body = Console.ReadLine()!;

                Console.Write("Please Enter Question Mark: ");
                int mark = int.Parse(Console.ReadLine()!);

                Question q;
                if (qType == 1)
                {
                    q = new MCQQuestion(body, mark);
                    for (int j = 1; j <= 3; j++)
                    {
                        Console.Write($"Please Enter Choice number {j}: ");
                        string choice = Console.ReadLine()!;
                        q.Answers.Add(new Answer(j, choice));
                    }
                }
                else
                {
                    q = new TrueFalseQuestion(body, mark);
                    q.Answers.Add(new Answer(1, "True"));
                    q.Answers.Add(new Answer(2, "False"));
                }

                Console.Write("Please Enter the right answer id for true false question 1 for true 2 for false: ");
                int rightId = int.Parse(Console.ReadLine()!);
                q.RightAnswer = q.Answers.Find(a => a.AnswerId == rightId)!;

                Exam.Questions.Add(q);
            }
        }

        public override string ToString() => $"{SubjectName} [{SubjectId}]";
    }
    #endregion

    #region Main
    class Program
    {
        static void Main()
        {
            Subject subject = new Subject(1, "OOP");
            subject.CreateExam();

            Console.Write("Do you want to Start Exam (Y|N): ");
            if (Console.ReadLine()!.ToLower() == "y")
            {
                subject?.Exam?.ShowExam();
                Console.WriteLine("Thank you");
            }
        }
    }
    #endregion
}
