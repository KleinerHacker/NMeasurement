namespace NMeasurement.Types.Energies.Interfaces
{
    public interface IEnergy<in T> : IMeasurement<T, IPrefix> where T : IEnergyUnitBase 
    {
    }
}