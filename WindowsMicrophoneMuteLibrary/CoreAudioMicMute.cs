using System;
using System.Collections.Generic;
using System.Text;
using CoreAudioApi;

namespace WindowsMicrophoneMuteLibrary
{
    /// <summary>
    /// Interface for Muting the Microphone using Microsoft Windows Vista core audio APIs:
    /// http://msdn.microsoft.com/en-us/library/dd370802%28VS.85%29.aspx
    /// Built by Matt Palmerlee November 2010
    /// Using Ray Molenkamp's C# managed wrapper for accessing the Vista Core Audio API
    /// http://www.codeproject.com/KB/vista/CoreAudio.aspx?msg=2489276
    /// </summary>
    internal class CoreAudioMicMute
    {
        private List<MMDevice> micDevices = null;

        public CoreAudioMicMute()
        {
            //SearchDevices();
        }

        public String[] SearchDevices()
        {
            micDevices = new List<MMDevice>();
            List<String> device_names = new List<String>();

            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();

            MMDeviceCollection devices = DevEnum.EnumerateAudioEndPoints(EDataFlow.eCapture, EDeviceState.DEVICE_STATE_ACTIVE);

            //tbMaster.Value = (int)(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            //device.AudioEndpointVolume.OnVolumeNotification += new AudioEndpointVolumeNotificationDelegate(AudioEndpointVolume_OnVolumeNotification);
            for (int i = 0; i < devices.Count; i++)
            {
                MMDevice deviceAt = devices[i];

                if (deviceAt.FriendlyName.ToLower() == "microphone")
                {
                    this.micDevices.Add(deviceAt);
                    device_names.Add(deviceAt.ID);
                }
            }

            //if (this.micDevice == null)
            //    throw new InvalidOperationException("Microphone not found by MicMute Library!");
            return device_names.ToArray();
        }

        public bool MuteMic(int index, bool mute)
        {
            try
            {
                this.micDevices[index].AudioEndpointVolume.Mute = mute;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GetMute(int index)
        {
            try
            {
                return this.micDevices[index].AudioEndpointVolume.Mute;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void MuteAll(bool mute)
        {
            foreach (MMDevice device in micDevices)
            {
                device.AudioEndpointVolume.Mute = mute;
            }
        }

    }
}
