﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TuyenPham.CommonExtensions.Helpers
{
    public static class IoHelper
    {
        public static FileInfo GetFile(this DirectoryInfo di, params string[] fileParts)
        {
            var path = fileParts.Aggregate(di.FullName, Path.Combine);
            return new FileInfo(Path.Combine(di.FullName, path));
        }

        public static DirectoryInfo GetFolder(this DirectoryInfo di, params string[] fileParts)
        {
            var path = fileParts.Aggregate(di.FullName, Path.Combine);
            return new DirectoryInfo(Path.Combine(di.FullName, path));
        }

        public static async Task CopyDirectoryAsync(
            string sourceFolder,
            string destinationFolder,
            bool deleteFirst)
        {
            if (deleteFirst)
                await DeleteFolderAsync(destinationFolder);

            ConsoleHelper.Yellow($"Copy '{sourceFolder}' to '{destinationFolder}'");

            var result = ProcessHelper.RunAsync(
                "robocopy",
                $@"""{sourceFolder}"" ""{destinationFolder}"" /e /NFL /NDL /NJH",
                sourceFolder);

            if (result == 1)
                ConsoleHelper.Green("Copied successfully.");

            else
                throw new Exception($"Copy failed with code {result}.");

            Console.WriteLine();
        }

        public static async Task DeleteFolderAsync(string folderPath)
        {
            do
            {
                ConsoleHelper.Yellow($"Deleting '{folderPath}'...");

                try
                {
                    await DeleteFolderInternalAsync(folderPath);
                }
                catch
                {
                    ConsoleHelper.Yellow(
                        $"Can not delete '{folderPath}'. Press 'Enter' to try again or delete it manually then come back here.");

                    Console.ReadKey();
                }
            } while (Directory.Exists(folderPath));

            ConsoleHelper.Green("Deleted.");

            Console.WriteLine();
        }

        private static async Task DeleteFolderInternalAsync(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                return;

            foreach (var directory in Directory.GetDirectories(folderPath))
            {
                await DeleteFolderInternalAsync(directory);
            }

            var attempts = 0;

            do
            {
                attempts++;

                try
                {
                    foreach (var file in Directory.GetFiles(folderPath))
                    {
                        var attributes = File.GetAttributes(file);
                        var newAttributes = attributes ^ FileAttributes.ReadOnly;

                        if (attributes != newAttributes)
                            File.SetAttributes(file, newAttributes);
                    }

                    Directory.Delete(folderPath, true);
                }
                catch (DirectoryNotFoundException)
                {
                    break;
                }
                catch
                {
                    await Task.Delay(100 * attempts);
                }
            } while (attempts < 10 && Directory.Exists(folderPath));
        }
    }
}