﻿using System;
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
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
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
}