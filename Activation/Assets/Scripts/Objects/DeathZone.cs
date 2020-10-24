using ProjectReversing.Handlers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectReversing.Objects
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider col)
        {
            if (col.transform.tag == ConstantHandler.PLAYER_TAG)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}