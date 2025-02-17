using System;
using System.Collections.Generic;
using System.ComponentModel;

class Program
{
    static List<TaskItem> tasks = new List<TaskItem>();
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("- Task Manager - ");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Display Tasks");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;

                    case "2":
                    DisplayTasks();
                    break;  
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void AddTask()
    {
        Console.Write("Enter title: ");
        string title = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty!");
            return;
        }

        Console.Write("Enter description: ");
        string description = Console.ReadLine();

        Console.Write("Enter due date (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
        {
            Console.WriteLine("Invalid date format!");
            return;
        }

        tasks.Add(new TaskItem
        {
            Title = title,
            Description = description,
            DueDate = dueDate,
            IsCompleted = false,
            Category = "Personal"
        });

        Console.WriteLine("Task added successfully!");
    }

    static void DisplayTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }
        for (int i = 0; i < tasks.Count; i++)
        {
            var task = tasks[i];
            Console.WriteLine($"{i}. Title {task.Title}, Due Date: {task.DueDate.ToShortDateString()}, Completed: {task.IsCompleted}");
        }
    }
}