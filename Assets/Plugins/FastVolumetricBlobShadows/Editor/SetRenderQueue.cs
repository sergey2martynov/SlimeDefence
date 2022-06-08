using UnityEngine;
using UnityEditor;

namespace PixelTea.BlobShadow.Editor
{
	public class SetRenderQueue : EditorWindow
	{
		private Material _material;
		private int _renderQueue;

		[MenuItem("Window/Rendering/Set Material Queue")]
		public static void ShowWindow()
		{
			SetRenderQueue window = (SetRenderQueue)GetWindow(typeof(SetRenderQueue));
			window.Show();
		}

		private void OnGUI()
		{
			EditorGUI.BeginChangeCheck();
			_material = EditorGUILayout.ObjectField(_material, typeof(Material), true) as Material;
			EditorGUILayout.LabelField(_material == null ? "Please select a material to modify." : $"Render Queue of {_material.name} is: {_material.renderQueue.ToString()}");
			_renderQueue = EditorGUILayout.IntField("Render Queue", _renderQueue);

			EditorGUI.BeginDisabledGroup(_material == null);
			if (GUILayout.Button("Set Render Queue"))
			{
				Undo.RecordObject(_material, $"Set Render Queue of {_material.name} to {_renderQueue}");
				_material.renderQueue = _renderQueue;
			}

			EditorGUI.EndDisabledGroup();
		}
	}
}