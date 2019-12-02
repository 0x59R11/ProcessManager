using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManager.BL.Model
{
    [Serializable]
    public class ProcessItemGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProcessItem> Processes { get; set; }


        public ProcessItemGroup() { }
        public ProcessItemGroup(int id, string name, List<ProcessItem> processes = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null");
            }

            Id = id;
            Name = name;
            Processes = processes ?? new List<ProcessItem>();
        }


        public override string ToString()
        {
            return $"[{Id}]  {Name}  - [{Processes.Count}] [{(string.Join(", ", Processes.Select(el => el.Name)))}]";
        }
    }
}
