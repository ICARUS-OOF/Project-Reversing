using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Data.Serializables
{
    [System.Serializable]
    public class SoundEffect
    {
        public string ID;
        public AudioClip clip;
        public float volume = 1f;
    }
}