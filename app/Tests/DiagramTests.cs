using System.Collections.Generic;
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
    +string Name
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
Order3 *-[#yellow]up-|> CompanyInfo:Customer2
Order3 *-----{ OrderItem3:Items
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
            
            
            var file = diagram.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);
            string expected=@"@startuml
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
    +string Name
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
Order3 *-[#yellow]up-|> CompanyInfo:Customer2
Order3 *-----{ OrderItem3:Items
@enduml
";
            Assert.Equal(expected, code);
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
    +string Name
}
class OrderItem4Related1
{
    +int SomeMethodWithNestedMethods()
}
abstract class OrderItem4Related3
{
    +{abstract} int CalculateSum(int a,int b)
}
package Orders <<Cloud>> {
    class Order4
    {
    }
    class OrderItem4
    {
        +{static} void SomeStaticMethod()
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
            const string diagramName = "Generics";
            Assert.True(b.Diagrams.ContainsKey(diagramName));
            var diagram = b.Diagrams[diagramName];
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
    +Dictionary<string,double> GenericDic
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
                .UpdateVisitor<ClassMemberScannerVisitor>(a => { a.ScanFlags |= ReflectionFlags.StaticMethod; })
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();

            Assert.NotNull(b);
            const string diagramName = "Test4";
            Assert.True(b.Diagrams.ContainsKey(diagramName));
            var diagram = b.Diagrams[diagramName];
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
    +string Name
}
class OrderItem4Related1
{
    +int SomeMethodWithNestedMethods()
}
abstract class OrderItem4Related3
{
    +{abstract} int CalculateSum(int a,int b)
}
package Orders <<Cloud>> {
    class Order4
    {
    }
    class OrderItem4
    {
        +{static} void SomeStaticMethod()
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
        public void T13_Should_do_not_convert_collections()
        {
            const string diagramName = "Test13";
            var b = new ReflectionProjectBuilder(true)
                .UpdateVisitor<ClassMemberScannerVisitor>(a => { a.ScanFlags |= ReflectionFlags.StaticMethod; })
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();

            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey(diagramName));
            var diagram = b.Diagrams[diagramName];
            Assert.NotNull(diagram);

            var file = diagram.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);

            var expected = @"@startuml
title
 Diagram Test13
end title

class AttributesListOwner
{
}
class AttributesList
{
    +int Capacity
    +int Count
    +void Add(AttributesListItem item)
    +void AddRange(IEnumerable<AttributesListItem> collection)
    +ReadOnlyCollection<AttributesListItem> AsReadOnly()
    +int BinarySearch(int index,int count,AttributesListItem item,IComparer<AttributesListItem> comparer)
    +int BinarySearch(AttributesListItem item)
    +int BinarySearch(AttributesListItem item,IComparer<AttributesListItem> comparer)
    +void Clear()
    +bool Contains(AttributesListItem item)
    +List<TOutput> ConvertAll(Converter<AttributesListItem,TOutput> converter)
    +void CopyTo(AttributesListItem[] array)
    +void CopyTo(int index,AttributesListItem[] array,int arrayIndex,int count)
    +void CopyTo(AttributesListItem[] array,int arrayIndex)
    +bool Exists(Predicate<AttributesListItem> match)
    +AttributesListItem Find(Predicate<AttributesListItem> match)
    +List<AttributesListItem> FindAll(Predicate<AttributesListItem> match)
    +int FindIndex(Predicate<AttributesListItem> match)
    +int FindIndex(int startIndex,Predicate<AttributesListItem> match)
    +int FindIndex(int startIndex,int count,Predicate<AttributesListItem> match)
    +AttributesListItem FindLast(Predicate<AttributesListItem> match)
    +int FindLastIndex(Predicate<AttributesListItem> match)
    +int FindLastIndex(int startIndex,Predicate<AttributesListItem> match)
    +int FindLastIndex(int startIndex,int count,Predicate<AttributesListItem> match)
    +void ForEach(Action<AttributesListItem> action)
    +Enumerator<AttributesListItem> GetEnumerator()
    +List<AttributesListItem> GetRange(int index,int count)
    +int IndexOf(AttributesListItem item)
    +int IndexOf(AttributesListItem item,int index)
    +int IndexOf(AttributesListItem item,int index,int count)
    +void Insert(int index,AttributesListItem item)
    +void InsertRange(int index,IEnumerable<AttributesListItem> collection)
    +int LastIndexOf(AttributesListItem item)
    +int LastIndexOf(AttributesListItem item,int index)
    +int LastIndexOf(AttributesListItem item,int index,int count)
    +bool Remove(AttributesListItem item)
    +int RemoveAll(Predicate<AttributesListItem> match)
    +void RemoveAt(int index)
    +void RemoveRange(int index,int count)
    +void Reverse()
    +void Reverse(int index,int count)
    +void Sort()
    +void Sort(IComparer<AttributesListItem> comparer)
    +void Sort(int index,int count,IComparer<AttributesListItem> comparer)
    +void Sort(Comparison<AttributesListItem> comparison)
    +AttributesListItem[] ToArray()
    +void TrimExcess()
    +bool TrueForAll(Predicate<AttributesListItem> match)
}
class AttributesListItem
{
    +string Name
}

AttributesListOwner --{ AttributesListItem:Attributes
AttributesListOwner o--> AttributesList:Attributes2
AttributesList --> AttributesListItem:Item
@enduml
";
            Assert.Equal(expected, code);
        }
        
        
        [Fact]
        public void T14_Should_do_not_add_relation_to_base_interface()
        {
            const string diagramName = "Test14";
            var b = new ReflectionProjectBuilder(true)
                .UpdateVisitor<ClassMemberScannerVisitor>(a =>
                {
                    a.ScanFlags |= ReflectionFlags.StaticMethod;
                })
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();

            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey(diagramName));
            var diagram = b.Diagrams[diagramName];
            Assert.NotNull(diagram);

            var file = diagram.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);

            var expected = @"@startuml
title
 Diagram Test14
end title

interface ITopInterface14
{
    +string Name
}
class Info14
{
    +DateTime Created
}
interface INestedInterface14
{
    +int Count
}
class Class14
{
    +int Count
    +string Name
    +Info14 CreationInfo
    +ExInfo14 ExInfo
}
class DerivedClass14
{
}

ITopInterface14 o-up-> Info14:CreationInfo
INestedInterface14 -up-|> ITopInterface14
Class14 -up-|> INestedInterface14
DerivedClass14 -up-|> Class14
@enduml
";
            Assert.Equal(expected, code);
        }
        
            
        [Fact]
        public void T15_Should_do_not_add_relation_property_if_declaring_interface_is_on_diagram()
        {
            const string diagramName = "Test15";
            var b = new ReflectionProjectBuilder(true)
                .UpdateVisitor<ClassMemberScannerVisitor>(a =>
                {
                    a.ScanFlags |= ReflectionFlags.StaticMethod;
                })
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();

            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey(diagramName));
            var diagram = b.Diagrams[diagramName];
            Assert.NotNull(diagram);

            var file = diagram.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);

            var expected = @"@startuml
title
 Diagram Test15
end title

class Info15A
{
    +DateTime Created
}
class Info15B
{
    +int Count
}
interface Interface15A
{
}
interface Interface15B
{
}
interface Interface15C
{
}
interface Interface15D
{
}
class Class15
{
    +Info15A CreationInfo
    +Info15B InfoB
}

Interface15A --> Info15A:CreationInfo
Interface15A --> Info15B:InfoB
Interface15B --> Info15B:InfoB
Interface15C -up-|> Interface15A
Interface15D -up-|> Interface15B
Interface15D -up-|> Interface15C
Class15 -up-|> Interface15D
@enduml
";
            Assert.Equal(expected, code);
        }
        
        
        [Fact]
        public void T16_Should_add_starting_lines()
        {
            const string diagramName = "Test16";
            var b = new ReflectionProjectBuilder(true)
                .UpdateVisitor<ClassMemberScannerVisitor>(a =>
                {
                    a.ScanFlags |= ReflectionFlags.StaticMethod;
                })
                .WithAssembly(typeof(DiagramTests).Assembly)
                .Build();

            Assert.NotNull(b);
            Assert.True(b.Diagrams.ContainsKey(diagramName));
            var diagram = b.Diagrams[diagramName];
            Assert.NotNull(diagram);

            var file = diagram.CreateFile();
            Assert.NotNull(file);
            var code = file.Code;
            Save(code);

            var expected = @"@startuml
title
 Diagram Test16
end title

class Info16A
{
    Line 1
    Line 2
    ~~Line 3~~
    ==
    +DateTime Created
}

@enduml
";
            Assert.Equal(expected, code);
        }
    }
}