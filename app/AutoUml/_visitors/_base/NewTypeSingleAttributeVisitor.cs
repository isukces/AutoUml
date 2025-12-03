using System;
using System.Reflection;

namespace AutoUml;

/// <summary>
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class NewTypeSingleAttributeVisitor<T> : INewTypeInDiagramVisitor
    where T : Attribute
{
    public void Visit(UmlDiagram diagram, UmlEntity info)
    {
        var att = info.Type.GetCustomAttribute<T>();
        if (att == null)
            return;
        VisitInternal(diagram, info, att);
    }

    protected abstract void VisitInternal(UmlDiagram diagram, UmlEntity info, T att);
}