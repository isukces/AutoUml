namespace AutoUml
{
    public struct SolidColorFill : IUmlFill
    {
        public SolidColorFill(UmlColor color)
        {
            _color = color;
        }

        public string GetCode()
        {
            return _color.PlantUmlCode;
        }

        private readonly UmlColor _color;
    }
   
}