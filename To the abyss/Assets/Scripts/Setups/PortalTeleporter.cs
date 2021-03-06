﻿using ProjectReversing.Handlers;
using ProjectReversing.Movement;
using ProjectReversing.Traits;
using UnityEngine;
namespace ProjectReversing.Setups
{
    public class PortalTeleporter : MonoBehaviour
    {
		private Transform player;
		[SerializeField] private Transform reciever;

		private bool playerIsOverlapping = false;

        private void Start()
        {
			player = PlayerMovement.singleton.transform;
        }

        // Update is called once per frame
        void Update()
		{
			if (playerIsOverlapping)
			{
				Vector3 portalToPlayer = player.position - transform.position;
				float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

				// If this is true: The player has moved across the portal
				if (dotProduct < 0f)
				{
					// Teleport him!
					float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
					rotationDiff += 180;
					player.Rotate(Vector3.up, rotationDiff);

					Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
					player.position = reciever.position + positionOffset;

					playerIsOverlapping = false;
				}
			}
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.tag == ConstantHandler.PLAYER_TAG)
			{
				playerIsOverlapping = true;
			}
		}

		void OnTriggerExit(Collider other)
		{
			if (other.tag == ConstantHandler.PLAYER_TAG)
			{
				playerIsOverlapping = false;
			}
		}
	}
}