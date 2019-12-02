using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManager.BL.Model
{
    [Serializable]
    public class ProcessItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Directory { get; set; }
        public string Description { get; set; }


        public ProcessItem() { }
        public ProcessItem(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty!");
            }

            Id = id;
            Name = name;
        }
        public ProcessItem(int id, string name, string directory, string description = "")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty!");
            }
            if (string.IsNullOrWhiteSpace(directory))
            {
                throw new ArgumentException("Directory cannot be empty!");
            }

            Id = id;
            Name = name;
            Directory = directory;
            Description = description;
        }



        public override string ToString()
        {
            return $"[{Id}]  {Name} - [{(string.IsNullOrWhiteSpace(Description) ? "null" : Description)}]";
        }
    }
}
