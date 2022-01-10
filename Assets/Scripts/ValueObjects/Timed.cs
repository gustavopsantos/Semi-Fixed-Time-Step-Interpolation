namespace ValueObjects
{
	public readonly struct Timed<T>
	{
		public readonly T Value;
		public readonly float Time;

		public Timed(T value, float time)
		{
			Value = value;
			Time = time;
		}
	}
}