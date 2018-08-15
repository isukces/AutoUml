using System.Linq;
using AutoUml;
using Xunit;

namespace Tests
{
    public class DiagramTests : TestsBase
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
            Save(code);

            var expected = @"@startuml
title
 Diagram Test
end title

class Order #ff0000
{
}
class OrderItem
{
}

Order --{ OrderItem:Items
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

            var ent = diag.TryGetEntityByType(typeof(Order));
            ent.AddNote(NoteLocation.Top, "Sample note");

            var file = diag.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);
            // 1074296
            var expected = @"@startuml
title
 Diagram Test
end title

class Order #ff0000
{
}
note top of Order
Sample note
end note
class OrderItem
{
}

Order --{ OrderItem:Items
@enduml
";
            Assert.Equal(expected, code);
        }

        [Fact]
        public void T03_Should_create_simple_diagram_with_auto_entity_note()
        {
            var b = new ReflectionProjectBuilder(true)
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();
            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey("Test2"));
            var diag = b.Diagrams["Test2"];
            Assert.NotNull(diag);

            var file = diag.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);
            var expected = @"@startuml
title
 Diagram Test2
end title

class Order2
{
}
note right of Order2
Note from annotation
end note
class OrderItem2
{
}
note left of OrderItem2
Note from UmlNote
end note

Order2 --{ OrderItem2:Items
@enduml
";
            Assert.Equal(expected, code);
        }

        [Fact]
        public void T04_Should_create_simple_diagram_with_entity_note_with_background()
        {
            var b = new ReflectionProjectBuilder(true)
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();
            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey("Test"));
            var diag = b.Diagrams["Test"];
            Assert.NotNull(diag);

            var ent = diag.TryGetEntityByType(typeof(Order));
            ent.AddNote(NoteLocation.Top, "Sample note", UmlColor.IndianRed.ToFill());

            var file = diag.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);
            // 1074296
            var expected = @"@startuml
title
 Diagram Test
end title

class Order #ff0000
{
}
note top of Order #indianred
Sample note
end note
class OrderItem
{
}

Order --{ OrderItem:Items
@enduml
";
            Assert.Equal(expected, code);
        }

        [Fact]
        public void T05_Should_create_spot_with_background()
        {
            var b = new ReflectionProjectBuilder(true)
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();
            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey("Test"));
            var diag = b.Diagrams["Test"];
            Assert.NotNull(diag);

            var ent = diag.TryGetEntityByType(typeof(Order));
            ent.Spot = new UmlSpot
            {
                InCircle              = "X",
                CircleBackgroundColor = UmlColor.Blue
            };

            var file = diag.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);
            var expected = @"@startuml
title
 Diagram Test
end title

class Order << (X,#0000ff) >> #ff0000
{
}
class OrderItem
{
}

Order --{ OrderItem:Items
@enduml
";
            Assert.Equal(expected, code);
        }

        [Fact]
        public void T06_Should_create_note_on_relation()
        {
            var b = new ReflectionProjectBuilder(true)
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();
            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey("Test"));
            var diag = b.Diagrams["Test"];
            Assert.NotNull(diag);
            var rel = diag.Relations.Single();
            rel.Note           = "Note on rel";
            rel.NoteBackground = new GradientColorFill(UmlColor.Aqua, UmlColor.AliceBlue, GradientDirection.DownRight);

            var file = diag.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);

            var expected = @"@startuml
title
 Diagram Test
end title

class Order #ff0000
{
}
class OrderItem
{
}

Order --{ OrderItem:Items
note on link  #aqua/aliceblue
Note on rel
end note
@enduml
";
            Assert.Equal(expected, code);
        }


        [Fact]
        public void T07_Should_add_related_class_to_diagram()
        {
            var b = new ReflectionProjectBuilder(true)
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();
            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey("Test3"));
            var diagram = b.Diagrams["Test3"];
            Assert.NotNull(diagram);
            Assert.Equal(3, diagram.GetEntities().Count());

            var file = diagram.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);

            var expected = @"@startuml
title
 Diagram Test3
end title

class Order3
{
}
note right of Order3
Note from annotation
end note
class CompanyInfo
{
    string Name
}
class OrderItem3
{
}
note left of OrderItem3
Note from UmlNote
end note

