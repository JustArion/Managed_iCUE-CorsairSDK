﻿using Corsair.Bindings;
using Corsair.Device.Devices;
using Corsair.Exceptions;

namespace Corsair.Lighting.Internal;

using System.Diagnostics;
using Connection.Internal.Contracts;
using Device;
using Lighting.Contracts;

internal class KeyboardLighting : IKeyboardLighting, IDisposable
{
    private readonly IDeviceConnectionHandler _connectionHandler;
    private readonly Keyboard? _device;

    internal KeyboardLighting(IDeviceConnectionHandler connectionHandler)
    {
        _connectionHandler = connectionHandler;

        _colorController = new KeyboardColorController(_connectionHandler);

        Effects = new EffectController(_colorController);
    }

    internal KeyboardLighting(IDeviceConnectionHandler connectionHandler, Keyboard device)
    {
        _connectionHandler = connectionHandler;

        _colorController = new KeyboardColorController(_connectionHandler);

        Effects = new EffectController(_colorController);
        _device = device;
    }


    public bool TryInitialize(AccessLevel accessLevel = AccessLevel.Exclusive)
    {
        var connected =  _connectionHandler.Connect(DeviceReconnectPolicy.Default);

        var initialized =  connected && OnConnectionEstablished(accessLevel, _device);

        // Device is initialized
        // Device is not initialized
        Debug.WriteLine($"Device {(initialized
            ? "is"
            : "is not")} initialized", "Keyboard Lighting");

        LogSessionInfo();
        return initialized;
    }

    [Conditional("DEBUG")]
    private void LogSessionInfo()
    {
        var sessionDetails = CorsairSDK._connectionHandler._deviceConnection.GetConnectionDetails();

        Debug.WriteLine($"Client Version: {VersionToString(sessionDetails.clientVersion)}", "Session Analytics");
        Debug.WriteLine($"Server Version: {VersionToString(sessionDetails.serverVersion)}", "Session Analytics");
        Debug.WriteLine($"Sever-Host Version: {VersionToString(sessionDetails.serverHostVersion)}", "Session Analytics");

        return;
        string VersionToString(CorsairVersion version) => $"{version.major}.{version.minor}.{version.patch}";
    }

    private bool OnConnectionEstablished(AccessLevel accessLevel, Keyboard? keyboard = null)
    {
        try
        {
            keyboard ??= CorsairSDK.GetDeviceAs<Keyboard>();

            if (keyboard == null)
            {
                Debug.WriteLine("iCUE could connect, but did not detect a Corsair Keyboard connected to this system");
                return false;
            }
            _colorController.SetContext(keyboard, accessLevel);

            Debug.WriteLine($"Lighting established for Device 'Corsair {keyboard.Model}'", "Keyboard Lighting");
            return true;
        }
        catch (CorsairException ex)
        {
            Debug.WriteLine($"Failed to establish Lighting: {ex}", "Keyboard Lighting");
            return false;
        }

    }


    private readonly KeyboardColorController _colorController;
    public IKeyboardColorController Colors => _colorController;
    public IEffectController Effects { get; }
    public Keyboard Device => Colors.Device;

    public void Shutdown()
    {
        Colors.Dispose();
        Effects?.Dispose();
        _connectionHandler.Disconnect();
    }

    public void Dispose() => Shutdown();
}
