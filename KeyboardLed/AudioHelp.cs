#region file header

// -----------------------------------------------------------------------------
// Project: KeyboardLed.KeyboardLed
// File:    AudioHelp.cs
// Author:  Robert.L
// Created: 2013/09/23 19:19
// -----------------------------------------------------------------------------

#endregion

namespace KeyboardLed
{
    #region using statements

    using CoreAudioApi;

    #endregion

    /// <summary>The audio help.</summary>
    public static class AudioHelp
    {
        /// <summary>The device.</summary>
        private static readonly MMDevice device;

        /// <summary>Initializes static members of the <see cref="AudioHelp"/> class.</summary>
        static AudioHelp()
        {
            var devEnum = new MMDeviceEnumerator();
            device = devEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
        }

        /// <summary>The is mute.</summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsMute()
        {
            return device.AudioEndpointVolume.Mute;
        }

        /// <summary>The set mute.</summary>
        /// <param name="mute">The mute.</param>
        public static void SetMute(bool mute)
        {
            device.AudioEndpointVolume.Mute = mute;
        }
    }
}