namespace NMeasurement.Commons.Builder
{
    public interface IUnitBuilder
    {
    }

    public interface IUnitBuilderTerminator<out T> : IUnitBuilder where T : ICombinedUnit
    {
        T Build(string abbreviation = null);
    }
}