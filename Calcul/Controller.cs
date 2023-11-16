using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Calcul
{
    public class Controller
    {
        private IMailSender mailSender;

        public Controller(IMailSender mailSender)
        {
            this.mailSender = mailSender;
        }

        public string ProcessData(string inputData)
        {
            // Путь к текстовому файлу для хранения записей
            string filePath = "database1.rtf";

            // Проверка наличия записи в файле
            string result = ReadDataFromFile(inputData, filePath);
            if (result == null)
            {
                // Если записи нет, то вычисляем результат
                result = CalculateResult(inputData);

                // Добавляем запись в файл
                AddDataToFile(inputData, result, filePath);
            }

            // Отправка строки-результата
            SendResultByEmail(result);

            return result;
        }

        private string ReadDataFromFile(string inputData, string filePath)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length == 2 && parts[0] == inputData)
                    {
                        return parts[1];
                    }
                }
            }

            return null;
        }

        private void AddDataToFile(string inputData, string result, string filePath)
        {
            string newLine = $"{inputData};{result}";
            File.AppendAllText(filePath, newLine + Environment.NewLine);
        }

        private string CalculateResult(string inputData)
        {
            // Здесь происходит вычисление результата на основе входных данных
            // В данном примере просто возвращаем обратную строку
            char[] chars = inputData.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        private void SendResultByEmail(string result)
        {
            string recipient = "example@example.com";
            string subject = "Результат вычисления";
            string body = $"Результат: {result}";

            mailSender.SendEmail(recipient, subject, body);
        }
    }
}
