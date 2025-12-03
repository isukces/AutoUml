using System;
using AutoUml;

namespace Tests;

[UmlDiagram("Test14")]
public interface ITopInterface14
{
    string Name         { get; }
    [UmlRelation(ForceAddToDiagram = true,ArrowDirection = UmlArrowDirections.Up)]
    Info14 CreationInfo { get; }
}

[UmlDiagram("Test14")]
public interface INestedInterface14 : ITopInterface14
{
    int Count { get; }
}

[UmlDiagram("Test14")]
public class Class14 : INestedInterface14
{
    public int      Count        { get; }
    public string   Name         { get; }
    public Info14   CreationInfo { get; }
    public ExInfo14 ExInfo       { get; set; }
}


[UmlDiagram("Test14")]
public class DerivedClass14 : Class14
{
}


    
public class Info14
{
    public DateTime Created { get; set; }
}
 
    
public class ExInfo14
{
    public int SomeNumber { get; set; }
}