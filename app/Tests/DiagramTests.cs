using System.Collections.Generic;
using AutoUml;
using Xunit;

namespace Tests
{
    public class DiagramTests
    {
        [Fact]
        public void T01_Should_create_simple_diagram()
        {
            var b = new ReflectionProjectBuilder(true)
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();
            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey("Test"));
            var diag = b.Diagrams["Test"];
            Assert.NotNull(diag);

            var file = diag.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            var expected = @"@startuml
title
 Diagram Test
end title

class Order
{
}
class OrderItem
{
}

Order --{ OrderItem:""Items""
@enduml
";
            Assert.Equal(expected, code);            
        }
        
        
        [Fact]
        public void T02_Should_create_simple_diagram_with_entity_note()
        {
            var b = new ReflectionProjectBuilder(true)
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();
            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey("Test"));
            var diag = b.Diagrams["Test"];
            Assert.NotNull(diag);

            var ent = diag.GetEntityByType(typeof(Order));
            ent.AddNote(NoteLocation.Top, "Sample note");

            var file = diag.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            var expected = @"@startuml
title
 Diagram Test
end title

class Order
{
}
note top of Order
Sample note
end note
class OrderItem
{
}

Order --{ OrderItem:""Items""
@enduml
";
            Assert.Equal(expected, code);            
        }
    }


    [UmlDiagram("Test")]
    public class Order
    {
        public List<OrderItem> Items { get; set; }
    }

    [UmlDiagram("Test")]
    public class OrderItem
    {
    }
}