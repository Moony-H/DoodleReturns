using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CoreSystem.Additional
{
    [CreateAssetMenu(menuName = "GameSetting/NoiseSetting")]
    public class NoiseSettings : SignalSourceAsset
    {
        /// <summary>Describes the behaviour for a channel of noise</summary>
        [Serializable]
        public struct NoiseParams
        {
            /// <summary>The frequency of noise for this channel.  Higher magnitudes vibrate faster</summary>
            [Tooltip("The frequency of noise for this channel.  Higher magnitudes vibrate faster.")]
            public float Frequency;

            /// <summary>The amplitude of the noise for this channel.  Larger numbers vibrate higher</summary>
            [Tooltip("The amplitude of the noise for this channel.  Larger numbers vibrate higher.")]
            public float Amplitude;

            /// <summary>If checked, then the amplitude and frequency will not be randomized</summary>
            [Tooltip("If checked, then the amplitude and frequency will not be randomized.")]
            public bool Constant;

            /// <summary>Get the signal value at a given time, offset by a given amount</summary>
            public float GetValueAt(float time, float timeOffset)
            {
                var t = (Frequency * time) + timeOffset;
                if (Constant)
                    return Mathf.Cos(t * 2 * Mathf.PI) * Amplitude * 0.5f;
                return (Mathf.PerlinNoise(t, 0f) - 0.5f) * Amplitude;
            }
        }

        /// <summary>
        /// Contains the behaviour of noise for the noise module for all 3 cardinal axes of the camera
        /// </summary>
        [Serializable]
        public struct TransformNoiseParams
        {
            /// <summary>Noise definition for X-axis</summary>
            [Tooltip("Noise definition for X-axis")]
            public NoiseParams X;
            /// <summary>Noise definition for Y-axis</summary>
            [Tooltip("Noise definition for Y-axis")]
            public NoiseParams Y;
            /// <summary>Noise definition for Z-axis</summary>
            [Tooltip("Noise definition for Z-axis")]
            public NoiseParams Z;

            /// <summary>Get the signal value at a given time, offset by a given amount</summary>
            public Vector3 GetValueAt(float time, Vector3 timeOffsets)
            {
                return new Vector3(
                    X.GetValueAt(time, timeOffsets.x),
                    Y.GetValueAt(time, timeOffsets.y),
                    Z.GetValueAt(time, timeOffsets.z));
            }
        }

        /// <summary>The array of positional noise channels for this <c>NoiseSettings</c></summary>
        [Tooltip("These are the noise channels for the virtual camera's position. Convincing noise setups typically mix low, medium and high frequencies together, so start with a size of 3")]
        [FormerlySerializedAs("m_Position")]
        public TransformNoiseParams[] PositionNoise = new TransformNoiseParams[0];

        /// <summary>The array of orientation noise channels for this <c>NoiseSettings</c></summary>
        [Tooltip("These are the noise channels for the virtual camera's orientation. Convincing noise setups typically mix low, medium and high frequencies together, so start with a size of 3")]
        [FormerlySerializedAs("m_Orientation")]
        public TransformNoiseParams[] OrientationNoise = new TransformNoiseParams[0];

        /// <summary>Get the noise signal value at a specific time</summary>
        /// <param name="noiseParams">The parameters that define the noise function</param>
        /// <param name="time">The time at which to sample the noise function</param>
        /// <param name="timeOffsets">Start time offset for each channel</param>
        /// <returns>The 3-channel noise signal value at the specified time</returns>
        public static Vector3 GetCombinedFilterResults(
            TransformNoiseParams[] noiseParams, float time, Vector3 timeOffsets)
        {
            var pos = Vector3.zero;
            if (noiseParams == null) return pos;
            for (var i = 0; i < noiseParams.Length; ++i)
                pos += noiseParams[i].GetValueAt(time, timeOffsets);
            return pos;
        }

        /// <summary>
        /// Returns the total length in seconds of the signal.  
        /// Returns 0 for signals of indeterminate length.
        /// </summary>
        public override float SignalDuration => 0;

        /// <summary>Interface for raw signal provider</summary>
        /// <param name="pos">The position impulse signal</param>
        /// <param name="rot">The rotation impulse signal</param>
        public override void GetSignal(float timeSinceSignalStart, out Vector3 pos, out Quaternion rot)
        {
            pos = GetCombinedFilterResults(PositionNoise, timeSinceSignalStart, Vector3.zero);
            rot = Quaternion.Euler(GetCombinedFilterResults(OrientationNoise, timeSinceSignalStart, Vector3.zero));
        }
    }
}
