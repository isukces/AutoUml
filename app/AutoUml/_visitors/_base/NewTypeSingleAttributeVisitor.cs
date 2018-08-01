using System;
using System.Reflection;
using JetBrains.Annotations;

namespace AutoUml
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class NewTypeSingleAttributeVisitor<T> : INewTypeInDiagramVisitor
        where T : Attribute
    {
        public void Visit(UmlProjectDiagram diagram, UmlEntity info)
        {
            var att = info.Type.GetCustomAttribute<T>();
            if (att == null)
                return;
            VisitInternal(diagram, info, att);
        }

        protected abstract void VisitInternal(UmlProjectDiagram diagram, UmlEntity info, [NotNull] T att);
    }
}