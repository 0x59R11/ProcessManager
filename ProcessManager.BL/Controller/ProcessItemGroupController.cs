using ProcessManager.BL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManager.BL.Controller
{
    public sealed class ProcessItemGroupController : ControllerBase
    {
        public List<ProcessItemGroup> ProcessGroups { get; }


        public ProcessItemGroupController()
        {
            ProcessGroups = GetProcessGroupsData();
        }



        private List<ProcessItemGroup> GetProcessGroupsData()
        {
            return Load<ProcessItemGroup>() ?? new List<ProcessItemGroup>();
        }


        public void StartProcessGroup(ProcessItemGroup item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentException("Item cannot be null");
                }


                foreach (ProcessItem proc in item.Processes)
                {
                    Console.Write(proc.Name + " ->  ");
                    Process process = Process.Start(proc.Directory);
                    Console.WriteLine("Started!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void StartProcessGroup(string param)
        {
            try
            {
                ProcessItemGroup item = GetProcessGroupByNameOrID(param);
                StartProcessGroup(item);
            }
            catch (FormatException)
            {
                Console.WriteLine("Process not found!");
            }
        }


        public ProcessItemGroup GetProcessGroupByNameOrID(string param)
        {
            ProcessItemGroup result = null;
            try
            {
                result = ProcessGroups.FirstOrDefault(el => el.Name.ToLower() == param || el.Name.ToLower().StartsWith(param)) ?? ProcessGroups.FirstOrDefault(el => el.Id == int.Parse(param));
            }
            catch { }
            return result;
        }


        public bool AddProcessGroup(ProcessItemGroup item)
        {
            bool result = false;
            try
            {
                if (item == null)
                {
                    throw new ArgumentException("Item cannot be null");
                }
                if (ProcessGroups.FirstOrDefault(el => el.Id == item.Id) != null)
                {
                    throw new ArgumentException("A Process group with this Id already exists.");
                }
                if (ProcessGroups.FirstOrDefault(el => el.Name == item.Name) != null)
                {
                    throw new ArgumentException("A Process group with this Name already exists.");
                }

                ProcessGroups?.Add(item);
                Save();
                result = true;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public bool RemoveProcessGroup(ProcessItemGroup item)
        {
            bool? result = false;
            result = ProcessGroups?.Remove(item);
            Save();
            return result.Value;
        }
        public bool ChangeProcessGroup(ref ProcessItemGroup item, int? id, string name, List<ProcessItem> processes = null)
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
                    if (ProcessGroups.FirstOrDefault(el => el.Id == id.Value) != null)
                    {
                        throw new ArgumentException("A Process group with this Id already exists.");
                    }
                    item.Id = id.Value;
                }
                if (!string.IsNullOrWhiteSpace(name))
                {
                    if (ProcessGroups.FirstOrDefault(el => el.Name == name) != null)
                    {
                        throw new ArgumentException("A Process group with this Name already exists.");
                    }
                    item.Name = name;
                }
                if (processes != null && processes.Count > 0) { item.Processes = processes; }
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
            Save(ProcessGroups);
        }
    }
}
