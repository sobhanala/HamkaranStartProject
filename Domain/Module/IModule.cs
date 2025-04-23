using System.Windows.Forms;

namespace Domain.Module
{
    public interface IModule
    {
        string Name { get; }
        string Description { get; }
        Form CreateForm();
    }
}