using Corsair.Lighting.Animations.Internal;
using Corsair.Lighting.Contracts;

namespace Corsair.Lighting.Animations;

/// <exception cref="T:Corsair.Exceptions.DeviceNotConnectedException">The device is not connected, the operation could not be completed</exception>
/// <exception cref="T:Corsair.Exceptions.CorsairException">An unexpected event happened, the device may have gotten disconnected</exception>
public sealed class VerticalWaveAnimation(WaveAnimationOptions options, IKeyboardColorController keyboardColors) : WaveAnimation(VWAOptionsFormatter.Format(options), keyboardColors)
{
    /// <summary>
    /// A vertical wave animation using the first available Corsair keyboard
    /// </summary>
    public VerticalWaveAnimation(WaveAnimationOptions options) : this(options, CorsairSDK.KeyboardLighting.Colors) { }
}

static file class VWAOptionsFormatter
{
    /// <summary>
    /// Sets the starting position to be vertical if not already
    /// </summary>
    internal static WaveAnimationOptions Format(WaveAnimationOptions options)
    {
        if (options.StartPosition is StartingPosition.LeftToRight or StartingPosition.RightToLeft)
            return options with { StartPosition = StartingPosition.TopToBottom };
        return options;
    }
}
