using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManager.BL.Controller
{
    internal class SerializableSaver : IDataSaver
    {
        private readonly string formatFile = ".dat";

        public void Save<T>(List<T> item) where T : class
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string file = typeof(T).Name + formatFile;

            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, item);
            }
        }
        public List<T> Load<T>() where T : class
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string file = typeof(T).Name + formatFile;

            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate))
            {
                if (fs.Length > 0 && formatter.Deserialize(fs) is List<T> items)
                {
                    return items;
                }
                else
                {
                    return new List<T>();
                }
            }
        }
    }
}
