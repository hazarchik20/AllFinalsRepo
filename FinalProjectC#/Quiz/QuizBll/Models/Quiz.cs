using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBll.Models
{
    public class Quiz
    {
        public string Name;
        public string Categorogy;
        public List<Question> Questions;
        public Quiz(Quiz quiz)
        {
            Name = quiz.Name;
            Categorogy=quiz.Categorogy;
            Questions = quiz.Questions;
        }
        public Quiz()
        {
        }
    }
}
