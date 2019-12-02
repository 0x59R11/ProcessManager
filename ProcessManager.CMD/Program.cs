using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessManager.BL;
using ProcessManager.BL.Model;
using ProcessManager.BL.Controller;
using System.Reflection;

namespace ProcessManager.CMD
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = Assembly.GetExecutingAssembly().GetName().Name;

            Wr(ConsoleColor.White, ConsoleColor.DarkYellow, true, "       [ProcessManager]        ");
            Jump(2);



            while (true)
            {
                Console.WriteLine($"\n{new string('-', 12)}[TODO]{new string('-', 12)}");

                Console.WriteLine($"[E] -> Start Process(es)");
                Console.WriteLine($"[G] -> Process Groups");
                Console.WriteLine($"[F] -> Show list Processes");
                Console.WriteLine($"[A] -> Add new Process");
                Console.WriteLine($"[D] -> Remove Process");
                Console.WriteLine($"[C] -> Change Process");
                Console.WriteLine($"[H] -> Help");
                Console.WriteLine($"[Q] -> Exid");


                Console.WriteLine(new string ('-', 30));

                Jump(1);
                Console.Write("-> ");

                ConsoleKey key = Console.ReadKey().Key;


                switch (key)
                {
                    case  ConsoleKey.E: // Start Process(es)
                        #region Start Process(es)
                        Console.Write(" [Start Process(es)]");
                        Jump(2);

                        ProcessItemController processItemController = new ProcessItemController();

                        if (DrawProcesses())
                        {
                            List<string> par = ParseParametrs("-> ", 1);

                            foreach (string item in par)
                            {
                                processItemController.StartProcess(item.ToLower());
                            }
                            Jump(1);
                        }
                        #endregion
                        break;
                    case ConsoleKey.G: // Process Groups
                        #region Proces-Groups
                        Console.Write(" [Process Groups]");
                        Jump(2);
                        bool back = false;

                        while (true)
                        {
                            Console.WriteLine($"{new string('-', 10)}[TODO]{new string('-', 10)}");

                            Console.WriteLine($"[E] -> Start Process Group(s)");
                            Console.WriteLine($"[F] -> Show list Process Groups");
                            Console.WriteLine($"[A] -> Add Process Group");
                            Console.WriteLine($"[D] -> Remove Process Group");
                            Console.WriteLine($"[C] -> Change Process Group");
                            Console.WriteLine($"[Q] -> Back");

                            Console.WriteLine(new string('-', 26));

                            Jump(1);
                            Console.Write("-> ");

                            ConsoleKey keyGroup = Console.ReadKey().Key;

                            switch (keyGroup)
                            {
                                case ConsoleKey.E: // Start Process Groups
                                    #region Start-Process-Group
                                    Console.Write(" [Start Process Groups]");
                                    Jump(2);

                                    ProcessItemGroupController groupController = new ProcessItemGroupController();

                                    if (DrawProcessGroups())
                                    {
                                        List<string> par = ParseParametrs("-> ", 1);

                                        foreach (string item in par)
                                        {
                                            groupController.StartProcessGroup(item);
                                        }
                                        Jump(1);
                                    }
                                    #endregion
                                    break;
                                case ConsoleKey.F: // Show list Process Groups
                                    #region Show-list-Process-Group
                                    Console.Write(" [Show list Procrss Groups]");
                                    Jump(2);
                                    DrawProcessGroups();
                                    #endregion
                                    break;
                                case ConsoleKey.A: // Add Process Group
                                    #region Add-Process-Group
                                    Console.Write(" [Add Process Group]");
                                    Jump(2);

                                    ProcessItemGroupController groupController2 = new ProcessItemGroupController();
                                    ProcessItemController processItemController2 = new ProcessItemController();

                                    DrawProcessGroups();

                                    int groupId = ParseInt32("-> ID: ", 1);
                                    string groupName = ParseString("-> Name: ");
                                    Jump(1);
                                    DrawProcesses();

                                    List<string> groupList = ParseParametrs("-> Processes: ", 1);
                                    List<ProcessItem> processes = processItemController2.GetProcessesByNameOrID(groupList);

                                    try
                                    {
                                        ProcessItemGroup group = new ProcessItemGroup(groupId, groupName, processes);

                                        if (groupController2.AddProcessGroup(group))
                                        {
                                            Console.WriteLine("Done!\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error!\n");
                                        }
                                    }
                                    catch (ArgumentException ex) { Console.WriteLine(ex.Message); }
                                    #endregion
                                    break;
                                case ConsoleKey.D: // Remove Process Group
                                    #region Remove-Process-Group
                                    Console.Write(" [Remove Process Group]");
                                    Jump(2);

                                    ProcessItemGroupController groupController3 = new ProcessItemGroupController();

                                    
                                    if (DrawProcessGroups())
                                    {
                                        string parametr = ParseString("-> Name/ID: ", 1);


                                        try
                                        {
                                            ProcessItemGroup group = groupController3.GetProcessGroupByNameOrID(parametr.ToLower());

                                            if (group != null)
                                            {
                                                if (groupController3.RemoveProcessGroup(group))
                                                {
                                                    Console.WriteLine("Done!\n");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Error!\n");
                                                }
                                            }
                                        }
                                        catch (FormatException) { Console.WriteLine("Process group not found!"); }
                                    }
                                    #endregion
                                    break;
                                case ConsoleKey.C: // Change Process Group
                                    #region Change-Process-Group
                                    Console.Write(" [Change Process Group]");
                                    Jump(2);

                                    ProcessItemGroupController groupController4 = new ProcessItemGroupController();
                                    ProcessItemController processItemController5 = new ProcessItemController();

                                    if (DrawProcessGroups())
                                    {
                                        string parametr = ParseString("-> Name/ID: ", 1);

                                        try
                                        {
                                            ProcessItemGroup group2 = groupController4.GetProcessGroupByNameOrID(parametr);

                                            if (group2 != null)
                                            {
                                                int? newId = ParseInt32("-> New ID: ", 1);
                                                string newName = ParseString("-> New Name: ", 0, true);
                                                Jump(1);
                                                DrawProcesses();

                                                List<string> groupList2 = ParseParametrs("-> New Processes: ", 1);
                                                List<ProcessItem> processes2 = processItemController5.GetProcessesByNameOrID(groupList2);

                                                if (groupController4.ChangeProcessGroup(ref group2, newId == 0 ? null : newId, string.IsNullOrWhiteSpace(newName) ? null : newName, processes2))
                                                {
                                                    Console.WriteLine("Done!\n");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Error!\n");
                                                }
                                            }
                                        }
                                        catch (FormatException) { Console.WriteLine("Process group not found!"); }
                                    }
                                    #endregion
                                    break;
                                case ConsoleKey.Q: // Back
                                    back = true;
                                    break;
                                default:
                                    Console.WriteLine(" - key not found!");
                                    Jump(1);
                                    continue;
                            }
                            if (back) { Console.WriteLine(" - [Press Enter]"); break; }
                            else { Console.ReadLine(); }
                        }
                        #endregion
                        break;
                    case ConsoleKey.F: // Show list Processes
                        #region Show-list_Processes
                        Console.Write(" [Show list Processes]");
                        Jump(2);
                        DrawProcesses();
                        #endregion
                        break;
                    case ConsoleKey.A: // Add Process
                        #region Add-Process
                        Console.Write(" [Add Process]");
                        Jump(2);

                        ProcessItemController p = new ProcessItemController();

                        DrawProcesses();

                        int id = ParseInt32("-> ID: ", 1);
                        string name = ParseString("-> Name: ");
                        string directory = ParseString("-> Directory: ");
                        string description = ParseString("-> Description: ", 0, true);

                        try
                        {
                            ProcessItem item = new ProcessItem(id, name, directory, description);


                            if (p.AddProcess(item))
                            {
                                Console.WriteLine("Done!\n");
                            }
                            else
                            {
                                Console.WriteLine("Error!\n");
                            }
                        }
                        catch (ArgumentException ex) { Console.WriteLine(ex.Message); }
                        #endregion
                        break;
                    case ConsoleKey.D: // Remove Process
                        #region Remove-Process
                        Console.Write(" [Remove Process]");
                        Jump(2);

                        ProcessItemController p2 = new ProcessItemController();

                        if (DrawProcesses())
                        {
                            string parametr = ParseString("-> Name/ID: ", 1);


                            try
                            {
                                ProcessItem item2 = p2.GetProcessByNameOrID(parametr.ToLower());

                                if (item2 != null)
                                {
                                    if (p2.RemoveProcess(item2))
                                    {
                                        Console.WriteLine("Done!\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error!\n");
                                    }
                                }
                                else { throw new FormatException(); }
                            }
                            catch (FormatException) { Console.WriteLine("Process not found!"); }
                        }
                        #endregion
                        break;
                    case ConsoleKey.C: // Change Process
                        #region Change-Process
                        Console.Write(" [Change Process]");
                        Jump(2);

                        ProcessItemController p3 = new ProcessItemController();

                        if (DrawProcesses())
                        {
                            string parametr2 = ParseString("-> Name/ID: ", 1);


                            try
                            {
                                ProcessItem item3 = p3.GetProcessByNameOrID(parametr2.ToLower());

                                if (item3 != null)
                                {

                                    int? id2 = ParseInt32("-> New ID: ", 1);
                                    string name2 = ParseString("-> New Name: ", 0, true);
                                    string directory2 = ParseString("-> New Directory: ", 0, true);
                                    string description2 = ParseString("-> New Description: ", 0, true);

                                    if (p3.ChangeProcess(ref item3, id2 == 0 ? null : id2, name2, directory2, description2))
                                    {
                                        Console.WriteLine("Done!\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error!\n");
                                    }
                                }
                                else { throw new FormatException(); }
                            }
                            catch (FormatException) { Console.WriteLine("Process not found!"); }
                        }
                        #endregion
                        break;
                    case ConsoleKey.H: // Help
                        #region Help

                        Console.Write(" [Help]");

                        #endregion
                        break;
                    case ConsoleKey.Q: // Eqid
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine(" - key not found!");
                        Jump(1);
                        continue;
                }
                Console.ReadLine();
            }
        }



        private static void Jump(int count)
        {
            if (count <= 0) { return; }
            string str = "";
            for (int i = 0; i < count; i++) { str += "\n"; }
            Console.Write(str);
        }

        private static List<string> ParseParametrs(string text, int skip = 0)
        {
            List<string> list = new List<string>();
            Jump(skip);

            while (true)
            {
                Console.Write(text);

                string result = Console.ReadLine();
                try
                {
                    if (string.IsNullOrWhiteSpace(result)) { throw new FormatException(); }

                    string[] array = result.Split(new char[] { ',', '.', '|' });

                    foreach(string item in array) { list.Add(item); }
                    return list;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"-> Wrong format.");
                }
            }
        }

        #region Not-Work_Parser
        //private static List<int> ParseParametrs(string text, int skip = 0)
        //{
        //    List<int> list = new List<int>();
        //    Jump(skip);

        //    while (true)
        //    {
        //        Console.Write(text);

        //        string result = Console.ReadLine();
        //        try
        //        {
        //            if (string.IsNullOrWhiteSpace(result)) { throw new FormatException(); }


        //            List<char> chars = new List<char>();

        //            for (int i = 0; i < result.Length; i++)
        //            {
        //                if (result[i] == ' ') { continue; }

        //                if ((result[i] >= '0' && result[i] <= '9') || result[i] == ',')
        //                {
        //                    chars.Add(result[i]);
        //                }
        //                else
        //                {
        //                    throw new FormatException();
        //                }
        //            }

        //            string element = "";
        //            for (int i = 0; i < chars.Count; i++)
        //            {
        //                if (chars[i] == ',') { if (int.TryParse(element, out int res)) { list.Add(res); element = ""; } }
        //                else { element += chars[i].ToString(); }
        //            }
        //            if (!string.IsNullOrWhiteSpace(element)) { if (int.TryParse(element, out int res)) { list.Add(res); element = ""; } }
        //            return list;

        //        }
        //        catch (FormatException)
        //        {
        //            Console.WriteLine($"-> Wrong format.");
        //        }
        //    }
        //}
        #endregion

        private static string ParseString(string text, int skip = 0, bool canBeNull = false)
        {
            Jump(skip);

            while (true)
            {
                Console.Write(text);

                string result = Console.ReadLine();
                try
                {
                    if (!canBeNull)
                    {
                        if (string.IsNullOrWhiteSpace(result)) { throw new FormatException(); }
                    }

                    return result;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"-> Wrong format.");
                }
            }
        }
        private static int ParseInt32(string text, int skip = 0)
        {
            Jump(skip);

            while (true)
            {
                Console.Write(text);
                if (int.TryParse(Console.ReadLine(), out int res))
                {
                    return res;
                }
                else
                {
                    Console.WriteLine($"-> Wrong format.");
                }
            }
        }

        private static bool DrawProcesses()
        {
            ProcessItemController process = new ProcessItemController();

            if (process.Processes.Count == 0)
            {
                Console.WriteLine("Processes not found! You can add new process - [A]");
                return false;
            }
            else
            {
                Console.WriteLine($"{new string('-', 17)}[Processes]{new string('-', 17)}");

                int count = process.Processes.Count;
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"-> {process.Processes[i].ToString()}");
                }

                Console.WriteLine(new string('-', 45));
                return true;
            }     
        }
        private static bool DrawProcessGroups()
        {
            ProcessItemGroupController process = new ProcessItemGroupController();

            if (process.ProcessGroups.Count == 0)
            {
                Console.WriteLine("Process Groups not found! You can add new Process Group - [A]");
                return false;
            }
            else
            {
                Console.WriteLine($"{new string('-', 15)}[Process-Groups]{new string('-', 15)}"); // [Processes] 11 - [Process-Groups] 16

                int count = process.ProcessGroups.Count;
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"-> {process.ProcessGroups[i].ToString()}");
                }

                Console.WriteLine(new string('-', 46));
                return true;
            }
        }

        private static void DrawHelp()
        {
            Console.WriteLine("HELP");
        }

        /// <summary>
        /// Написать в консоль.
        /// </summary>
        /// <param name="textColor"> Цвет текста. </param>
        /// <param name="newLine"> если True тогда сообщение будет в новой строке. </param>
        /// <param name="text"> текст. </param>
        private static void Wr(ConsoleColor textColor, bool newLine, params string[] text)
        {
            Console.ForegroundColor = textColor;
            switch (newLine)
            {
                case true:
                    Console.WriteLine(string.Join(" ", text));
                    break;
                case false:
                    Console.Write(string.Join(" ", text));
                    break;
            }
            Console.ResetColor();

        }
        /// <summary>
        /// Написать в консоль.
        /// </summary>
        /// <param name="textColor"> Цвет текста. </param>
        /// <param name="backgroudColor"> Цвет заднего фона. </param>
        /// <param name="newLine"> если True тогда сообщение будет в новой строке. </param>
        /// <param name="text"> текст. </param>
        private static void Wr(ConsoleColor textColor, ConsoleColor backgroudColor, bool newLine, params string[] text)
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroudColor;
            switch (newLine)
            {
                case true:
                    Console.WriteLine(string.Join(" ", text));
                    break;
                case false:
                    Console.Write(string.Join(" ", text));
                    break;
            }
            Console.ResetColor();
        }
    }
}
