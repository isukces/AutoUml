using System;
using AutoUml;

namespace Tests;

[UmlDiagram("Test15")]
public class Info15A
{
    public DateTime Created { get; set; }
}
    
[UmlDiagram("Test15")]
public class Info15B
{
    public int Count { get; set; }
}

    
[UmlDiagram("Test15")]
public interface Interface15A
{
    Info15A CreationInfo { get; }
    Info15B InfoB        { get; set; }
}
    
[UmlDiagram("Test15")]
public interface Interface15B
{
    Info15B InfoB { get; set; }
}
    
[UmlDiagram("Test15")]
public interface Interface15C:Interface15A
{
}
    
    
[UmlDiagram("Test15")]
public interface Interface15D: Interface15B,Interface15C
{
}
 
[UmlDiagram("Test15")]
public class Class15 : Interface15D
{
    public Info15A CreationInfo { get; }
    public Info15B InfoB        { get; set; }
}