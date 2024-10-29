using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using genrick_valid;
using System.Web;

namespace genrick_valid
{
    class Program
    {
        //Метод запускающийся при запуске программы
        static void Main()
        {
            Console.WriteLine("Welcome.....\n    1. Generate\n    2. Validation\n    3. Clear\n\n" +
                "---------------------------------------------------------------------------------------------------------------\n");
            SendMessage(false, "");
        }

        //Метод для отправки сообщений и начала сообщения пользователя. Проверка на отправителя сообщения( генератор/человек)
        private static void SendMessage(bool Generick, string Message)
        {
            if (Generick)
            {
                Message = $"Generick > {Message}";
                Console.WriteLine(Message);
            }
            Console.Write("You > ");
            string query = Console.ReadLine();
            if (query == "Generate") //Проверки на команду
            {
                GenerateCode generateCode = new GenerateCode();
                SendMessage(true, generateCode.GetMessage().Result);
                return;
            }
            else if (query == "Validation") { SendMessage(true, "A funcion in the development peocess....."); }
            else if (query == "Clear") { Console.Clear(); Main(); }
            else { SendMessage(true, "The command was not recognized"); }
        }
    }

    //Класс генерирующий код
    public class GenerateCode
    {
        Random random = new Random();

        //private string[] kwOOP = { "class ", "void ", "struct ", "return "}; //Массив с ключевыми словами от синтаксиса ООП, где kw - keywords
        private string[] kwType = { "bool ", "int ", "string ", "double ", "float ", "char ", "object " }; //Массив с типами данных
        private string[] kwConditionalsOper = { "if", "else", "switch", "case" }; //Массив с условными операторами
        private string[] kwExeptValid = { "try ", "catch ", "finally " }; //Массив с обработчиками исключений
        private string[] kwAcessMod = { "private ", "protected ", "public ", "", "internal " }; //Массив с модификаторами доступа
        private string[] kwCycle = { "while ", "do ", "for " }; //Массив с циклами
        private List<string> kwUsing = new List<string>
        {
            "Newtonsoft.Json.Linq;", "System;", "System.Collections.Generic;", "System.Linq;", "System.Net.Configuration;", "System.Net.Http;",
            "System.Runtime.CompilerServices;", "System.Text;", "System.Threading;", "System.Threading;", "System.Data;", "System.Data.SqlClient;",
            "System.Security.Cryptography;", "System.Diagnostics;", "System.DirectoryServices;", "System.ComponentModel;"
        }; // Список с воможными для добавления библиотеками

        private int CountClass = 1; // Переменная для подсчета количества классов

        private string ClassName;  //Переменная с названием созданного класса

        private static string linkAPI = "https://api.chucknorris.io/jokes/random?category=dev";  //Ссылка на API запрос
        public static string LinkAPI
        {
            get { return linkAPI; } set { }
        }


        private int TabCounter = 0;  //Переменная для счета  отступов, соответственно зависимостей

        private string Message = "\n"; //Строка с кодом которая при создании экземпляра класса будет обрабатываться методами класса, и возвращающаяся при вызове класса

        public GenerateCode() => GenerateEvent();  //Коструктор вызывает метод Generate

        private string Tab() => MyTools.GetTab(TabCounter); //Метод ссылающийся на метод другого класса (сделан для упрощения читаемости кода)

        private void GenerateEvent()
        {
            int ChanceToCreateSecondClass = random.Next(2, 12);
            CreateUsing(ChanceToCreateSecondClass);
            CreateNamespace(false);
            CreateClass(random.Next(0, 3), false);

            CreateVarailable();

            CreateVoid(false);
            CreateVoid(true);


            CreateClass(ChanceToCreateSecondClass, true);
            CreateNamespace(true);

        }  // Метод для генерации количества и содержания элементов кода

        private void CreateUsing(int NumberConnection)
        {
            for (int i = 0; i != NumberConnection; i++)
            {
                int UsingName = random.Next(0, kwUsing.Count);
                Message += "using " + kwUsing[UsingName] + "\n";
                kwUsing[UsingName].Remove(UsingName);
            }
            Message += "\n\n";
        }  // Метод для добавления в код библиотек, в аргументы принимает количество подключений

        private void CreateNamespace(bool EnableNamespace)
        {
            if (!EnableNamespace)
            {
                Message += "namespace " + MyTools.RandomWords(linkAPI).Result + "\n{\n";
                TabCounter++;
            }
            else
            {
                TabCounter--;
                Message += "}\n";
            }

        }   // Метод для создания пространстра имен

        private void CreateClass(int ChanceSecondClass, bool EnableClass)
        {
            if (!EnableClass)
            {
                if (ChanceSecondClass > 2) { CountClass = 2; }
                ClassName = MyTools.RandomWords(linkAPI).Result;
                Message += Tab() + kwAcessMod[random.Next(2, kwAcessMod.Length)] + "class " +
                     ClassName + "\n" + Tab() + "{\n";
                TabCounter++;
            }
            else
            {
                TabCounter--;
                Message += Tab() + "}\n";
            }
            if (ChanceSecondClass > 2) { CountClass = 2; }
            //Message += MyTools.GetTab(TabCounter) + kwAcessMod[random.Next(2, kwAcessMod.Length)] + "class " + MyTools.RandomWords(LinkAPI).Result; 
        }  //Метод для генерации классов, принимает шанс на создание второго класса

        private void CreateVarailable() 
        {
            string Type = kwType[random.Next(0, kwType.Length)];
            Message += Tab() + kwAcessMod[random.Next(0, kwAcessMod.Length)] + Type +
                MyTools.RandomWords(linkAPI).Result;
            Message += random.NextDouble() > 0.5 ? ";" : " = " + MyTools.RandomValues(Type) + ";";
            Message += "\n";
        } //Метод для генерации переменных 

        private void CreateVoid(bool EnableVoid)
        {
            if (!EnableVoid)
            {
                Message += Tab() + kwAcessMod[random.Next(0, 3)] + "void " +
                    MyTools.RandomWords(linkAPI).Result + $"()\n{Tab()}{{\n";
                TabCounter++;
            }
            else
            {
                TabCounter--;
                Message += Tab() + "}\n";
            }
        } // Метод для генерации методов

        private void CreateCycle_and_ConditionOperatos(bool Enable)
        {
            if (!Enable)
            {

            }
            else
            {
                TabCounter--;
                Message += Tab() + "}\n";
            }
        } // Метод для генерации циклов и условных операторов

        public async Task<string> GetMessage()
        {
            return Message.ToString();
        }   //Метод для получения готового сообщения


    }
}
