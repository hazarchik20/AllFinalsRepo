using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBll.Models;

namespace User.Models
{
    public class UseR
    {
        public string Name;
        public string Password;
        public DateTime BirthDay;
        public int[] PointArr=new int[5];
        public int AllPoint;
        public List<Quiz> UsersQuizs;
        public UseR(string name)
        {
            this.Name = name;
        }
        public UseR(string name, string pass, DateTime birthDay, int[] pointArr, int allPoint)
        {
            this.Name = name;
            this.Password = pass;
            this.BirthDay = birthDay;
            this.PointArr = pointArr;
            this.AllPoint = allPoint;

        }
    }
}
