using ProjectReversing.Handlers;
using ProjectReversing.Traits;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectReversing.Objects
{
    public class ActivationSwarm : MonoBehaviour
    {
        [SerializeField] private float stormRadius = 25f;
        [SerializeField] private float crashRadius = 8f;

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
            forceFieldAudioSource = GetComponent<AudioSource>();
            forceFieldAudioSource.clip = AudioHandler.GetSoundEffect("ForceField").clip;
            forceFieldAudioSource.Play();
        }
        private void Update()
        {
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
            
            Collider[] IntoCrashColliders = Physics.OverlapSphere(transform.position, stormRadius);
            for (int i = 0; i < IntoCrashColliders.Length; i++)
            {
                Collider col = IntoCrashColliders[i];
                if (col.transform.tag == ConstantHandler.ACTCUBE_TAG)
                {
                    Destroy(col.transform.gameObject);
                    ZaWarudo();
                }
            }

            for (int i = 0; i < ActivationCubeList.Count; i++)
            {
                GameObject activationCube = ActivationCubeList[i];
                activationCube.transform.position = Vector3.Lerp(activationCube.transform.position, transform.position, TimeController.singleton.DeltaTime * .1f);
            }
        }

        private void ZaWarudo()
        {

        }
    }
}