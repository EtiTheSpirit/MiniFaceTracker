using FaceTrackerDriver.FaceTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerDriver.FaceTracker.API {

	/// <summary>
	/// The API providing access to the lip tracker.
	/// </summary>
	public sealed class LipAPI : IDisposable {
		/// <summary>
		/// The width of the mouth image.
		/// </summary>
		const int IMAGE_WIDTH = 800;

		/// <summary>
		/// The height of the mouth image.
		/// </summary>
		const int IMAGE_HEIGHT = 400;

		/// <summary>
		/// The number of color channels in the image.
		/// </summary>
		const int IMAGE_CHANNELS = 1;

		/// <summary>
		/// The stored lip data.
		/// </summary>
		private LipData _lipData;

		/// <summary>
		/// Whether or not this object has been disposed of.
		/// </summary>
		private bool _disposed;

		public LipAPI() {
			_lipData.image = Marshal.AllocCoTaskMem(IMAGE_WIDTH * IMAGE_HEIGHT * IMAGE_CHANNELS);
		}

		/// <summary>
		/// Returns the intensity of the provided blendshape. Raises <see cref="ArgumentOutOfRangeException"/> if the provided
		/// <see cref="MouthShape"/> is not valid.
		/// </summary>
		/// <param name="shape"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException">If the shape is undefined.</exception>
		public float GetShapeIntensity(MouthShape shape) {
			if (_disposed) throw new ObjectDisposedException(nameof(LipAPI));

			if (shape <= MouthShape.Invalid && shape > MouthShape.NUMBER_OF_SHAPES) throw new ArgumentOutOfRangeException(nameof(shape));
			shape--; // Correct for the +1 offset.

			if (TryUpdateData()) {
				return _lipData.predictionData.blendshapeWeights[(int)shape];
			} else {
				return 0;
			}
		}

		/// <summary>
		/// Attempts to update the stored data by acquiring it from sr_runtime, 
		/// returning true if the data was correctly received, false if not.
		/// </summary>
		/// <returns></returns>
		private bool TryUpdateData() {
			if (_disposed) throw new ObjectDisposedException(nameof(LipAPI));

			return SRAnipalAPI.GetLipData(ref _lipData) == ApiStatus.SUCCESS;
		}

		public void Dispose() {
			if (_disposed) return;

			_disposed = true;
			Marshal.FreeCoTaskMem(_lipData.image);
			_lipData.image = IntPtr.Zero;
		}
	}
}
