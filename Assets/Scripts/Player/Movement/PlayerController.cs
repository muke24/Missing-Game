using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace PeterThompson
{
	public enum CurrentState
	{
		idle, walking, crouching, sprinting, sliding, climbingLadder, wallRunning,
		vaulting, grabbedLedge, climbingLedge, surfaceSwimming, underwaterSwimming
	}

	public class StatusEvent : UnityEvent<Status, Func<IKData>>
	{

	}

	public class PlayerController : MonoBehaviour
	{
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}
