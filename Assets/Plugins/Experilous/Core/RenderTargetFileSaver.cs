using UnityEngine;

namespace Experilous.Core
{
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Experilous/Render Target File Saver")]
	public class RenderTargetFileSaver : MonoBehaviour
	{
		public string filename = "screenshot.png";

		private Camera _camera;

		protected void Start()
		{
			_camera = GetComponent<Camera>();
		}

		protected void OnPostRender()
		{
			if (string.IsNullOrEmpty(filename)) return;

			var renderTexture = _camera.targetTexture;

			if (renderTexture == null) return;

			using (var file = System.IO.File.Open(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write))
			{
				var texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
				texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0, false);
				var png = texture.EncodeToPNG();
				file.Write(png, 0, png.Length);
			}
		}
	}
}
