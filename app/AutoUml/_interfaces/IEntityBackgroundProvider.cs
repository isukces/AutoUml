namespace AutoUml;

public interface IEntityBackgroundProvider
{
    IUmlFill? GetEntityBackground();
}

public interface IUmlFill
{
    string GetCode(bool convertToRgb = false);
}

public static class UmlFillExt
{
    extension(IUmlFill? fill)
    {
        public string GetCodeWithSpace(bool convertToRgb = false)
        {
            var code = fill?.GetCode(convertToRgb);
            return string.IsNullOrEmpty(code) ? string.Empty : " " + code;
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(fill?.GetCode());
        }
    }
}

public interface INoteProvider
{
    IUmlFill? GetNoteBackground();
    string? GetNoteText();
}

public interface INoteWithLocationProvider : INoteProvider
{
    NoteLocation GetNoteLocation();
}
