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
                var umlPackageName = new UmlPackageName(attribute.PackageName);
                if (!packages.TryGetValue(umlPackageName, out var x))
                    packages[umlPackageName] = x = new UmlPackage();
                x.Kind = attribute.Kind;
            }
        }
    }
}