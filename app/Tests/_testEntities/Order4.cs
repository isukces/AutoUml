using System.Collections.Generic;
using AutoUml;

[assembly: UmlPackageStyle("Orders", UmlPackageKind.Cloud)]

namespace Tests
{
    [UmlDiagram("Test4")]
    [UmlPackage("Orders")]
    public class Order4
    {
        public List<OrderItem4> Items { get; set; }
    }

    [UmlPackage("Orders")]
    [UmlDiagram("Test4")]
    public class OrderItem4
    {
    }
}