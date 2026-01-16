using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using User.Models;
using User.Services;
using Quiz.menu;

namespace Quiz.LSin
{
    public static class LogIn
    {
        public static bool Login()
        {
            string userFile = "userFile.txt";



            string name;
            string pass;
            DateTime birthday;
            do
            {
                Console.Write("Enter Name:");
                name = Console.ReadLine();
                Console.Write("Enter Pass:");
                pass = Console.ReadLine();
                Console.Write("Enter birthday(yyyy,mm,dd):");
                birthday = Convert.ToDateTime(Console.ReadLine());

                if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(pass) || String.IsNullOrEmpty(birthday.ToString()))
                {
                    Console.WriteLine("enter data correct, please");
                }
            } while (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(pass) || String.IsNullOrEmpty(birthday.ToString()));
            string TempStr;
            string UsersTempStr = name;
            using (FileStream fileStream = new FileStream(userFile, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fileStream, Encoding.Default))
                {
                    while (reader.Peek() > 0)
                    {
                        TempStr = reader.ReadLine();
                        string str = "";
                        for(int i = 0; i < TempStr.Length && TempStr[i] != ' '; i++)
                        {
                            str += TempStr[i];
                        }
                        if (str == UsersTempStr)
                        {
                            return false;
                        }
                    }
                }
            }
            using (FileStream fileStream = new FileStream(userFile, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter reader = new StreamWriter(fileStream, Encoding.Default))
                {
                    UsersTempStr += " "; UsersTempStr += pass; UsersTempStr += " "; UsersTempStr += birthday.ToShortDateString();UsersTempStr += "\n";
                    reader.Write(UsersTempStr);
                }
            }
            UseR NewUser = new UseR(name);
            StartProject.Myuser = NewUser;
           

            return true;
        }
    }
}
