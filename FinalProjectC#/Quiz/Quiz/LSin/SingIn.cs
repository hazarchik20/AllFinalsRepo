using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using User.Models;
using Quiz.menu;

namespace Quiz.LSin
{
    public static class SingIn
    {
        public static bool Singin()
        {
            string userFile = "userFile.txt";
            string name;
            string pass;
            do
            {
                Console.Write("Enter Name:");
                name = Console.ReadLine();
                Console.Write("Enter Pass:");
                pass = Console.ReadLine();

                if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(pass))
                {
                    Console.WriteLine("enter data correct, please");
                }
            } while (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(pass));
            string TempStr;
            string UsersTempStr = name; UsersTempStr += " "; UsersTempStr += pass;
            using (FileStream fileStream = new FileStream(userFile, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fileStream, Encoding.Default))
                {
                    while (reader.Peek() > 0)
                    {
                        TempStr = reader.ReadLine();
                        string temp="";
                        int count = 0;
                        for(int i=0;i < TempStr.Length;i++)
                        {
                            if (TempStr[i] == ' ')
                                count++;
                            if (count == 2)
                                break;
                            temp += TempStr[i];
                        }
                        TempStr = temp;
                        if (TempStr == UsersTempStr)
                        {
                            UseR NewUser = new UseR(name);
                            StartProject.Myuser = NewUser;
                            return true;
                        }
                    }
                }
            }
           

            return false;
        }
    }
}
