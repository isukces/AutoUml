using System.Reflection;

namespace AutoUml
{
    public interface IAssemblyVisitor
    {
        void Visit(Assembly assembly, UmlDiagram diagram);        
    }
}