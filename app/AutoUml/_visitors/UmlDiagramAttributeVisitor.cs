using System;
using System.Reflection;

namespace AutoUml;

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
                (info, created) =>
                {
                    var background = (att as IEntityBackgroundProvider).GetEntityBackground();
                    if (background != null)
                        info.Background = background;
                    info.AddNote(att);
                });
        }
    }
}
