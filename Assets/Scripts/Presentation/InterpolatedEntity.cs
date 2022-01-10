using System;
using Settings;
using UnityEngine;
using ValueObjects;

namespace Presentation
{
	// Based on https://developer.valvesoftware.com/wiki/Source_Multiplayer_Networking#Entity_interpolation
	public class InterpolatedEntity : MonoBehaviour
	{
		[SerializeField] private Configuration _configuration;

		private readonly Memory<Timed<Vector2>> _positionBuffer = new();

		public int BufferCount => _positionBuffer.Count;

		public void BufferPosition(float time, Vector2 position)
		{
			_positionBuffer.Add(new Timed<Vector2>(position, time), DateTime.Now.AddSeconds(_configuration.BufferMemory));
		}

		private void Update()
		{
			var renderTime = Time.time - _configuration.InterpolationWindow;
			if (TryFindSnapshots(renderTime, out var prev, out var next))
			{
				var interpolationPercentage = Mathf.InverseLerp(prev.Time, next.Time, renderTime);
				transform.position = Vector2.Lerp(prev.Value, next.Value, interpolationPercentage);
			}
			else
			{
				Debug.LogWarning($"Cannot find snapshots before and after {renderTime}, " +
				                 $"probably the value of interpolation window is higher than the buffer memory.");
			}
		}

		private bool TryFindSnapshots(float time, out Timed<Vector2> prev, out Timed<Vector2> next)
		{
			_positionBuffer.RemoveExpiredItems();

			for (int i = 0; i < _positionBuffer.Count - 1; i++)
			{
				if (_positionBuffer[i].Time < time && time <= _positionBuffer[i + 1].Time)
				{
					prev = _positionBuffer[i];
					next = _positionBuffer[i + 1];
					return true;
				}
			}

			prev = next = default;
			return false;
		}
	}
}