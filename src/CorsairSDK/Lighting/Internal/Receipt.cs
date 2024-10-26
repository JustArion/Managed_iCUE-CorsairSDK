namespace Corsair.Lighting.Internal;

public struct Receipt<T> where T : notnull
{
    internal readonly Dictionary<T, IDisposable> _underlyingReceipt;

    internal Receipt(Dictionary<T, IDisposable> underlyingReceipt ) => _underlyingReceipt = underlyingReceipt;
}
