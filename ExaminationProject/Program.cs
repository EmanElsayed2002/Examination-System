using System;
using System.Collections.Generic;

namespace ExaminationProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t\t\t==============================");
            Console.WriteLine("\t\t\t\t\t  Welcome to the Examination System!");
            Console.WriteLine("\t\t\t\t\t==============================");
            Console.ResetColor();

            var questions = new QuestionsList();
            var answers = new AnswersList();

            questions.ReadFromFile("questions.txt");
            answers.ReadFromFile("answers.txt");


            if (questions.Count == 0)
            {
                questions.Add(new TrueFalseQuestion("Q1:", "C# supports OOP principles?", 1));
                questions.Add(new MCQQuestion("Q2:", "Which of these is NOT a C# data type?", 1,
                    new AnswersList { new Answer("bool"), new Answer("float"), new Answer("integer") }));
                questions.Add(new TrueFalseQuestion("Q3:", "C# is case-sensitive?", 1));
                questions.Add(new MCQQuestion("Q4:", "What is the correct way to declare a constant in C#?", 1,
                    new AnswersList { new Answer("const int num = 5;"), new Answer("int const num = 5;"), new Answer("constant int num = 5;") }));
                questions.Add(new TrueFalseQuestion("Q5:", "Namespaces are used to organize code in C#?", 1));
                questions.Add(new MCQQuestion("Q6:", "Which of these is used to handle exceptions in C#?", 1,
                    new AnswersList { new Answer("try-catch"), new Answer("if-else"), new Answer("for loop") }));
                questions.Add(new TrueFalseQuestion("Q7:", "C# supports multiple inheritance directly?", 1));
                questions.Add(new MCQQuestion("Q8:", "Which keyword is used to create an abstract class in C#?", 1,
                    new AnswersList { new Answer("abstract"), new Answer("virtual"), new Answer("override") }));
                questions.Add(new TrueFalseQuestion("Q9:", "The 'static' keyword is used for creating shared members?", 1));
                questions.Add(new MCQQuestion("Q10:", "What does the 'using' directive in C# do?", 1,
                    new AnswersList { new Answer("Imports namespaces"), new Answer("Declares variables"), new Answer("Creates classes") }));

                answers.Add(new Answer("1"), true);
                answers.Add(new Answer("3"), true);
                answers.Add(new Answer("1"), true);
                answers.Add(new Answer("1"), true);
                answers.Add(new Answer("1"), true);
                answers.Add(new Answer("1"), true);
                answers.Add(new Answer("2"), true);
                answers.Add(new Answer("1"), true);
                answers.Add(new Answer("1"), true);
                answers.Add(new Answer("1"), true);
            }
            var practicalExam = new PracticalExam("C# Practice Exam", 60, questions.Count);
            var finalExam = new FinalExam("C# Final Exam", 60, questions.Count);

            for (int i = 0; i < questions.Count; i++)
            {
                practicalExam.QuestionAnswerDict.Add(questions[i], answers[i]);
                finalExam.QuestionAnswerDict.Add(questions[i], answers[i]);
            }

            int choice;
            char again;
            do
            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\t\t\t\t\t=====================================");
                    Console.WriteLine("\t\t\t\t\t\tSelect Exam Type:");
                    Console.WriteLine("\t\t\t\t\t=====================================");
                    Console.WriteLine("\t\t\t\t\t1. Practice Exam");
                    Console.WriteLine("\t\t\t\t\t2. Final Exam");
                    Console.WriteLine("\t\t\t\t\t3. Exit");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\t\t\t\t\tEnter Your Choice: ");
                    Console.ResetColor();

                    if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\t\t\t\t\tInvalid choice. Please try again.");
                        Console.ResetColor();
                    }
                } while (choice < 1 || choice > 3);

                switch (choice)
                {
                    case 1:
                        practicalExam.showExam();
                        break;
                    case 2:
                        finalExam.showExam();

                        double halfMark = questions.Count / 2.0;

                        if (finalExam.StudentMarks > halfMark)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\t\t\t\t\t\t\tYour Score: {finalExam.StudentMarks}/{questions.Count}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\t\t\t\t\t\t\tYour Score: {finalExam.StudentMarks}/{questions.Count}");
                            Console.WriteLine("\t\t\t\t\t\t\tStudy Hard");
                        }

                        Console.ResetColor();
                        break;

                    case 3:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("\t\t\t\t\tThank you for using the Examination System. Goodbye!");
                        Console.ResetColor();
                        Environment.Exit(0);
                        break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\t\t\t\t\tDo you want to take the test again (Y/N)? ");
                again = char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();
                Console.ResetColor();
                
            } while (again == 'y');
        }
    }
}