Order3 o-right-> CompanyInfo:Customer
note on link  #ffe0f0
Note on relation
from UmlRelationAtribute
end note
Order3 *--{ OrderItem3:Items
@enduml
";
            Assert.Equal(expected, code);
        }


        [Fact]
        public void T08_Should_add_metadata_with_reflection()
        {
            var b = new ReflectionProjectBuilder(true)
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();
            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey("Test3"));
            var diagram = b.Diagrams["Test3"];
            Assert.NotNull(diagram);

            var x = diagram.TryGetEntityByType(typeof(Order3));
            Assert.Equal("world", x.TryGetStringMetadata("hello"));
            Assert.Null(x.TryGetStringMetadata("hello 2"));

            x = diagram.TryGetEntityByType(typeof(OrderItem3));
            Assert.Null(x.TryGetStringMetadata("hello"));
            Assert.Null(x.TryGetStringMetadata("hello 2"));
        }


        [Fact]
        public void T09_Should_add_sprite()
        {
            var b = new ReflectionProjectBuilder(true)
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();
            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey("Test"));
            var diag = b.Diagrams["Test"];
            Assert.NotNull(diag);

            diag.Sprites["test"] = new UmlSprite
            {
                Width     = 50,
                Height    = 100,
                GrayLevel = SpriteGrayLevels.Level8,
                Zipped    = true,
                Data =
                    @"xTH5ZiL034NHHzd_aflHglgMco5t6fsW7M3UcJW5yL0u6WlE0Esf-Fp7OAB7IA1FUP4jjimHxvFiUrUhpqqyzSXARDuKMIkF8SpI5un8viBuR07YSpiZr-Ex
1udm72ddBks43nEFqKvYIqxO3wES8nQ9cnot6y8aVk9qr6s8Ok8v9Mm5oo4F1N-cy4Pe9o2kHLX44nDNqHFD19HO9EaYzgd-z_ietoNCEXCk9Q76N2IEkHVK
UWwv5Kf7gk1AW8vxKObc0aeu4t0y54mq4r3CNbGo5107egQfeAE2QvHVbYD-QYsKVMi1NWXVtHav1J6dGlYlmiCHrn7N96dlV6JTbYXcRNED-PEVmiHlxXe
"
            };
            /*
            var rel = diag.Relations.Single();
            rel.Note           = "<$test>Note on rel";
            rel.NoteBackground = new GradientColorFill(UmlColor.Aqua, UmlColor.AliceBlue, GradientDirection.DownRight);
 */
            diag.GetEntities().First().Members.Add(new UmlTextMember(UmlSprite.MakeCode("test")));
            var file = diag.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);

            var expected = @"@startuml
title
 Diagram Test
end title
sprite $test [50x100/8z] {
xTH5ZiL034NHHzd_aflHglgMco5t6fsW7M3UcJW5yL0u6WlE0Esf-Fp7OAB7IA1FUP4jjimHxvFiUrUhpqqyzSXARDuKMIkF8SpI5un8viBuR07YSpiZr-Ex
1udm72ddBks43nEFqKvYIqxO3wES8nQ9cnot6y8aVk9qr6s8Ok8v9Mm5oo4F1N-cy4Pe9o2kHLX44nDNqHFD19HO9EaYzgd-z_ietoNCEXCk9Q76N2IEkHVK
UWwv5Kf7gk1AW8vxKObc0aeu4t0y54mq4r3CNbGo5107egQfeAE2QvHVbYD-QYsKVMi1NWXVtHav1J6dGlYlmiCHrn7N96dlV6JTbYXcRNED-PEVmiHlxXe
}

class Order #ff0000
{
    <$test>
}
class OrderItem
{
}

Order --{ OrderItem:Items
@enduml
";
            Assert.Equal(expected, code);
        }

        [Fact]
        public void T10_Should_create_packages()
        {
            var b = new ReflectionProjectBuilder(true)
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();
            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey("Test4"));
            var diagram = b.Diagrams["Test4"];
            Assert.NotNull(diagram);

            var file = diagram.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);

            var expected = @"@startuml
title
 Diagram Test4
end title

class CompanyInfo
{
    string Name
}
class OrderItem4Related1
{
}
abstract class OrderItem4Related3
{
    {abstract} int CalculateSum(int a,int b)
}
package Orders <<Cloud>> {
    class Order4
    {
    }
    class OrderItem4
    {
    }
}

Order4 --{ OrderItem4:Items
OrderItem4 o--> OrderItem4Related1:""DoSomething1(a)""
OrderItem4 o--> OrderItem4Related3:""DoSomething2()""
@enduml
";
            Assert.Equal(expected, code);
        }

        [Fact]
        public void T11_Should_convert_generics()
        {
            var b = new ReflectionProjectBuilder(true)
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();
            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey("Generics"));
            var diagram = b.Diagrams["Generics"];
            Assert.NotNull(diagram);

            var file = diagram.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);

            var expected = @"@startuml
title
 Diagram Generics
end title

class GenericBase
{
}
class ""Generic1<T>""
{
}
class ""Generic2<TModel,TElement>""
{
}
class NonGeneric
{
    Dictionary<string,double> GenericDic
}

""Generic1<T>"" -up-|> GenericBase
""Generic2<TModel,TElement>"" -up-|> ""Generic1<T>"":""T=TModel""
NonGeneric -up-|> ""Generic2<TModel,TElement>"":""TModel=int, TElement=string""
@enduml
";
            Assert.Equal(expected, code);
        }


        [Fact]
        public void T12_Should_mark_static_method()
        {
            var b = new ReflectionProjectBuilder(true)
                .UpdateVisitor<ClassMemberScannerVisitor>(a =>
                {
                    a.MethodsBindingFlags |= ReflectionFlags.Static;
                })
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();
            
            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey("Test4"));
            var diagram = b.Diagrams["Test4"];
            Assert.NotNull(diagram);

            var file = diagram.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);

            var expected = @"@startuml
title
 Diagram Test4
end title

class CompanyInfo
{
    string Name
}
class OrderItem4Related1
{
}
abstract class OrderItem4Related3
{
    {abstract} int CalculateSum(int a,int b)
}
package Orders <<Cloud>> {
    class Order4
    {
    }
    class OrderItem4
    {
        {static} void SomeStaticMethod()
    }
}

Order4 --{ OrderItem4:Items
OrderItem4 o--> OrderItem4Related1:""DoSomething1(a)""
OrderItem4 o--> OrderItem4Related3:""DoSomething2()""
@enduml
";
            Assert.Equal(expected, code);
        }
    }
}