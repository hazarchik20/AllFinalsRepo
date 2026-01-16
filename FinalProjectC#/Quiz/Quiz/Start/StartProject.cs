using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Quiz.LSin;
using QuizBll.Models;
using User.Models;
using User.Services;

namespace Quiz.menu
{
    public class StartProject
    {
        static public string userAchievement = "userAchievement.txt";
        static public UseR Myuser;
        static public List<string> QuizmenuChoice = new List<string>()
                {
                    "Car Quiz","FootBall Quiz", "Computer Quiz","English Quiz","Random Quiz","Your Quizs"
                };
        public static void StartProgramMenu()
        {
            Console.CursorVisible = false;
            ConsoleKeyInfo consoleKey;
            int currentSelection = 0;
            List<string> menuChoice = new List<string>()
            {
                "Log in", "SingIn in"
            };
            do
            {   Console.Clear();
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                for (int i = 0; i < menuChoice.Count; i++)
                {
                    if (i == currentSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"->{menuChoice[i]}<-");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.WriteLine(menuChoice[i]);
                    }
                }
                Console.SetCursorPosition(101, 28);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Esc - exit program");
                
                consoleKey = Console.ReadKey();
                if ((consoleKey.Key == ConsoleKey.DownArrow || consoleKey.Key == ConsoleKey.S) && currentSelection < menuChoice.Count - 1)
                {
                    currentSelection++;
                }
                else if ((consoleKey.Key == ConsoleKey.UpArrow || consoleKey.Key == ConsoleKey.W) && currentSelection > 0)
                {
                    currentSelection--;
                }
                else if (consoleKey.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    switch (currentSelection)
                    {
                        case 0:
                            {
                                Console.Clear();
                                Console.CursorVisible = true;
                                if (!LogIn.Login())
                                {
                                    MessageBox.Show("This name is taken :(\nTry again", " ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                   
                                    StartProgramMenu();
                                }
                                else
                                {
                                    Console.CursorVisible = false;
                                    User.Services.Services.SetUserachievement(userAchievement, Myuser);
                                    MeinMenu();
                                    return;
                                }
                            }
                            break;
                        case 1:
                            {
                                Console.Clear();
                                Console.CursorVisible = true;
                                if (!SingIn.Singin())
                                {
                                    MessageBox.Show("This name doesn`t exist :(\nTry again", " ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    
                                    StartProgramMenu();
                                }
                                else
                                {
                                    Console.CursorVisible = false;
                                    User.Services.Services.SetUserachievement(userAchievement, Myuser);
                                    MeinMenu();
                                    return;
                                }
                            }
                            break;
                    }
                }
                
            }
            while (consoleKey.Key != ConsoleKey.Escape);
        }
        public static void MeinMenu()
        {

            ConsoleKeyInfo consoleKey;
            int currentSelection = 0;
            List<string> menuChoice = new List<string>()
            {
                "Start new Quiz", "Show past quiz results","Show top 5 from a specific quiz","Change info about you","My new Quiz"
            };
            do
            {
                Console.Clear();
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                for (int i = 0; i < menuChoice.Count; i++)
                {
                    if (i == currentSelection)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"->{menuChoice[i]}<-");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.WriteLine(menuChoice[i]);
                    }
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Esc - exit program");
                consoleKey = Console.ReadKey();
                if ((consoleKey.Key == ConsoleKey.DownArrow || consoleKey.Key == ConsoleKey.S) && currentSelection < menuChoice.Count - 1)
                {
                    currentSelection++;
                }
                else if ((consoleKey.Key == ConsoleKey.UpArrow || consoleKey.Key == ConsoleKey.W) && currentSelection > 0)
                {
                    currentSelection--;
                }
                else if (consoleKey.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    switch (currentSelection)
                    {
                        case 0:
                            {
                                ConsoleKeyInfo QuizconsoleKey;
                                int QuizcurrentSelection = 0;
                                
                                do
                                {
                                    Console.Clear();
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    for (int i = 0; i < QuizmenuChoice.Count; i++)
                                    {
                                        if (i == QuizcurrentSelection)
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.WriteLine($"->{QuizmenuChoice[i]}<-");
                                            Console.ResetColor();
                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                        }
                                        else
                                        {
                                            Console.WriteLine(QuizmenuChoice[i]);
                                        }
                                    }
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Esc - exit program");
                                    QuizconsoleKey = Console.ReadKey();
                                    if ((QuizconsoleKey.Key == ConsoleKey.DownArrow || QuizconsoleKey.Key == ConsoleKey.S) && QuizcurrentSelection < QuizmenuChoice.Count - 1)
                                    {
                                        QuizcurrentSelection++;
                                    }
                                    else if ((QuizconsoleKey.Key == ConsoleKey.UpArrow || QuizconsoleKey.Key == ConsoleKey.W) && QuizcurrentSelection > 0)
                                    {
                                        QuizcurrentSelection--;
                                    }
                                    else if (QuizconsoleKey.Key == ConsoleKey.Enter)
                                    {
                                        AllQuiz allQuiz = new AllQuiz();
                                        switch (QuizcurrentSelection)
                                        {
                                            case 0:
                                                {
                                                    Myuser.PointArr[0] = StartQuizMenu(allQuiz.AllQuizzes[0]);
                                                    User.Services.Services.SetUserachievement(userAchievement, Myuser);
                                                }
                                                break;
                                            case 1:
                                                {
                                                    Myuser.PointArr[1] = StartQuizMenu(allQuiz.AllQuizzes[1]);
                                                    User.Services.Services.SetUserachievement(userAchievement, Myuser);
                                                }
                                                break;
                                            case 2:
                                                {
                                                    Myuser.PointArr[2] = StartQuizMenu(allQuiz.AllQuizzes[2]);
                                                    User.Services.Services.SetUserachievement(userAchievement, Myuser);
                                                }
                                                break;
                                            case 3:
                                                {
                                                    Myuser.PointArr[3] = StartQuizMenu(allQuiz.AllQuizzes[3]);
                                                    User.Services.Services.SetUserachievement(userAchievement, Myuser);

                                                }
                                                break;
                                            case 4:
                                                {
                                                    Myuser.PointArr[4] = StartQuizMenu(allQuiz.AllQuizzes[4]);
                                                    User.Services.Services.SetUserachievement(userAchievement, Myuser);
                                                }
                                                break;
                                            case 5:
                                                {
                                                    Myuser.PointArr[4] = StartQuizMenu(allQuiz.AllQuizzes[4]);
                                                    User.Services.Services.SetUserachievement(userAchievement, Myuser);
                                                }
                                                break;
                                        }
                                    }
                                } while (QuizconsoleKey.Key != ConsoleKey.Escape);
                            }
                            break;
                        case 1:
                            {

                            }
                            break;
                        case 2:
                            {

                            }
                            break;
                        case 3:
                            {

                            }
                            break;
                        case 4:
                            {
                                ConsoleKeyInfo NewQuizconsoleKey;
                                int userQuizCurrentSelection = 0;
                                List<string> userQuizChoise = new List<string>()
                                    {
                                        "Create Quiz", "start my Quiz"
                                    };
                                do
                                {
                                    Console.Clear();
                                    Console.ResetColor();
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    for (int i = 0; i < userQuizChoise.Count; i++)
                                    {
                                        if (i == userQuizCurrentSelection)
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.WriteLine($"->{userQuizChoise[i]}<-");
                                            Console.ResetColor();
                                            Console.ForegroundColor = ConsoleColor.DarkGray;
                                        }
                                        else
                                        {
                                            Console.WriteLine(userQuizChoise[i]);
                                        }
                                    }
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Esc - exit program");
                                    NewQuizconsoleKey = Console.ReadKey();
                                    if ((NewQuizconsoleKey.Key == ConsoleKey.DownArrow || NewQuizconsoleKey.Key == ConsoleKey.S) && userQuizCurrentSelection < QuizmenuChoice.Count - 1)
                                    {
                                        userQuizCurrentSelection++;
                                    }
                                    else if ((NewQuizconsoleKey.Key == ConsoleKey.UpArrow || NewQuizconsoleKey.Key == ConsoleKey.W) && userQuizCurrentSelection > 0)
                                    {
                                        userQuizCurrentSelection--;
                                    }
                                    else if (NewQuizconsoleKey.Key == ConsoleKey.Enter)
                                    {
                                        switch (userQuizCurrentSelection)
                                        {
                                            case 0:
                                                {
                                                    Console.Clear();
                                                    Console.CursorVisible=true;
                                                    CreateQuiz(Myuser);
                                                    Console.CursorVisible = false;
                                                }
                                                break;
                                            case 1:
                                                {
                                                    if (Myuser.UsersQuizs==null)
                                                    {
                                                        Console.WriteLine("You haven`t quiz");
                                                        Thread.Sleep(1500);
                                                        break;
                                                    }
                                                    ConsoleKeyInfo CreateQuizconsoleKey;
                                                    int CreateQuizcurrentSelection = 0;
                                                    List<string> UserQuizmenuChoice = new List<string>();
                                                    
                                                    for(int j=0; j<Myuser.UsersQuizs.Count; j++)
                                                    {
                                                        UserQuizmenuChoice.Add(Myuser.UsersQuizs[j].Name);
                                                    }
                                                    do
                                                    {
                                                        Console.Clear();
                                                        Console.ResetColor();
                                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                                        for (int i = 0; i < UserQuizmenuChoice.Count; i++)
                                                        {
                                                            if (i == CreateQuizcurrentSelection)
                                                            {
                                                                Console.ForegroundColor = ConsoleColor.White;
                                                                Console.WriteLine($"->{UserQuizmenuChoice[i]}<-");
                                                                Console.ResetColor();
                                                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine(UserQuizmenuChoice[i]);
                                                            }
                                                        }
                                                        Console.ForegroundColor = ConsoleColor.White;
                                                        Console.WriteLine("Esc - exit program");
                                                        CreateQuizconsoleKey = Console.ReadKey();
                                                        if ((CreateQuizconsoleKey.Key == ConsoleKey.DownArrow || CreateQuizconsoleKey.Key == ConsoleKey.S) && CreateQuizcurrentSelection < QuizmenuChoice.Count - 1)
                                                        {
                                                            CreateQuizcurrentSelection++;
                                                        }
                                                        else if ((CreateQuizconsoleKey.Key == ConsoleKey.UpArrow || CreateQuizconsoleKey.Key == ConsoleKey.W) && CreateQuizcurrentSelection > 0)
                                                        {
                                                            CreateQuizcurrentSelection--;
                                                        }
                                                        else if (CreateQuizconsoleKey.Key == ConsoleKey.Enter)
                                                        {
                                                            int tepmPoint = 0;
                                                            switch (CreateQuizcurrentSelection)
                                                            {
                                                                case 0:
                                                                    {
                                                                       
                                                                        tepmPoint = StartQuizMenu(Myuser.UsersQuizs[0]);
                                                                        
                                                                    }
                                                                    break;
                                                                case 1:
                                                                    {
                                                                        tepmPoint = StartQuizMenu(Myuser.UsersQuizs[1]);
                                                                        
                                                                    }
                                                                    break;
                                                                case 2:
                                                                    {
                                                                        tepmPoint = StartQuizMenu(Myuser.UsersQuizs[2]);
                                                                        
                                                                    }
                                                                    break;
                                                                case 3:
                                                                    {
                                                                        tepmPoint = StartQuizMenu(Myuser.UsersQuizs[3]);
                                                                        

                                                                    }
                                                                    break;
                                                                case 4:
                                                                    {
                                                                        tepmPoint = StartQuizMenu(Myuser.UsersQuizs[4]);
                                                                        
                                                                    }
                                                                    break;
                                                                case 5:
                                                                    {
                                                                        tepmPoint = StartQuizMenu(Myuser.UsersQuizs[5]);
                                                                       
                                                                    }
                                                                    break;
                                                            }
                                                            Console.WriteLine("YourPoint"+tepmPoint);
                                                            Thread.Sleep(1500);
                                                        }
                                                    } while (CreateQuizconsoleKey.Key != ConsoleKey.Escape);
                                                }
                                                break;
                                        }
                                    }
                                } while (NewQuizconsoleKey.Key != ConsoleKey.Enter);
                            }
                            break;
                    }
                }
            }
            while (consoleKey.Key != ConsoleKey.Escape);
        }

        private static int StartQuizMenu(QuizBll.Models.Quiz Quiz)
        {
            float userPoints = 0;
            

            Console.WriteLine("Quiz is Srating!!");
            
            for(int i = 0;i< Quiz.Questions.Count; i++)
            {
                Console.CursorVisible = false;
                ConsoleKeyInfo consoleKey;
                int currentSelection = 0;

                bool isCorrectAnsw1 = Quiz.Questions[i].Answers[0].Iscurrent;
                bool isCorrectAnsw2 = Quiz.Questions[i].Answers[1].Iscurrent;
                bool isCorrectAnsw3 = Quiz.Questions[i].Answers[2].Iscurrent;
                bool isCorrectAnsw4 = Quiz.Questions[i].Answers[3].Iscurrent;

                DateTime timer = DateTime.Now;
                int StartMinut = timer.Minute;
                int StartSecond = timer.Second;
                int StartMilliSecond = timer.Millisecond;

                do
                {
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine(Quiz.Questions[i].question);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    for (int y = 0; y < Quiz.Questions[i].Answers.Count; y++)
                    {
                        if (y == currentSelection)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"->{Quiz.Questions[i].Answers[y].Name}<-");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        }
                        else
                        {
                            Console.WriteLine(Quiz.Questions[i].Answers[y].Name);
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Esc - exit program");

                    consoleKey = Console.ReadKey();
                    if ((consoleKey.Key == ConsoleKey.DownArrow || consoleKey.Key == ConsoleKey.S) && currentSelection < Quiz.Questions[i].Answers.Count - 1)
                    {
                        currentSelection++;
                    }
                    else if ((consoleKey.Key == ConsoleKey.UpArrow || consoleKey.Key == ConsoleKey.W) && currentSelection > 0)
                    {
                        currentSelection--;
                    }
                    else if (consoleKey.Key == ConsoleKey.Enter)
                    {
                        timer = DateTime.Now;
                        int EndMinut = timer.Minute;
                        int EndSecond = timer.Second;
                        int EndMilliSecond = timer.Millisecond;
                        int StartAllTime;
                        int EndAllTime;
                        Console.Clear();
                        switch (currentSelection)
                        {
                            case 0:
                                {
                                    if (isCorrectAnsw1)
                                    {
                                        StartAllTime = ((StartMinut * 60 * 1000) + (StartSecond * 1000) + (StartMilliSecond));
                                        EndAllTime = ((EndMinut * 60 * 1000) + (EndSecond * 1000) + (EndMilliSecond));
                                        userPoints += 50 - ((EndAllTime - StartAllTime) / 1000);
                                    }
                                }
                                break;
                            case 1:
                                {
                                    if (isCorrectAnsw2)
                                    {
                                        StartAllTime = ((StartMinut * 60 * 1000) + (StartSecond * 1000) + (StartMilliSecond));
                                        EndAllTime = ((EndMinut * 60 * 1000) + (EndSecond * 1000) + (EndMilliSecond));
                                        userPoints += 50 - ((EndAllTime - StartAllTime) / 1000);
                                    }
                                }
                                break;
                            case 2:
                                {
                                    if (isCorrectAnsw3)
                                    {
                                        StartAllTime = ((StartMinut * 60 * 1000) + (StartSecond * 1000) + (StartMilliSecond));
                                        EndAllTime = ((EndMinut * 60 * 1000) + (EndSecond * 1000) + (EndMilliSecond));
                                        userPoints += 50 - ((EndAllTime - StartAllTime) / 1000);
                                    }
                                }
                                break;
                            case 3:
                                {
                                    if (isCorrectAnsw4)
                                    {
                                        StartAllTime = ((StartMinut * 60 * 1000) + (StartSecond * 1000) + (StartMilliSecond));
                                        EndAllTime = ((EndMinut * 60 * 1000) + (EndSecond * 1000) + (EndMilliSecond));
                                        userPoints += 50 - ((EndAllTime - StartAllTime) / 1000);
                                    }
                                }
                                break;
                        }
                    }
                }
                while (consoleKey.Key != ConsoleKey.Enter);
            }
            return (int)userPoints;
        }
        static public void CreateQuiz(UseR user)
        {
            QuizBll.Models.Quiz TempQuiz = new QuizBll.Models.Quiz();
            Console.Write("Введiть назву вашої нової вiкторини: ");
            string quizName = Console.ReadLine();
            TempQuiz.Name = quizName;
            TempQuiz.Categorogy = quizName + " Quiz";
            List<Question> questions = new List<Question>();

            for (int i = 0; i < 12; i++)
            {
                Question Tempquestions = new Question();
                Console.WriteLine($"\nПитання {i + 1}:");

                Console.Write("Текст питання: ");
                string questionText = Console.ReadLine();
                Tempquestions.question = questionText;
                List<Answer> answers = new List<Answer>();

                Console.WriteLine("Введiть 4 варiанти вiдповiдей, та вкажiть чи ця вiдповiдь правильна");
                int answerIndex = 0;

                for(int j = 0; j < 4; j++)
                {
                    Answer TempAnswer = new Answer();
                    Console.Write($"Вiдповiдь {answerIndex + 1}: ");
                    string answer = Console.ReadLine();
                    TempAnswer.Name = answer;
                    Console.Write($"чм правильна відповідь(0-нi,1-так) {answerIndex + 1}: ");
                    string boolstr = Convert.ToString(Console.ReadLine());
                    bool tempIsCorrect;
                    if (boolstr == "1")
                        tempIsCorrect = true;
                    else
                        tempIsCorrect = false;

                    TempAnswer.Iscurrent = tempIsCorrect;
                    answerIndex++;
                    answers.Add(TempAnswer);
                }
                Tempquestions.Answers= answers;
                questions.Add(Tempquestions);
            }
            TempQuiz.Questions= questions;
            user.UsersQuizs.Add(TempQuiz);
            Console.WriteLine($"Вiкторина '{quizName}' успiшно додана!");
        }
    }

}
