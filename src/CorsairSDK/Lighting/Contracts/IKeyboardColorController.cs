﻿using Corsair.Device.Devices;

namespace Corsair.Lighting.Contracts;

using System.Drawing;

public interface IKeyboardColorController : IDisposable
{
    Keyboard Device { get; }
    IReadOnlySet<KeyboardKey> KeyboardKeys { get; }

    IDisposable SetFromBitmap(byte[] bitmap);

    /// <returns>The lifetime of the key being set</returns>
    IDisposable SetKeys(Color color, params KeyboardKeys[] keys);
    IDisposable SetKeys(Color color, IEnumerable<KeyboardKeys> keys);

    IDisposable SetKeys(Color color, params KeyboardKey[] keys);
    void ClearKeys(params KeyboardKeys[] keys);
    void ClearKeys(IEnumerable<KeyboardKeys> keys);

    /// <returns>The lifetime of the key being set</returns>
    IDisposable SetZones(Color color, KeyboardZones zones);
    void ClearZones(KeyboardZones zones);


    /// <returns>The lifetime of the key being set</returns>
    IDisposable SetMultiple(params KeyboardKeyColor[] keyColors);
    void ClearMultiple(params KeyboardKeyColor[] keyColors);

    /// <summary>
    /// An alias for SetZones(KeyboardZones.AllZones, color);
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    IDisposable SetGlobal(Color color);
    void ClearAll();

    /// <summary>
    /// Native Interop doesn't have optimized tracking for colors already in use (Red -> Red), using the two interchangably may lead to undefined behaviour.
    /// </summary>
    ILightingInterop NativeInterop { get; }
}
