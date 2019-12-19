using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoUml
{
    public class ReflectionProjectBuilder
    {
        public ReflectionProjectBuilder(bool addStandardVisitors)
        {
            if (addStandardVisitors)
                WithStandardVisitors();
        }

        public UmlProject Build()
        {
            var result = _project;
            _project = null;
            if (result == null)
                throw new Exception("Project already built");
            foreach (var diagram in result.Diagrams.Values)
            foreach (var i in DiagramVisitors)
                i.VisitBeforeEmit(diagram);
            return result;
        }

        public ReflectionProjectBuilder UpdateVisitor<T>(Action<T> updateAction)
            where T : IUmlVisitor
        {
            if (updateAction == null)
                return this;
            foreach (var element in NewTypeVisitors)
                if (element is T t)
                    updateAction(t);
            foreach (var element in DiagramVisitors)
                if (element is T t)
                    updateAction(t);
            return this;
        }

        public ReflectionProjectBuilder VisitType(Type type)
        {
            VisitTypeInternal(type, true);
            return this;
        }

        public ReflectionProjectBuilder WithAssembly(Assembly assembly)
        {
            if (!_scannedAssemblies.Add(assembly))
                return this;
            _project.OnAddTypeToDiagram += ProjectOnOnAddTypeToDiagram;
            _project.OnAddDiagram       += ProjectOnOnAddDiagram;
            foreach (var type in assembly.GetTypes())
                VisitTypeInternal(type, false);
            _project.OnAddTypeToDiagram -= ProjectOnOnAddTypeToDiagram;
            _project.OnAddDiagram       -= ProjectOnOnAddDiagram;
            return this;
        }

        public ReflectionProjectBuilder WithNewDiagramSettings(Action<UmlDiagram> diagramUpdateAction)
        {
            if (diagramUpdateAction != null)
            {
                var visitor = new ActionDiagramCreatedVisitor(diagramUpdateAction);
                DiagramVisitors.Add(visitor);
            }

            return this;
        }

        public ReflectionProjectBuilder WithoutVisitor<T>()
            where T : IUmlVisitor
        {
            NewTypeVisitors.DeleteFromListIf(i => i.GetType().IsInstanceOfType(typeof(T)));
            DiagramVisitors.DeleteFromListIf(i => i.GetType().IsInstanceOfType(typeof(T)));
            return this;
        }


        public ReflectionProjectBuilder WithStandardVisitors()
        {
            ReflectionTypeVisitors.Add(new UmlDiagramAttributeVisitor());
            NewTypeVisitors.Add(new StructSpotVisitor(UmlColor.Empty));
            NewTypeVisitors.Add(new UmlAddRelationAttributeVisitor());
            NewTypeVisitors.Add(new UmlNoteAttributeVisitor());
            NewTypeVisitors.Add(new UmlPackageAttributeVisitor());
            NewTypeVisitors.Add(new ClassMemberScannerVisitor());
            NewTypeVisitors.Add(new ForceAddToDiagramVisitor());
            NewTypeVisitors.Add(new UmlAddImplementedInterfacesToDiagramAttributeVisitor());
            NewTypeVisitors.Add(new AddTypesToDiagramVisitor());

            DiagramVisitors.Add(new MemberToRelationVisitor());
            DiagramVisitors.Add(new HideTrivialMethodsVisitor());
            DiagramVisitors.Add(new AddInheritRelationVisitor());
            DiagramVisitors.Add(new UmlAddMetaAttributeVisitor());

            AssemblyVisitors.Add(new UmlPackageStyleAttributeVisitor());

            return this;
        }


        public ReflectionProjectBuilder WithVisitor(INewTypeInDiagramVisitor v, bool addFirst = false)
        {
            if (addFirst && NewTypeVisitors.Any())
                NewTypeVisitors.Insert(0, v);
            else
                NewTypeVisitors.Add(v);
            return this;
        }

        public ReflectionProjectBuilder WithVisitor(IDiagramVisitor v, bool addFirst = false)
        {
            if (addFirst && DiagramVisitors.Any())
                DiagramVisitors.Insert(0, v);
            else
                DiagramVisitors.Add(v);
            return this;
        }

        private void ProjectOnOnAddDiagram(object sender, AddDiagramEventArgs e)
        {
            foreach (var a in _scannedAssemblies)
            {
                foreach (var v in AssemblyVisitors)
                    v.Visit(a, e.Diagram);
            }

            foreach (var i in DiagramVisitors)
                i.VisitDiagramCreated(e.Diagram);
        }

        private void ProjectOnOnAddTypeToDiagram(object sender, AddTypeToDiagramEventArgs e)
        {
            foreach (var v in NewTypeVisitors)
                v.Visit(e.Diagram, e.Info);
        }

        private void VisitTypeInternal(Type type, bool addEvents)
        {
            if (!_scannedTypes.Add(type))
                return;
            if (addEvents)
            {
                _project.OnAddTypeToDiagram += ProjectOnOnAddTypeToDiagram;
                _project.OnAddDiagram       += ProjectOnOnAddDiagram;
            }

            foreach (var visitor in ReflectionTypeVisitors)
                visitor.Visit(type, _project);
            if (addEvents)
            {
                _project.OnAddTypeToDiagram -= ProjectOnOnAddTypeToDiagram;
                _project.OnAddDiagram       -= ProjectOnOnAddDiagram;
            }
        }


        public List<IReflectionTypeVisitor>   ReflectionTypeVisitors { get; } = new List<IReflectionTypeVisitor>();
        public List<INewTypeInDiagramVisitor> NewTypeVisitors        { get; } = new List<INewTypeInDiagramVisitor>();
        public List<IDiagramVisitor>          DiagramVisitors        { get; } = new List<IDiagramVisitor>();
        public List<IAssemblyVisitor>         AssemblyVisitors       { get; } = new List<IAssemblyVisitor>();

        private UmlProject _project = new UmlProject();
        private readonly HashSet<Assembly> _scannedAssemblies = new HashSet<Assembly>();
        private readonly HashSet<Type> _scannedTypes = new HashSet<Type>();
    }
}