using System.Globalization;
using System.Linq;
using Settings;
using UnityEditor;
using UnityEngine;

namespace Presentation
{
	public class InformationGUI : MonoBehaviour
	{
		[SerializeField] private Configuration _config;
		[SerializeField] private InterpolatedEntity _interpolatedEntity;
		[SerializeField] private Renderer[] _entityRenderers;
		[SerializeField] private Renderer _interpolatedEntityRenderer;

		private GUIStyle _labelStyle;

		private void Start()
		{
			_labelStyle = new GUIStyle { normal = { textColor = Color.white } };
		}

		private void OnGUI()
		{
			using (new GUILayout.AreaScope(new Rect(8, 8, 240, Screen.height)))
			{
				PresentEntityVisibility();
				PresentInterpolatedEntityVisibility();
				PresentTickRate();
				PresentBufferSize();
				PresentBufferMemory();
				PresentRenderTime();
				PresentSimulationTime();
				PresentInterpolationWindowSlider();
			}
		}

		private void PresentEntityVisibility()
		{
			using (new EditorGUILayout.HorizontalScope("box"))
			{
				GUILayout.Label("Entity Visibility");

				if (GUILayout.Button(_entityRenderers.Any(r => r.enabled) ? "Disable" : "Enable", GUILayout.Width(64)))
				{
					foreach (var rend in _entityRenderers)
					{
						rend.enabled ^= true;
					}
				}
			}
		}
	
		private void PresentInterpolatedEntityVisibility()
		{
			using (new EditorGUILayout.HorizontalScope("box"))
			{
				GUILayout.Label("Interpolated Entity Visibility");

				if (GUILayout.Button(_interpolatedEntityRenderer.enabled ? "Disable" : "Enable", GUILayout.Width(64)))
				{
					_interpolatedEntityRenderer.enabled ^= true;
				}
			}
		}

		private void PresentTickRate()
		{
			using (new EditorGUILayout.VerticalScope("box"))
			{
				GUILayout.Label($"Tick Rate {1f / Time.fixedDeltaTime}", _labelStyle);
			}
		}
	
		private void PresentBufferSize()
		{
			using (new EditorGUILayout.VerticalScope("box"))
			{
				GUILayout.Label($"Buffer Size {_interpolatedEntity.BufferCount}", _labelStyle);
			}
		}
		
		private void PresentBufferMemory()
		{
			using (new EditorGUILayout.VerticalScope("box"))
			{
				GUILayout.Label($"Buffer Memory {_config.BufferMemory.ToString("0.00", CultureInfo.InvariantCulture)}", _labelStyle);
				_config.BufferMemory = GUILayout.HorizontalSlider(_config.BufferMemory, Time.fixedDeltaTime, 1);
			}
		}
	
		private void PresentSimulationTime()
		{
			using (new EditorGUILayout.VerticalScope("box"))
			{
				GUILayout.Label($"Simulation Time {Time.time.ToString("0.00", CultureInfo.InvariantCulture)}", _labelStyle);
			}
		}
	
		private void PresentRenderTime()
		{
			using (new EditorGUILayout.VerticalScope("box"))
			{
				var renderTime = Time.time - _config.InterpolationWindow;
				GUILayout.Label($"Render Time {renderTime.ToString("0.00", CultureInfo.InvariantCulture)}", _labelStyle);
			}
		}
	
		private void PresentInterpolationWindowSlider()
		{
			using (new EditorGUILayout.VerticalScope("box"))
			{
				_config.InterpolationWindow = Mathf.Clamp(_config.InterpolationWindow, Time.fixedDeltaTime, _config.BufferMemory);
				GUILayout.Label($"Interpolation Window {_config.InterpolationWindow.ToString("0.00", CultureInfo.InvariantCulture)}", _labelStyle);
				_config.InterpolationWindow = GUILayout.HorizontalSlider(_config.InterpolationWindow, Time.fixedDeltaTime, _config.BufferMemory);
			}
		}
	}
}