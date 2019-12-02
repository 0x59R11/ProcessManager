using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessManager.BL.Controller
{
    public abstract class ControllerBase
    {
        private readonly IDataSaver manager = new SerializableSaver();


        protected virtual void Save<T>(List<T> item) where T : class
        {
            manager.Save(item);
        }
        protected virtual List<T> Load<T>() where T : class
        {
            return manager.Load<T>();
        }
    }
}
