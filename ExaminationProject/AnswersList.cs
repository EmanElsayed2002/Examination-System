using System;
using System.Collections.Generic;
using System.IO;

namespace ExaminationProject
{
    public class AnswersList : List<Answer>
    {
        public AnswersList() { }

        public void Add(Answer v , bool w)
        {
            base.Add(v);
            if(w)
            WriteToFile(v);
        }

        public void WriteToFile(Answer answer)
        {
            string filePath = "Answers.txt";
            try
            {
                using (var writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine(answer.AnswerText);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error writing answer to file: {ex.Message}");
            }
        }

        public void ReadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Answers file not found.");
                return;
            }

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Add(new Answer(line));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading answers from file: {ex.Message}");
            }
        }
    }
}
