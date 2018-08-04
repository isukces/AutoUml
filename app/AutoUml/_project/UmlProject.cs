using System;
using System.Collections.Generic;

namespace AutoUml
{
    public class UmlProject
    {
        public void GenerateAll(Func<UmlDiagram, string> filenameFactory)
        {
            foreach (var i in Diagrams)
            {
                var fileName = filenameFactory(i.Value);
                i.Value.SaveToFile(fileName);
            }
        }

        public UmlDiagram GetOrCreateDiagram(string diagramName)
        {
            if (Diagrams.TryGetValue(diagramName, out var x)) return x;
            Diagrams[diagramName] = x = new UmlDiagram
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

        public Dictionary<string, UmlDiagram> Diagrams { get; } =
            new Dictionary<string, UmlDiagram>();

        public event EventHandler<AddTypeToDiagramEventArgs> OnAddTypeToDiagram;
        public event EventHandler<AddDiagramEventArgs>       OnAddDiagram;
    }
}