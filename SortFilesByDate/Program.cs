using System;
using System.IO;

namespace SortFilesByDate
{
    class Program
    {
        static CreateFolders _createFolders = new CreateFolders();
        private static void Main(string[] args)
        {
            UsersFolderDecisions();
            _createFolders.StartSorting();

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static void UsersFolderDecisions()
        {
            string path;

            Console.WriteLine("This program sorts files by their date of last modification. Creating new folders for matching files." +
                "\nTo select a folder copy and paste it's path, then press enter.");
            do
            {
                Console.Write("\n\nPath:\t");
                path = Console.ReadLine();

            } while (!_createFolders.FolderSelection(path));
            _createFolders.MyPathToSort = path;
            path = "";

            Console.Write("\n\nTo start give path where new folder will be placed. In case of no path current path will be selected\nPath:\t");
            do
            {         
                path = Console.ReadLine();
            } while (!_createFolders.FolderCreation(path));

            FolderSortingDecission();
        }

        private static void FolderSortingDecission()
        {
            Console.WriteLine("\n\nDecide if you want to sort by:\n[1]\t- year\n[2]\t- month\n[3]\t- day");
            int decide = 0;
            bool exit = false;
            do
            {
                string decission = Console.ReadLine();
                try
                {
                    decide = Int16.Parse(decission);
                }
                catch (Exception) { }

                if (decide == 1 || decide == 2 || decide == 3)
                {
                    exit = true;
                }
            } while (!exit);

            _createFolders.MyDecission = decide;
        }
    }
}
