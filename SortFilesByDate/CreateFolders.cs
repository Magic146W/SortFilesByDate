using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortFilesByDate
{
    class CreateFolders
    {
        private static int myDecission = 3;
        public int MyDecission { get { return myDecission; } set { myDecission = value; } }

        private static string myPathToSort;
        public string MyPathToSort { get { return myPathToSort; } set { myPathToSort = value; } }

        private static string fromSelectedDate;
        public string MySelectedDate { get { return fromSelectedDate; } set { fromSelectedDate = value; } }

        private static List<string> _files = new List<string>();
        private static string _dateFormat;
        private static string myPathNewFolder;

        public void StartSorting()
        {
            _dateFormat = TimeFormat();
            CreateFoldersForItems();
        }

        private static void CreateFoldersForItems()
        {
            for (int i = 0; i < _files.Count; i++)
            {
                string dateFolderName = "", dateFolderPath = "";
                string myPathNewFolderBackup = myPathNewFolder;

                if (CheckWithUserFromDateDecission(File.GetLastWriteTime(Path.Combine(myPathToSort, _files[i])).ToString("yyyy-MM-dd")))
                {
                    for (int j = 1; j < 4; j++)
                    {
                        myDecission = j;
                        dateFolderName = File.GetLastWriteTime(Path.Combine(myPathToSort, _files[i])).ToString(TimeFormat());
                        dateFolderPath = Path.Combine(myPathNewFolderBackup, dateFolderName);
                        myPathNewFolderBackup = dateFolderPath;

                        if (!Directory.Exists(dateFolderPath))
                        {
                            Directory.CreateDirectory(dateFolderPath);
                        }
                    }
                    File.Move(Path.Combine(myPathToSort, Path.GetFileName(_files[i])), Path.Combine(dateFolderPath, Path.GetFileName(_files[i])));
                }
            }
        }

        private static bool CheckWithUserFromDateDecission(string date)
        {
            if (fromSelectedDate == null)
                return true;

            DateTime date1 = DateTime.Parse(fromSelectedDate);
            DateTime date2 = DateTime.Parse(date);

            if (DateTime.Compare(date1, date2) < 0)
            {
                return true;
            }

            return false;
        }

        public static string TimeFormat()
        {
            if (myDecission == 3)
            {
                return "yyyy-MM-dd";
            }
            else if (myDecission == 2)
            {
                return "yyyy-MM";
            }
            else if (myDecission == 1)
            {
                return "yyyy";
            }
            else
            {
                return "";
            }
        }

        public bool FolderSelection(string path)
        {
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                _files = files.ToList<string>();

                return true;
            }
            else
            {
                Console.WriteLine($"Folder doesn't exist.");
                return false;
            }
        }

        public bool FolderCreation(string path)
        {
            if (Directory.Exists(path))
            {
                FolderPath(path);
            }
            else
            {
                FolderPath(myPathToSort);
            }

            return true;
        }

        private void FolderPath(string path)
        {
            myPathNewFolder = Path.Combine(path, "SortedFiles");
            Directory.CreateDirectory(myPathNewFolder);
        }

        public CreateFolders()
        { }
    }
}
