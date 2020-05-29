using System;
using UnityEngine;

public class Timer
{
	public bool IsActive { get; private set; }

	private event Action timeReachedZero;
	private event Func<bool> stopCondition;

	private float time;

	public Timer(Action actionOnEnd, Func<bool> stopCondition)
	{
		timeReachedZero = actionOnEnd;
		this.stopCondition = stopCondition;
	}

	public void StartCountdown(float time)
	{
		this.time = time;
		IsActive = true;
	}

	public void Stop()
	{
		IsActive = false;
	}

	public void ManagedUpdate()
	{
		if (!IsActive)
		{
			return;
		}

		if (stopCondition != null && stopCondition.Invoke())
		{
			IsActive = false;
			return;
		}

		if (time > 0f)
		{
			time -= Time.deltaTime;
		}

		if (time < 0f)
		{
			time = 0f;
		}

		if (time == 0f)
		{
			timeReachedZero?.Invoke();
			IsActive = false;
		}
	}
}
