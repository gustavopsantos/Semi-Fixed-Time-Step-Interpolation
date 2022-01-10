using Settings;
using UnityEngine;

namespace Presentation
{
	public class Entity : MonoBehaviour
	{
		[SerializeField] private Transform _transform;
		[SerializeField] private Configuration _configuration;
		[SerializeField] private InterpolatedEntity _interpolatedEntity;

		private void FixedUpdate()
		{
			ApplyMovement();
			_interpolatedEntity.BufferPosition(Time.fixedTime, _transform.position);
		}

		private void ApplyMovement()
		{
			var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			var motion = Vector2.ClampMagnitude(input, 1f) * _configuration.PlayerMovementSpeed * Time.fixedDeltaTime;
			_transform.Translate(motion);
		}
	}
}