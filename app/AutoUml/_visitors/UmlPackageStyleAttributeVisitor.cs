using System;
using System.Reflection;

namespace AutoUml
{
    public class UmlPackageStyleAttributeVisitor : IAssemblyVisitor
    {
        public void Visit(Assembly assembly, UmlDiagram diagram)
        {
            var packages = diagram.Packages;
            foreach (var attribute in assembly.GetCustomAttributes<UmlPackageStyleAttribute>())
            {
                if (!attribute.CanBeUsedFor(diagram))
                    continue;
                if (!packages.TryGetValue(attribute.PackageName, out var x))
                    packages[attribute.PackageName] = x = new UmlPackage();
                x.Kind = attribute.Kind;
            }
        }
    }
}