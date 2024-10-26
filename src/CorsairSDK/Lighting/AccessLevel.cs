namespace Corsair.Lighting;

public enum AccessLevel
{
    /// <summary>
    /// Shared mode. Other applications can set Leds
    /// </summary>
    Shared = 0,
    /// <summary>
    /// Exclusive mode. Only this specific application can set Leds (Disconnects other applications from iCUE)
    /// </summary>
    Exclusive = 1,
    /// <summary>
    /// Exclusive key events, but shared lightings
    /// </summary>
    ExclusiveKeyEventsListening = 2,
    /// <summary>
    /// Fully exclusive mode
    /// </summary>
    ExclusiveLightingControlAndKeyEventsListening = 3,
}
