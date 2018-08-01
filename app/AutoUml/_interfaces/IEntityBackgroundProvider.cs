namespace AutoUml
{
    public interface IEntityBackgroundProvider
    {
        IUmlFill GetEntityBackground();
    }

    public interface IUmlFill
    {
        string GetCode();
    }


    public interface INoteProvider
    {
        string GetNoteText();
        NoteLocation? GetNoteLocation();
        IUmlFill GetNoteBackground();
    }
}