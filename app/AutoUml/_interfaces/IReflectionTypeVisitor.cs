using System;

namespace AutoUml;

public interface IReflectionTypeVisitor
{
    void Visit(Type type, UmlProject umlProject);
}