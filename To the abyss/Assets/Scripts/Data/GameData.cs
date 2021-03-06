﻿using ProjectReversing.Handlers;
using System;
using UnityEngine;
namespace ProjectReversing.Data
{
    [System.Serializable]
    public class GameData
    {
        public float[] position;
        public DateTime lastPlayedTime;
        public GameData(GameHandler handler)
        {
            Vector3 CheckPointPos = GameHandler.LastCheckPointPos;

            position = new float[3];
            position[0] = CheckPointPos.x;
            position[1] = CheckPointPos.y;
            position[2] = CheckPointPos.z;

            lastPlayedTime = DateTime.Now;
        }
    }
}