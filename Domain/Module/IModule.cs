using System.Collections.Generic;
using System.Windows.Forms;

namespace Domain.Module
{
    public interface IModule
    {

        string Name { get; }
        string Description { get; }
        Form GetMainForm();  
        void Initialize();
        int Id { get; } // new property


        //IEnumerable<ISubModule> GetSubModules();

    }
}