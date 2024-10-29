using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace genrick_valid
{
    public class MyTools
    {
        static Random random = new Random(); 

        private static string RandomWord(string GetString)
        {
            string[] esponce = GetString.Split(' ');
            
            return esponce[random.Next(0, esponce.Length)];
        }  //Метод для получения случайного словаиз переданной строки

        public static async Task<string> RandomWords(string linkAPI) 
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(linkAPI); //Получение ответа на запрос по API ссылке
            string content = await response.Content.ReadAsStringAsync();  //Получение JSON файла
            JObject jsonObject = JObject.Parse(content);
            string msg = jsonObject["value"].ToString();
            string word = RandomWord(msg);
            return word;
        } //Метод для получения сообщения от API сервиса

        public static string GetTab(int TabCounter)
        {
            string Tab = "";
            for (int i = 0; i != TabCounter; i++) { Tab += "    "; }
            return Tab;
        } //Метод для возварщения Tab отступов

        public static string RandomValues(string Type) 
        {
            string Values = "";
            switch (Type)
            {
                case "bool":
                    Values = random.NextDouble() > 0.5 ? "true" : "false" ;
                    break;
                case "string":
                    for (int i = 0; i < random.Next(0, 10); i++) { Values += RandomWords(GenerateCode.LinkAPI); }
                    break;
                case "int":

                    break;
                case "char":

                    break;
                case "float":

                    break;
                case "double":

                    break;
                case "object":

                    break;  
            }

            return Values;
        } //Метод возвращает рандомное значение по принимаемому типу

    }
}
