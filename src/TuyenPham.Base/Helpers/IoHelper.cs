using System;
using System.IO;
using System.Threading.Tasks;

namespace TuyenPham.Base.Helpers
{
    public static class IoHelper
    {
        //public static async Task DeleteFolderAsync(string folderPath)
        //{
        //    do
        //    {
        //        ConsoleHelper.Yellow($"Deleting '{folderPath}'...");

        //        try
        //        {
        //            await DeleteFolderInternalAsync(folderPath);
        //        }
        //        catch
        //        {
        //            ConsoleHelper.Yellow(
        //                $"Can not delete '{folderPath}'. Press 'Enter' to try again or delete it manually then come back here.");

        //            Console.ReadKey();
        //        }
        //    } while (Directory.Exists(folderPath));

        //    ConsoleHelper.Green("Deleted.");

        //    Console.WriteLine();
        //}

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