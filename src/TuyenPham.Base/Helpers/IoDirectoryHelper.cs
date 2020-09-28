using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TuyenPham.Base.Helpers
{
    public static class IoDirectoryHelper
    {
        public static FileInfo GetFile(this DirectoryInfo di, params string[] fileParts)
        {
            var path = fileParts.Aggregate(di.FullName, Path.Combine);
            return new FileInfo(Path.Combine(di.FullName, path));
        }

        public static DirectoryInfo GetFolder(this DirectoryInfo di, params string[] folderParts)
        {
            var path = folderParts.Aggregate(di.FullName, Path.Combine);
            return new DirectoryInfo(Path.Combine(di.FullName, path));
        }

        public static IEnumerable<FileInfo> FilterFiles(
            this DirectoryInfo di,
            string pattern,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return di.Exists ? di.EnumerateFiles(pattern, searchOption) : null;
        }

        public static IEnumerable<DirectoryInfo> FilterFolders(
        this DirectoryInfo di,
        string pattern,
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return di.Exists ? di.EnumerateDirectories(pattern, searchOption) : null;
        }

        public static async Task CopyDirectoryAsync(
            string sourceFolder,
            string destinationFolder,
            bool deleteFirst)
        {
            //if (deleteFirst)
            //    await DeleteFolderAsync(destinationFolder);

            ConsoleHelper.Yellow($"Copy '{sourceFolder}' to '{destinationFolder}'");

            //  /e	        Copies subdirectories. This option automatically includes empty directories.
            //  /mir	    Mirrors a directory tree (equivalent to /e plus /purge).
            //              Using this option with the /e option and a destination directory,
            //              overwrites the destination directory security settings.
            //  /ndl	    Specifies that directory names are not to be logged.
            //  /nfl	    Specifies that file names are not to be logged.
            //  /njh	    Specifies that there is no job header.
            //  /purge	    Deletes destination files and directories that no longer exist in the source.
            //              Using this option with the /e option and a destination directory,
            //              allows the destination directory security settings to not be overwritten.
            var result = await ProcessHelper.RunAsync(
                "robocopy",
                $@"""{sourceFolder}"" ""{destinationFolder}"" /E {(deleteFirst ? "/MIR" : "")} /NFL /NDL /NJH",
                sourceFolder);

            if (result == 1)
                ConsoleHelper.Green("Copied successfully.");

            else
                throw new Exception($"Copy failed with code {result}.");

            Console.WriteLine();
        }

        public static void DeleteAll(
            this IEnumerable<DirectoryInfo> diItems,
            Action<DirectoryInfo> beforeDeleteAction = null,
            Action<DirectoryInfo> afterDeleteAction = null)
        {
            foreach (var di in diItems)
            {
                beforeDeleteAction?.Invoke(di);

                if (di.Exists)
                    di.Delete(true);

                afterDeleteAction?.Invoke(di);
            }
        }

        public static void DeleteIfExist(this DirectoryInfo di)
        {
            if (di.Exists)
                di.Delete(true);
        }
    }
}
