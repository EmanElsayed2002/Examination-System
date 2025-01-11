using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationProject
{
    public class QuestionsList : List<Question>
    {
        public void Add(Question q)
        {
            base.Add(q);
            WriteToFile(q);
        }

        public void WriteToFile(Question question)
        {
            string filePath = "questions.txt";
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                if (question is MCQQuestion mcq)
                {
                    writer.WriteLine($"MultipleChoice|{question.Header}|{question.Body}|{question.Mark}|{string.Join(",", mcq.Choices.Select(c => c.AnswerText))}");
                }
                else if (question is TrueFalseQuestion tf)
                {
                    writer.WriteLine($"TrueFalse|{question.Header}|{question.Body}|{question.Mark}|True,False");
                }
            }
        }

        public void ReadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found.");

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        var parts = line.Split('|');
                        if (parts.Length < 5)
                            throw new FormatException("Invalid line format.");

                        string questionType = parts[0];
                        string header = parts[1];
                        string body = parts[2];
                        int mark = int.Parse(parts[3]);
                        string optionsString = parts[4];
                        Question question = null;
                        if (questionType == "MultipleChoice")
                        {
                            var options = parts[4].Split(',');
                            var choices = new AnswersList();
                            foreach (var option in options)
                            {
                                choices.Add(new Answer(option.Trim()) , false);
                            }
                            question = new MCQQuestion
                            {
                                Header = header,
                                Body = body,
                                Mark = mark,
                                Choices = choices
                            };
                        }
                        else if (questionType == "TrueFalse")
                        {
                            question = new TrueFalseQuestion
                            {
                                Header = header,
                                Body = body,
                                Mark = mark,

                            };
                        }
                        else
                        {
                            throw new NotImplementedException($"Question type {questionType} is not implemented.");
                        }

                        this.Add(question);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Skipping invalid question line: {line}. Error: {ex.Message}");
                    }
                }
            }
        }
    }
}
