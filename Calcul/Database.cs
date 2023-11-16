using Calcul;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calcul
{
    public class Database
    {
        private string filePath = "database.txt";  // Путь к текстовому файлу базы данных

        public Database(string filePath)
        {
            this.filePath = filePath;
        }

        public void AddRecord(float sideA, float sideB, float sideC, TriangleType triangleType, string errorMessage)
        {
            // Создаем строку для записи в формате CSV
            string record = $"{sideA},{sideB},{sideC},{triangleType},{errorMessage}";

            // Добавляем запись в конец файла
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(record);
            }
        }

        public List<string> GetRecords()
        {
            List<string> records = new List<string>();

            // Читаем все строки из файла
            using (StreamReader reader = File.OpenText(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string record = reader.ReadLine();
                    records.Add(record);
                }
            }

            return records;
        }

        public void DeleteRecord(float sideA, float sideB, float sideC)
        {
            // Загружаем все записи из файла
            List<string> records = GetRecords();

            // Создаем новый список записей, исключая запись с заданными сторонами
            List<string> updatedRecords = new List<string>();

            foreach (string record in records)
            {
                // Разделяем запись на отдельные поля
                string[] fields = record.Split(',');

                // Проверяем, совпадает ли запись с заданными сторонами
                float recordSideA = float.Parse(fields[0]);
                float recordSideB = float.Parse(fields[1]);
                float recordSideC = float.Parse(fields[2]);

                if (recordSideA != sideA || recordSideB != sideB || recordSideC != sideC)
                {
                    updatedRecords.Add(record);
                }
            }

            // Перезаписываем файл с обновленными записями
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string updatedRecord in updatedRecords)
                {
                    writer.WriteLine(updatedRecord);
                }
            }
        }
    }
}





