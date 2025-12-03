using System.Collections.Generic;
using AutoUml;

namespace Tests;

[UmlDiagram("Test13")]
public class AttributesListOwner
{
    public AttributesList Attributes { get; set; }

    [UmlRelation(DoNotResolveCollections = true)]
    public AttributesList Attributes2 { get; set; }
}

[UmlDiagram("Test13")]
public class AttributesList : List<AttributesListItem>
{
}

[UmlDiagram("Test13")]
public class AttributesListItem
{
    public string Name { get; set; }
}