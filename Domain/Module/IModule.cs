using System.Windows.Forms;

namespace Domain.Module
{
    public interface IModule
    {

        string Name { get; }
        string Subname { get; }
        Form GetMainForm();
        void Initialize();
        int Id { get; }


        //IEnumerable<ISubModule> GetSubModules();

    }
}