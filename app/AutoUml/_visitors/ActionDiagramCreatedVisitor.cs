using System;

namespace AutoUml;

public class ActionDiagramCreatedVisitor : IDiagramVisitor
{
    public ActionDiagramCreatedVisitor(Action<UmlDiagram> action)
    {
        _action = action;
    }

    public void VisitBeforeEmit(UmlDiagram diagram)
    {
    }

    public void VisitDiagramCreated(UmlDiagram diagram)
    {
        if (_action != null)
            _action(diagram);
    }

    private readonly Action<UmlDiagram> _action;
}