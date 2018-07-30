using System;

namespace AutoUml
{
    public class ActionDiagramCreatedVisitor : IDiagramVisitor
    {
        public ActionDiagramCreatedVisitor(Action<UmlProjectDiagram> action)
        {
            _action = action;
        }

        public void VisitBeforeEmit(UmlProjectDiagram diagram)
        {
        }

        public void VisitDiagramCreated(UmlProjectDiagram diagram)
        {
            if (_action != null)
                _action(diagram);
        }

        private readonly Action<UmlProjectDiagram> _action;
    }
}