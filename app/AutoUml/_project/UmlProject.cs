using System;
using System.Collections.Generic;

namespace AutoUml;

public class UmlProject
{
    public IReadOnlyList<SavedFileInfo> GenerateAll(Func<UmlDiagram, string> filenameFactory)
    {
        var result = new List<SavedFileInfo>(Diagrams.Count);
        foreach (var i in Diagrams)
        {
            var fileName = filenameFactory(i.Value);
            var wasSaved = i.Value.SaveToFile(fileName);
            result.Add(new SavedFileInfo
            {
                FileName = fileName,
                WasSaved = wasSaved,
                Diagram  = i.Value
            });
        }

        return result;
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

    private void XOnOnAddTypeToDiagram(object? sender, AddTypeToDiagramEventArgs e)
    {
        var handler = OnAddTypeToDiagram;
        if (handler == null) return;
        handler(this, e);
    }

    public Dictionary<string, UmlDiagram> Diagrams { get; } = new();

    public event EventHandler<AddDiagramEventArgs>? OnAddDiagram;

    public event EventHandler<AddTypeToDiagramEventArgs>? OnAddTypeToDiagram;

    public struct SavedFileInfo
    {
        public UmlDiagram Diagram  { get; set; }
        public string     FileName { get; set; }
        public bool       WasSaved { get; set; }
    }
}
