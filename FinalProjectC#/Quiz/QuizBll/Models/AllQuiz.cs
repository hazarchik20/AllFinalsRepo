using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBll.Models
{
    public class AllQuiz
    {
        public List<string> AllquizName = new List<string>()
        {
            "Car Quiz",
            "FootBall Quiz", 
            "Computer Quiz",
            "English Quiz",
            "Random Quiz"
        };
        static public List<Question> CarQuestion = new List<Question>()
        {
        new Question {question="Що означає абривiатура ABS у автомобiлi?",Answers= new List<Answer>{ new Answer {Name="Автоматичне балансування стабiлiзацiї",Iscurrent=false },new Answer {Name="Автоматична система безпеки",Iscurrent=false },new Answer {Name="Антиблокувальна гальмiвна система",Iscurrent= true },new Answer {Name="Автоматична гальмiвна система",Iscurrent= false } } },//3
        new Question {question="Перший автомобiль в свiтi?",Answers= new List<Answer>{ new Answer {Name="Benz Patent",Iscurrent=true },new Answer {Name="Daimler motorkutsche",Iscurrent=false },new Answer {Name="BMW Alpha",Iscurrent= false },new Answer {Name="Невiдомо",Iscurrent= false } } },//1
        new Question {question="Яка компанiя випускає модель Mustang?",Answers= new List<Answer>{ new Answer {Name="Chevrolet",Iscurrent=false },new Answer {Name="Ford",Iscurrent=true },new Answer {Name="Dodge",Iscurrent= false },new Answer {Name="Tesla",Iscurrent= false } } },//2
        new Question {question="Чим варто заправляти Daewoo Matiz?",Answers= new List<Answer>{ new Answer {Name="Газ",Iscurrent=false },new Answer {Name="Бензин",Iscurrent=false },new Answer {Name="Залежить вiд моделi",Iscurrent= false },new Answer {Name="Казан Плову",Iscurrent= true } } },//4
        new Question {question="Яка найпопулярнiша марка машини?",Answers= new List<Answer>{ new Answer {Name="Toyota",Iscurrent=true },new Answer {Name="Volkswagen",Iscurrent=false },new Answer {Name="Byd",Iscurrent= false },new Answer {Name="Honda",Iscurrent= false } } },//1
        new Question {question="Яка компанiя виробляє двигуни V12 для своїх суперкарiв?",Answers= new List<Answer>{ new Answer {Name="Ferrari",Iscurrent=false },new Answer {Name= "Lamborgini", Iscurrent=true },new Answer {Name="Koenigsegg",Iscurrent= false },new Answer {Name="Aston Martin",Iscurrent= false } } },//2
        new Question {question="Який тип кузова має позначення SUV?",Answers= new List<Answer>{ new Answer {Name="Спортивне купе",Iscurrent=false },new Answer {Name="Седан",Iscurrent=false },new Answer {Name="Позашляховик",Iscurrent= true },new Answer {Name="Мiнiвен",Iscurrent= false } } },//3
        new Question {question="Якої марки найдорожча машина свiту?",Answers= new List<Answer>{ new Answer {Name="Rolls-Royce",Iscurrent=true },new Answer {Name="Bugatti",Iscurrent=false },new Answer {Name="Pagani",Iscurrent= false },new Answer {Name="Lamborgini",Iscurrent= false } } },//1
        new Question {question="Яка країна є батькiвщиною компанiї Volvo?",Answers= new List<Answer>{ new Answer {Name="Нiмеччина",Iscurrent=false },new Answer {Name="Данiя",Iscurrent=false },new Answer {Name="Швецiя",Iscurrent= true },new Answer {Name="Швейцарiя",Iscurrent= false } } },//3
        new Question {question="Найшвидший автомобiль в свiтi (2024 р.)?",Answers= new List<Answer>{ new Answer {Name="Koenegsegg Jesko",Iscurrent=false },new Answer {Name="Hennessey Venom",Iscurrent=false },new Answer {Name="Bugatti Chiron",Iscurrent= false },new Answer {Name="SSC Tuara",Iscurrent= true } } },//4
        new Question {question="Яка марка в Маквiна з мульфiльма 'Тачки'?",Answers= new List<Answer>{ new Answer {Name="Chevrolet",Iscurrent=false },new Answer {Name="Кокретної марки немає",Iscurrent=true },new Answer {Name= "Dodge", Iscurrent= false },new Answer {Name="Ford",Iscurrent= false } } },//2
        new Question {question="Яка машина найкраща?",Answers= new List<Answer>{ new Answer {Name="Daewoo Matiz",Iscurrent=true },new Answer {Name="BMW M5",Iscurrent=false },new Answer {Name="Mercedes cls",Iscurrent= false },new Answer {Name="Dodge Challenger",Iscurrent= false } } },//1
        
        };
        static public Quiz CarQuiz = new Quiz()
        {
            Categorogy = "Car",
            Name = "Car Quiz",
            Questions = CarQuestion
        };
        static public List<Question> FootBallQuestion = new List<Question>()
        {
        new Question {question="У якого футболiста найбiльше ЗМ?",Answers= new List<Answer>{ new Answer {Name="C. Ronaldo",Iscurrent=false },new Answer {Name="L. Messi",Iscurrent=true },new Answer {Name="Neymar Jr",Iscurrent= false },new Answer {Name="А. Шевченко",Iscurrent= false } } },//2
        new Question {question="Яка країна перемогла Євро2024?",Answers= new List<Answer>{ new Answer {Name="Україна",Iscurrent=false },new Answer {Name="Нiмеччина",Iscurrent=false },new Answer {Name="Англiя",Iscurrent= false },new Answer {Name="Iспанiя",Iscurrent= true } } },//4
        new Question {question="Який найдорожчий фут. клуб?",Answers= new List<Answer>{ new Answer {Name="Реал Мадрид",Iscurrent=true },new Answer {Name="Матчестер Сiтi",Iscurrent=false },new Answer {Name= "Матчестер Юнайтед", Iscurrent= false },new Answer {Name="Барселона",Iscurrent= false } } },//1
        new Question {question="Яка країна виграла перший Чемпiонат свiту з футболу в 1930 р?",Answers= new List<Answer>{ new Answer {Name= "Бразилiя", Iscurrent=false },new Answer {Name= "Уругвай", Iscurrent=true },new Answer {Name= "Аргентина", Iscurrent= false },new Answer {Name= "Францiя", Iscurrent= false } } },//2
        new Question {question="Кого називають \"Королем футбольного поля\" у Бразилiї?",Answers= new List<Answer>{ new Answer {Name= "Роналду", Iscurrent=false },new Answer {Name= "Кака", Iscurrent=false },new Answer {Name= "Пеле", Iscurrent= true },new Answer {Name= "Неймар", Iscurrent= false } } },//3
        new Question {question="У якому роцi А Шевченко отримав ЗМ?",Answers= new List<Answer>{ new Answer {Name="2003",Iscurrent=false },new Answer {Name= "2005", Iscurrent=false },new Answer {Name="2004",Iscurrent= true },new Answer {Name="2006",Iscurrent= false } } },//3
        new Question {question="Який футболiст став найкращим бомбардиром Чемпiонату свiту з футболу за всю iсторiю??",Answers= new List<Answer>{ new Answer {Name= "Роналду", Iscurrent=true },new Answer {Name= "Мiрошниченко", Iscurrent=false },new Answer {Name= "Мартiнес", Iscurrent= false },new Answer {Name= "Мартiн Шмiдт", Iscurrent= false } } },//1
        new Question {question="Що таке офсайд у футболi?",Answers= new List<Answer>{ new Answer {Name= "Коли гравець перебуває за межами поля.", Iscurrent=false },new Answer {Name= "Коли гравець отримує м'яч пiсля передачi вiд суперника, не порушуючи правил.", Iscurrent=false },new Answer {Name= "Коли пiд час передачi гравець знаходиться ближче до ворiт суперника нiж гравцi суперника", Iscurrent= true },new Answer {Name= "Коли м'яч виходить за межi поля i гри припиняється.", Iscurrent= false } } },//3
        new Question {question="Пiсля скiлькох попереджень дають жовту карточку?",Answers= new List<Answer>{ new Answer {Name="2",Iscurrent=true },new Answer {Name="1",Iscurrent=false },new Answer {Name="3",Iscurrent= false },new Answer {Name="4",Iscurrent= false } } },//1
        new Question {question="Яке твердження правильне?",Answers= new List<Answer>{ new Answer {Name= "Якщо м'яч потрапляє в руку гравця пiд час гри, це завжди дає пенальтi.", Iscurrent=false },new Answer {Name= "М'яч не вважається в межах поля, якщо частина його торкається лiнiї.", Iscurrent=false },new Answer {Name="Команда може розводити пропущений мя'ч якщо нiкого з суперникiв немає на полi ",Iscurrent= true },new Answer {Name= "Гравець може перебувати у офсайдi, якщо вiн не бере участь в атакцi", Iscurrent= true } } },//4
        new Question {question="Чи можe футболiст забрати мя'ч собi пiсля гри",Answers= new List<Answer>{ new Answer {Name="Так",Iscurrent=true },new Answer {Name="Нi",Iscurrent=false },new Answer {Name= "Тiльки якщо перемiг", Iscurrent= false },new Answer {Name="Тiльки якшщо заплатив",Iscurrent= false } } },//2
        new Question {question="Хто iз них Уругваєць?",Answers= new List<Answer>{ new Answer {Name="D. Silva",Iscurrent=false },new Answer {Name="L. Suares",Iscurrent=true },new Answer {Name="L. Messi",Iscurrent= false },new Answer {Name="Neymar Jr",Iscurrent= false } } },//1
        };
        static public Quiz FootBallQuiz = new Quiz()
        {
            Categorogy = "FootBall",
            Name = "FootBall Quiz",
            Questions = FootBallQuestion
        };
        static public List<Question> ComputerQuestion = new List<Question>()
        {
        };
        static public Quiz ComputerQuiz = new Quiz()
        {
            Categorogy = "Coomputer",
            Name = "Coomputer Quiz",
            Questions = ComputerQuestion
        };
        static public List<Question> EnglishQuestion = new List<Question>()
        {
        };
        static public Quiz EnglishQuiz = new Quiz()
        {
            Categorogy = "English ",
            Name = "English Quiz",
            Questions = EnglishQuestion
        };

        static public Quiz RandomQuiz = new Quiz()
        {
            Categorogy = " Random",
            Name = "Random Quiz",
            
        };
         public List<Quiz> AllQuizzes = new List<Quiz>()
        {
            new Quiz(CarQuiz),
            new Quiz(FootBallQuiz),
            new Quiz(ComputerQuiz),
            new Quiz(EnglishQuiz),
            new Quiz(RandomQuiz),
        };
        
    }
}
