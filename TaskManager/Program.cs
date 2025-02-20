using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

class Program
{
    // List to store tasks
    static List<TaskItem> tasks = new List<TaskItem>();
    static void Main(string[] args)
    {
        LoadTasks();  // Load tasks from file

        while (true)
        {
            // Display the main menu
            Console.WriteLine("- Task Manager - ");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Display Tasks");
            Console.WriteLine("3. Mark Task as Completed");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            // Handle user's menu choice
            switch (choice)
            {
                case "1":
                    AddTask();
                    break;

                    case "2":
                    DisplayTasksWithColors();
                    break;  

                    case"3":
                    MarkTaskCompleted();
                    break;

                    case "4":
                        SaveTasks();
                    return;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();    // Wait for user input
            Console.Clear();
        }
    }

    // Get a valid category from user input
    static string GetValidCategory()
    {
        while (true)
        {
            Console.Write("Enter category (Personal, Work, Study): ");
            string category = Console.ReadLine().ToLower();
            if (category == "personal" || category == "work" || category == "study")
        {
                return category;
            }
            Console.WriteLine("Invalid category! Please choose from Personal, Work, or Study.");
        }
    }
    // Add a new task to the task list
    static void AddTask()
    {
        Console.Write("Enter title: ");
        string title = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty!");
            return;
        }

        // Get task details from user
        Console.Write("Enter description: ");
        string description = Console.ReadLine();

        Console.Write("Enter due date (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
        {
            Console.WriteLine("Invalid date format!");
            return;
        }

        string category = GetValidCategory();

        // Add new task to the list
        tasks.Add(new TaskItem
        {
            Title = title,
            Description = description,
            DueDate = dueDate,
            IsCompleted = false,
            Category = category
        });

        Console.WriteLine("Task added successfully!");
    }

    // Display all tasks with color-coded categories
    static void DisplayTasksWithColors()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        // Loop through tasks and display them with category-specific colors
        for (int i = 0; i < tasks.Count; i++)
        {
            var task = tasks[i];
            switch (task.Category.ToLower())
            {

                case "personal":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "work":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "study":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            // Display task details
            Console.WriteLine($"{i}. Title: {task.Title}, Due Date: {task.DueDate.ToShortDateString()}, Completed: {task.IsCompleted}, Category: {task.Category}");
            Console.ResetColor();
        }
    }

    // Mark a task as completed by its index
    static void MarkTaskCompleted()
    {
        Console.WriteLine("Enter the index of the task to mark as completed: ");
        if (!int.TryParse(Console.ReadLine(), out int index) || index < 0 || index >= tasks.Count)
        {
            Console.WriteLine("Invalid index!");
            return;
        }
        tasks[index].IsCompleted = true;
        Console.WriteLine("Task marked as completed!");
    }


    // Define the file path to save and load tasks
    static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "tasks.txt");


    // Save all tasks to a file
    static void SaveTasks()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var task in tasks)
            {
                writer.WriteLine($"{task.Title}|{task.Description}|{task.DueDate}|{task.IsCompleted}|{task.Category}");
            }
        }
        Console.WriteLine("Tasks saved successfully!");
    }

    // Load tasks from a file
    static void LoadTasks()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                string[] parts = line.Split('|');
                tasks.Add(new TaskItem
                {
                    Title = parts[0],
                    Description = parts[1],
                    DueDate = DateTime.Parse(parts[2]),
                    IsCompleted = bool.Parse(parts[3]),
                    Category = parts[4]
                });
            }
        }
        Console.WriteLine("Tasks loaded successfully!");
    }
}