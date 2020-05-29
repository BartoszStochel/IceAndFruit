using System;
using System.Collections.Generic;
using UnityEngine;

// TODO - if UpdateHost is going to be created, this class does not need to derive from MonoBehaviour.
// Timers can register themselves to UpdateHost. Timers module could be obsolete this way.
// Now, the only purpose of Timers module is to invoke ManagedUpdate of each timer.
public class Timers : MonoBehaviour
{
	private List<Timer> timers = new List<Timer>();

	private void Update()
	{
		for (int i = 0; i < timers.Count; i++)
		{
			timers[i].ManagedUpdate();
		}
	}

	public Timer CreateTimer(Action actionOnEnd, Func<bool> stopCondition = null)
	{
		Timer timer = new Timer(actionOnEnd, stopCondition);
		timers.Add(timer);

		return timer;
	}
}
