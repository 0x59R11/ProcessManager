using ProcessManager.BL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManager.BL.Controller
{
    public sealed class ProcessItemController : ControllerBase
    {
        public List<ProcessItem> Processes { get; }


        public ProcessItemController()
        {
            Processes = GetProcessesData();
        }



        private List<ProcessItem> GetProcessesData()
        {
            return Load<ProcessItem>() ?? new List<ProcessItem>();
        }


        public List<ProcessItem> GetProcessesByNameOrID(List<string> param)
        {
            List<ProcessItem> result = new List<ProcessItem>();

            foreach (string par in param)
            {
                try
                {
                    ProcessItem item = Processes.FirstOrDefault(el => el.Name.ToLower() == par || el.Name.ToLower().StartsWith(par)) ?? Processes.FirstOrDefault(el => el.Id == int.Parse(par));
                    result.Add(item ?? throw new FormatException());
                }
                catch (FormatException)
                {
                    continue;
                }
            }
            return result;
        }
        public ProcessItem GetProcessByNameOrID(string param)
        {
            ProcessItem result = null;
            try
            {
                result = Processes.FirstOrDefault(el => el.Name.ToLower() == param || el.Name.ToLower().StartsWith(param)) ?? Processes.FirstOrDefault(el => el.Id == int.Parse(param));
            }
            catch { }
            return result;
        }

        public void StartProcess(ProcessItem item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentException("Item cannot be null");
                }


                Console.Write(item.Name + " ->  ");
                Process process = Process.Start(item.Directory);
                Console.WriteLine("Started!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void StartProcess(string param)
        {
            try
            {
                ProcessItem item = GetProcessByNameOrID(param);
                StartProcess(item);
            }
            catch (FormatException)
            {
                Console.WriteLine("Process not found!");
            }
        }

        public bool AddProcess(ProcessItem item)
        {
            bool result = false;
            try
            {
                if (item == null)
                {
                    throw new ArgumentException("Item cannot be null");
                }
                if (Processes?.FirstOrDefault(el => el.Id == item.Id) != null)
                {
                    throw new ArgumentException("A process with this Id already exists.");
                }
                if (Processes?.FirstOrDefault(el => el.Name == item.Name) != null)
                {
                    throw new ArgumentException("A process with this Name already exists.");
                }


                Processes?.Add(item);
                Save();
                result = true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public bool RemoveProcess(ProcessItem item)
        {
            bool? result = false;
            result = Processes?.Remove(item);
            Save();
            return result.Value;
        }
        public bool ChangeProcess(ref ProcessItem item, int? id, string name, string directory, string description)
        {
            bool result = false;
            try
            {
                if (item == null)
                {
                    throw new ArgumentException("Item cannot be null");
                }
                if (id != null && id.HasValue)
                {
                    if (Processes.FirstOrDefault(el => el.Id == id.Value) != null)
                    {
                        throw new ArgumentException("A process with this Id already exists.");
                    }
                    item.Id = id.Value;
                }
                if (!string.IsNullOrWhiteSpace(name))
                {
                    if (Processes.FirstOrDefault(el => el.Name == name) != null)
                    {
                        throw new ArgumentException("A process with this Name already exists.");
                    }
                    item.Name = name;
                }
                if (!string.IsNullOrWhiteSpace(directory)) { item.Directory = directory; }
                if (!string.IsNullOrWhiteSpace(description)) { item.Description = description; }

                Save();
                result = true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }



        public void Save()
        {
            Save(Processes);
        }
    }
}
