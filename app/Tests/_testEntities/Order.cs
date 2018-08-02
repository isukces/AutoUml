using System.Collections.Generic;
using AutoUml;

namespace Tests
{
    [UmlDiagram("Test", BackgroundColor = "ff0000")]
    public class Order
    {
        public List<OrderItem> Items { get; set; }
    }

    [UmlDiagram("Test")]
    public class OrderItem
    {
    }
}