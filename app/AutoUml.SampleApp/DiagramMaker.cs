namespace AutoUml.SampleApp;

internal class DiagramMaker
{
    public static void MakeDiagrams()
    {
        var rh = new ReflectionProjectBuilder(true)
            .WithAssembly(typeof(DiagramMaker).Assembly);

        var b = rh.Build();
    }
}