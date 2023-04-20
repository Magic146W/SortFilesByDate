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
            string path = "";

            Console.WriteLine("This program sorts files by their date of last modification. Creating new folders for matching files." +
                "\nTo select a folder copy and paste it's path, then press enter.");
            do
            {
                path = GeneratePath(path);
            } while (!_createFolders.FolderSelection(path));
            _createFolders.MyPathToSort = path;

            Console.Write("\n\nTo start give path where new folder will be placed. In case of no path current path will be selected");
            do
            {
                path = GeneratePath(path);
            } while (!_createFolders.FolderCreation(path));

            FilesSortingDecission();
        }

        private static string GeneratePath(string path)
        {
            Console.Write("\n\nPath:\t");
            return Console.ReadLine();
        }

        private static void FilesSortingDecission()
        {
            Console.WriteLine("\n\nDecide if you want to limit files by \"date\" (yyyy-MM-dd):\n[1] -\tYes \n[2] -\tNo");            
            GetUsersDecissionInput(1);
        }

        private static void GetUsersDecissionInput(int step)
        {
            int decide = 0;
            bool exit = false;
            string decission = "";
            do
            {
                decission = Console.ReadLine();
                
                try
                {
                    if (step == 1)
                    {
                        decide = Int16.Parse(decission);

                        if (decide == 1)
                        {
                            Console.WriteLine("\n\nGive correct date, from which point you want files to be taken. (e.g. 2021-01-01)");
                            step = 2;
                        }
                        else if (decide == 2)
                        {
                            exit = true;
                        }
                    }
                    if (step == 2)
                    {
                        bool isValid = DateTime.TryParseExact(decission, CreateFolders.TimeFormat(), null, System.Globalization.DateTimeStyles.None, out DateTime result);
                        _createFolders.MySelectedDate = decission;

                        if (isValid)
                            exit = true;
                        else
                            Console.WriteLine($"The input date {decission} is not valid.");
                    }                
                }
                catch (Exception) { }             
            } while (!exit);
        }   
    }
}
