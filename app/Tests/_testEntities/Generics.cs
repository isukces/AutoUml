using System.Collections.Generic;
using AutoUml;

namespace Tests;

[UmlDiagram("Generics")]
public class GenericBase
{
}

[UmlDiagram("Generics")]
public class Generic1<T> : GenericBase
{
}

[UmlDiagram("Generics")]
public class Generic2<TModel, TElement> : Generic1<TModel>
{
}

[UmlDiagram("Generics")]
public class NonGeneric : Generic2<int, string>
{
    public Dictionary<string, double> GenericDic { get; set; }
}