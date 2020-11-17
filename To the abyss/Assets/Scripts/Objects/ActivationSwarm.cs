using ProjectReversing.Handlers;
using ProjectReversing.Traits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class ActivationSwarm : MonoBehaviour
    {
        private bool timeStopped = false;
        private AudioSource audioSource;

        [SerializeField] private float stormRadius = 25f;
        [SerializeField] private float crashRadius = 8f;

        [SerializeField] private ParticleSystem stormParticles;
        [SerializeField] private ParticleSystem explosionParticles;

        private List<GameObject> ActivationCubeList = new List<GameObject>();

        private AudioSource forceFieldAudioSource;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, stormRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, crashRadius);
        }
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            forceFieldAudioSource = GetComponent<AudioSource>();
            forceFieldAudioSource.clip = AudioHandler.GetSoundEffect("ForceField").clip;
            forceFieldAudioSource.Play();
        }
        private void Update()
        {
            if (timeStopped)
            {
                audioSource.pitch = Mathf.Lerp(audioSource.pitch, 0f, Time.fixedDeltaTime * .08f);
                stormParticles.Pause();
            }
            if (TimeController.singleton.TimeSlowed)
            { return; }
            Collider[] IntoZoneColliders = Physics.OverlapSphere(transform.position, stormRadius);
            for (int i = 0; i < IntoZoneColliders.Length; i++)
            {
                Collider col = IntoZoneColliders[i];
                ActivationCube activationCube = col.transform.GetComponent<ActivationCube>();
                if (activationCube != null)
                {
                    Destroy(activationCube.GetComponent<Rigidbody>());
                    ActivationCubeList.Add(activationCube.gameObject);
                    Destroy(activationCube);
                }
            }
            
            Collider[] IntoCrashColliders = Physics.OverlapSphere(transform.position, crashRadius);
            for (int i = 0; i < IntoCrashColliders.Length; i++)
            {
                Collider col = IntoCrashColliders[i];
                if (col.transform.tag == ConstantHandler.ACTCUBE_TAG)
                {
                    Destroy(col.transform.gameObject);
                    ActivationCubeList.Remove(col.transform.gameObject);
                    StopTime();
                }
            }

            for (int i = 0; i < ActivationCubeList.Count; i++)
            {
                GameObject activationCube = ActivationCubeList[i];
                activationCube.transform.position = Vector3.Lerp(activationCube.transform.position, transform.position, TimeController.singleton.DeltaTime * .1f);
            }
        }

        private void StopTime()
        {
            AudioHandler.PlaySoundEffect("Explosion");
            explosionParticles.Play();
            ZaWarudo.singleton.MegaStopTime();
            timeStopped = true;
            StartCoroutine(SetAudioVolume(0f, 2f));
        }
        private IEnumerator SetAudioVolume(float targetVolume, float delay)
        {
            yield return new WaitForSeconds(delay);
            audioSource.volume = targetVolume;
        }

    }
}