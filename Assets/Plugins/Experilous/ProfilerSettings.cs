using UnityEngine;

namespace Experilous
{
	[ExecuteInEditMode]
	public class ProfilerSettings : MonoBehaviour
	{
		public int MaxSamplesPerFrame;

		public ProfilerSettings()
		{
		}

		void Awake()
		{
			UpdateSettings();
		}

		void Start()
		{
			UpdateSettings();
		}

		void Update()
		{
			UpdateSettings();
		}

		void UpdateSettings()
		{
			Profiler.maxNumberOfSamplesPerFrame = MaxSamplesPerFrame;
		}
	}
}
