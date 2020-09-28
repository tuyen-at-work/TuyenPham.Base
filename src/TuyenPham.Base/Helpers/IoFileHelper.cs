using System;
using System.Collections.Generic;
using System.IO;

namespace TuyenPham.Base.Helpers
{
    public static class IoFileHelper
    {
        public static void DeleteAll(
            this IEnumerable<FileInfo> fiItems,
            Action<FileInfo> beforeDeleteAction = null,
            Action<FileInfo> afterDeleteAction = null)
        {
            foreach (var fi in fiItems)
            {
                beforeDeleteAction?.Invoke(fi);

                if (fi.Exists)
                    fi.Delete();

                afterDeleteAction?.Invoke(fi);
            }
        }

        public static void DeleteIfExist(this FileInfo fi)
        {
            if (fi.Exists)
                fi.Delete();
        }
    }
}
