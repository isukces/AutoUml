using System;
using System.Reflection;

namespace AutoUml
{
    /// <summary>
    ///     Add class decorated with UmlDiagramAttribute to diagram
    /// </summary>
    public class UmlDiagramAttributeVisitor : IReflectionTypeVisitor
    {
        public void Visit(Type type, UmlProject umlProject)
        {
            foreach (var att in type.GetCustomAttributes<UmlDiagramAttribute>(false))
            {
                var diagram = umlProject.GetOrCreateDiagram(att.DiagramName);
                diagram.UpdateTypeInfo(type,
                    (info, created) => { info.BgColor = UmlColor.FromString(att.BackgroundColor); });
            }
        }
    }
}