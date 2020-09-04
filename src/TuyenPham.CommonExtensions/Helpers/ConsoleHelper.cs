using System;
using System.Collections.Generic;
using System.Linq;
using TuyenPham.Base.Interfaces;

namespace TuyenPham.Base.Helpers
{
    public static class ConsoleHelper
    {
        public static T PromptSelectItem<T>(IList<T> items, string selectedItem, string question)
               where T : class, IHasName
        {
            T output = null;

            do
            {
                var availableItemNames = items
                    .Select(i => i.Name)
                    .OrderBy(i => i)
                    .Join();

                Console.Write($"{question} ({availableItemNames}): ");

                if (!string.IsNullOrEmpty(selectedItem))
                    Console.WriteLine(selectedItem);

                var input = selectedItem ?? Console.ReadLine() ?? string.Empty;

                selectedItem = null;

                var selectedItems = items
                    .Where(i => i.Name.StartsWith(input, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                switch (selectedItems.Count)
                {
                    case 0:
                        Console.Error.WriteLine("No match found.");
                        break;

                    case 1:
                        output = selectedItems[0];
                        break;

                    default:
                        Yellow($"Too many matches found: ({selectedItems.Select(i => i.Name).Join()})");
                        break;
                }
            } while (output == null);


            Green(output.Name);
            Console.WriteLine();
            return output;
        }

        public static void Yellow(object message, bool newLine = true)
        {
            Color(message, ConsoleColor.Yellow, newLine);
        }

        public static void Green(object message, bool newLine = true)
        {
            Color(message, ConsoleColor.Green, newLine);
        }

        public static void Red(object message, bool newLine = true)
        {
            Color(message, ConsoleColor.Red, newLine);
        }

        private static void Color(object message, ConsoleColor color, bool newLine)
        {
            Console.Out.Flush();
            Console.ForegroundColor = color;

            if (newLine)
                Console.WriteLine(message);
            else
                Console.Write(message);

            Console.ForegroundColor = ConsoleColor.White;
            Console.Out.Flush();
        }
    }
}
