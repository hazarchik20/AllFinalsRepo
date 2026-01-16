using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using User.Models;

namespace User.Services
{
    public class Services
    {
        static public void SetUserachievement(string path, UseR user)
        {
            string TempStr;
            string UsersTempStr = user.Name;
           


            int count = 0;
            for (int i = 0; i < user.PointArr.Length; i++)
            {
                if (user.PointArr[i] <= 0)
                {
                    count++;
                }
            }
            bool isInList = false;
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fileStream, Encoding.Default))
                {
                    while (reader.Peek() > 0)
                    {
                        TempStr = reader.ReadLine();
                        string str = "";
                        for (int i = 0; i<TempStr.Length && TempStr[i] != ' '; i++)
                        {
                            str += TempStr[i];
                        }
                        if (str == UsersTempStr)
                        {
                            isInList = true;
                            break;
                        }
                    }
                }
            }
            if (!isInList)
            {
                AppendUserachievement(path, user);
            }
            else
            {

            }
        }
        static public void AppendUserachievement(string path, UseR user)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter reader = new StreamWriter(fileStream, Encoding.Default))
                {
                    string points = "";
                    for(int i=0; i<user.PointArr.Length; i++)
                    {
                        points += user.PointArr[0];
                        points += " ";
                    }
                    Console.WriteLine(points);
                    string UsersTempStr = user.Name; UsersTempStr += " "; UsersTempStr += points; UsersTempStr += "\n";
                    reader.Write(UsersTempStr);
                }
            }
        }
        static public void ReWritedUserachievement(string path, UseR user)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter reader = new StreamWriter(fileStream, Encoding.Default))
                {
                    string allText;

                    
                }
            }
        }

    }
}
