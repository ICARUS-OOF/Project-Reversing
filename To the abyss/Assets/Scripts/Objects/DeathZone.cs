using ProjectReversing.Handlers;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
namespace ProjectReversing.Objects
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider col)
        {
            if (col.transform.tag == ConstantHandler.PLAYER_TAG)
            {
                GameHandler.singleton.OnPlayerDie?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}