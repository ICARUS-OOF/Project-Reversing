using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectReversing.Objects
{
    public class Level : MonoBehaviour
    {
        public void PlayLevel(string _LevelName)
        {
            SceneManager.LoadScene(_LevelName);
        }
    }
}