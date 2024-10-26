using System.Diagnostics;
using System.Drawing;
using Corsair.Lighting.Animations.Internal;
using Corsair.Lighting.Contracts;
using Corsair.Lighting.Internal;

namespace Corsair.Lighting.Animations;

/// <exception cref="T:Corsair.Exceptions.DeviceNotConnectedException">The device is not connected, the operation could not be completed</exception>
/// <exception cref="T:Corsair.Exceptions.CorsairException">An unexpected event happened, the device may have gotten disconnected</exception>
public sealed class HorizontalWaveAnimation(WaveAnimationOptions options, IKeyboardColorController keyboardColors) : WaveAnimation(HWAOptionsFormatter.Format(options), keyboardColors)
{
    /// <summary>
    /// A horizontal wave animation using the first available Corsair keyboard
    /// </summary>
    public HorizontalWaveAnimation(WaveAnimationOptions options) : this(options, CorsairSDK.KeyboardLighting.Colors) { }
}

static file class HWAOptionsFormatter
{
    /// <summary>
    /// Sets the starting position to be horizontal if not already
    /// </summary>
    internal static WaveAnimationOptions Format(WaveAnimationOptions options)
    {
        if (options.StartPosition is StartingPosition.TopToBottom or StartingPosition.BottomToTop)
            return options with { StartPosition = StartingPosition.LeftToRight };
        return options;
    }
}
