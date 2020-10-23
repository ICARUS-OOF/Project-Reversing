using ProjectReversing.Data.Serializables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Handlers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioHandler : MonoBehaviour
    {
        #region Singleton
        public static AudioHandler singleton;
        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
            }
        }
        #endregion
        public List<SoundEffect> SoundEffects = new List<SoundEffect>();
        public AudioSource source;
        private void Start()
        {
            source = GetComponent<AudioSource>();
        }
        private void Update()
        {
            AudioSource[] sources = GameObject.FindObjectsOfType<AudioSource>();
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].volume = GameHandler.volume;
            }
        }
        public static void PlaySoundEffect(string ID)
        {
            AudioSource _source = singleton.source;
            SoundEffect _sfx = GetSoundEffect(ID);
            if (_sfx != null)
            {
                _source.PlayOneShot(_sfx.clip, _sfx.volume * GameHandler.volume);
            } else
            {
                Debug.LogError("Audio clip not found");
            }
        }
        public static SoundEffect GetSoundEffect(string ID)
        {
            SoundEffect sfx = null;
            List<SoundEffect> SoundEffectList = singleton.SoundEffects;
            for (int i = 0; i < SoundEffectList.Count; i++)
            {
                if (SoundEffectList[i].ID == ID)
                {
                    sfx = SoundEffectList[i];
                }
            }
            return sfx;
        }
    }
}