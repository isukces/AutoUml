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
        [UmlRelation(ForceAddToDiagram = true)]
        public OrderItem4Related1 DoSomething1(int a)
        {
            return null;
        }

        [UmlRelation(ForceType = typeof(OrderItem4Related3), ForceAddToDiagram = true)]
        public OrderItem4Related2 DoSomething2()
        {
            return null;
        }
    }

    public class OrderItem4Related1
    {
        
    }
    public class OrderItem4Related2
    {
        
    }
    public class OrderItem4Related3
    {
        
    }
}