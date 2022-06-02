using System;
using UnityEngine;

[Serializable]
public class WaveParameters : ScriptableObject
{
    [SerializeField] private int _numberOfWave;
    [SerializeField] private float _durationWave;
}
