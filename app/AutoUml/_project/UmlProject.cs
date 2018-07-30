using System;
using System.Collections.Generic;

namespace AutoUml
{
    public class UmlProject
    {
        public void GenerateAll(Func<UmlProjectDiagram, string> filenameFactory)
        {
            foreach (var i in Diagrams)
            {
                var fileName = filenameFactory(i.Value);
                i.Value.GenerateFile(fileName);
            }
        }

        public UmlProjectDiagram GetOrCreateDiagram(string diagramName)
        {
            if (Diagrams.TryGetValue(diagramName, out var x)) return x;
            Diagrams[diagramName] = x = new UmlProjectDiagram
            {
                Name  = diagramName,
                Title = "Diagram " + diagramName
            };
            x.OnAddTypeToDiagram += XOnOnAddTypeToDiagram;
            {
                var handler = OnAddDiagram;
                if (handler != null)
                    handler.Invoke(this, new AddDiagramEventArgs
                    {
                        Diagram = x
                    });
            }
            return x;
        }

        private void XOnOnAddTypeToDiagram(object sender, AddTypeToDiagramEventArgs e)
        {
            var handler = OnAddTypeToDiagram;
            if (handler == null) return;
            handler(this, e);
        }

        public Dictionary<string, UmlProjectDiagram> Diagrams { get; } =
            new Dictionary<string, UmlProjectDiagram>();

        public event EventHandler<AddTypeToDiagramEventArgs> OnAddTypeToDiagram;
        public event EventHandler<AddDiagramEventArgs>       OnAddDiagram;
    }
}