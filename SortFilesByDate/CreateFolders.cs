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
        private static int myDecission;
        public int MyDecission { get { return myDecission; } set { myDecission = value; } }

        private static string myPathToSort;
        public string MyPathToSort { get { return myPathToSort; } set { myPathToSort = value; } }

        private static string myPathNewFolder;

        private static List<string> _files = new List<string>();
        private static string _dateFormat;

        public void StartSorting()
        {
            _dateFormat = TimeFormat();
            CreateFoldersAndSortItems();
        }

        private static void CreateFoldersAndSortItems()
        {
            for (int i = 0; i < _files.Count; i++)
            {
                string monthFolderName = File.GetLastWriteTime(Path.Combine(myPathToSort, _files[i])).ToString(_dateFormat);
                string monthFolderPath = Path.Combine(myPathNewFolder, monthFolderName);

                if (!Directory.Exists(monthFolderPath))
                {
                    Directory.CreateDirectory(monthFolderPath);
                }

                File.Move(Path.Combine(myPathToSort, Path.GetFileName(_files[i])), Path.Combine(monthFolderPath, Path.GetFileName(_files[i])));
            }
        }

        private static string TimeFormat()
        {
            if (myDecission == 3)
            {
                return "yyyy-MM-dd";
            }
            else if (myDecission == 2)
            {
                return "yyyy-MM";
            }
            else
            {
                return "yyyy";
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
                myPathNewFolder = Path.Combine(path, "SortedFiles");
                Directory.CreateDirectory(myPathNewFolder);

                return true;
            }
            else
            {
                myPathNewFolder = Path.Combine(myPathToSort, "SortedFiles");
                Directory.CreateDirectory(myPathNewFolder);

                return true;
            }
        }

        public CreateFolders()
        { }
    }
}
