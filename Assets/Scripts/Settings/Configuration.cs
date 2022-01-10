using UnityEngine;

namespace Settings
{
	[CreateAssetMenu]
	public class Configuration : ScriptableObject
	{
		public float InterpolationWindow = 0.04f;
		public float PlayerMovementSpeed = 6;
		public float BufferMemory = 1;
	}
}