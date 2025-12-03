using System;
using System.Diagnostics;

namespace AutoUml;

[AttributeUsage(AttributesConsts.Entities)]
[Conditional("AUTOUML_ANNOTATIONS")]
public sealed class UmlAddImplementedInterfacesToDiagramAttribute : Attribute
{
}
