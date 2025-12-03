using System;
using System.Diagnostics;

namespace AutoUml;

[Conditional("AUTOUML_ANNOTATIONS")]
public class DontConvertToRelationAttribute : Attribute
{
}
