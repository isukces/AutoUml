namespace AutoUml
{
    public struct SolidColorFill : IUmlFill
    {
        public SolidColorFill(UmlColor color)
        {
            _color = color;
        }

        public string GetCode(bool convertToRgb)
        {
            if (_color.IsEmpty)
                return "";
            return convertToRgb ? _color.PlantUmlRgbCode : _color.PlantUmlCode;
        }

        private readonly UmlColor _color;
    }
}