﻿// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

using System;
using System.Collections.Generic;
using RGB.NET.Core;
using RGB.NET.Devices.Corsair.Native;

namespace RGB.NET.Devices.Corsair
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a generic information for a <see cref="T:RGB.NET.Devices.Corsair.CorsairCustomRGBDevice" />.
    /// </summary>
    public class CorsairCustomRGBDeviceInfo : CorsairRGBDeviceInfo
    {
        #region Properties & Fields

        public CorsairLedId ReferenceCorsairLed { get; }
        public int LedCount { get; }

        #endregion

        #region Constructors

        //TODO DarthAffe 07.07.2018: DAP is a fan right now, that's most likely wrong
        /// <inheritdoc />
        /// <summary>
        /// Internal constructor of managed <see cref="T:RGB.NET.Devices.Corsair.CorsairCustomRGBDeviceInfo" />.
        /// </summary>
        /// <param name="deviceIndex">The index of the <see cref="T:RGB.NET.Devices.Corsair.CorsairCustomRGBDevice" />.</param>
        /// <param name="nativeInfo">The native <see cref="T:RGB.NET.Devices.Corsair.Native._CorsairDeviceInfo" />-struct</param>
        /// <param name="channelDeviceInfo">The native <see cref="T:RGB.NET.Devices.Corsair.Native._CorsairChannelDeviceInfo"/> representing this device.</param>
        /// <param name="referenceCorsairLed">The id of the first led of this device.</param>
        /// <param name="modelCounter">A dictionary containing counters to create unique names for equal devices models.</param>
        internal CorsairCustomRGBDeviceInfo(int deviceIndex, _CorsairDeviceInfo nativeInfo, _CorsairChannelDeviceInfo channelDeviceInfo,
                                            CorsairLedId referenceCorsairLed, Dictionary<string, int> modelCounter)
            : base(deviceIndex, GetDeviceType(channelDeviceInfo.type), nativeInfo,
                   GetModelName(channelDeviceInfo.type), modelCounter)
        {
            this.ReferenceCorsairLed = referenceCorsairLed;

            LedCount = channelDeviceInfo.deviceLedCount;
        }

        #endregion

        #region Methods

        private static RGBDeviceType GetDeviceType(CorsairChannelDeviceType deviceType)
        {
            switch (deviceType)
            {
                case CorsairChannelDeviceType.Invalid:
                    return RGBDeviceType.Unknown;

                case CorsairChannelDeviceType.FanHD:
                case CorsairChannelDeviceType.FanSP:
                case CorsairChannelDeviceType.FanLL:
                case CorsairChannelDeviceType.FanML:
                case CorsairChannelDeviceType.DAP:
                    return RGBDeviceType.Fan;

                case CorsairChannelDeviceType.Strip:
                    return RGBDeviceType.LedStripe;

                case CorsairChannelDeviceType.Pump:
                    return RGBDeviceType.Cooler;

                default:
                    throw new ArgumentOutOfRangeException(nameof(deviceType), deviceType, null);
            }
        }

        private static string GetModelName(CorsairChannelDeviceType deviceType)
        {
            switch (deviceType)
            {
                case CorsairChannelDeviceType.Invalid:
                    return "Invalid";

                case CorsairChannelDeviceType.FanHD:
                    return "HD Fan";

                case CorsairChannelDeviceType.FanSP:
                    return "SP Fan";

                case CorsairChannelDeviceType.FanLL:
                    return "LL Fan";

                case CorsairChannelDeviceType.FanML:
                    return "ML Fan";

                case CorsairChannelDeviceType.Strip:
                    return "Led Strip";

                case CorsairChannelDeviceType.DAP:
                    return "DAP Fan";

                case CorsairChannelDeviceType.Pump:
                    return "Pump";

                default:
                    throw new ArgumentOutOfRangeException(nameof(deviceType), deviceType, null);
            }
        }

        #endregion
    }
}
