namespace AutoUml;

public interface IEntityBackgroundProvider
{
    IUmlFill GetEntityBackground();
}

public interface IUmlFill
{
    string GetCode(bool convertToRgb = false);
}

public static class UmlFillExt
{
    public static string GetCodeWithSpace(this IUmlFill? fill, bool convertToRgb = false)
    {
        var code = fill?.GetCode(convertToRgb);
        return string.IsNullOrEmpty(code) ? string.Empty : " " + code;
    }

    public static bool IsEmpty(this IUmlFill? fill)
    {
        return string.IsNullOrEmpty(fill?.GetCode());
    }
}

public interface INoteProvider
{
    IUmlFill GetNoteBackground();
    string GetNoteText();
}

public interface INoteWithLocationProvider : INoteProvider
{
    NoteLocation GetNoteLocation();
}