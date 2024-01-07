using FaceTrackerDriver.FaceTracker.API;
using FaceTrackerDriver.FaceTracker.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerDriver.FaceTracker {
	public sealed partial class FaceTrackerAPI : IDisposable {
		const int ANIPAL_TYPE_LIP = 3;

		/// <summary>
		/// Whether or not the API has been initialized.
		/// </summary>
		[MemberNotNullWhen(true, nameof(_lipAPI))]
		public bool Initialized => InitializationStatus == ApiInitStatus.INITIALIZED;

		/// <summary>
		/// The current status of the system's initialization.
		/// </summary>
		public ApiInitStatus InitializationStatus { get; private set; }

		private LipAPI? _lipAPI;

		/// <summary>
		/// Initializes the face tracker API. Returns whether or not the operation was successful, and provides the <see cref="ApiStatus"/>.
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		[MemberNotNull(nameof(_lipAPI))]
		public bool Initialize(out ApiStatus status) {
			if (InitializationStatus == ApiInitStatus.INITIALIZED) throw new InvalidOperationException("The API has already been initialized.");
			if (InitializationStatus == ApiInitStatus.INITIALIZING) throw new InvalidOperationException("The API is currently in the process of initializing, please wait.");
			_lipAPI = new LipAPI();
			InitializationStatus = ApiInitStatus.INITIALIZING;

			int failures = 0;
			do {
				status = SRAnipalAPI.Initialize(ANIPAL_TYPE_LIP, IntPtr.Zero);
				if (status == ApiStatus.CONNECTION_FAULT) {
					if (failures > 20) return false; // Just bail.

					failures++;
					Thread.Sleep(250);
					continue;
				}
				break;
			} while (true);

			bool success = status == ApiStatus.SUCCESS;
			InitializationStatus = success ? ApiInitStatus.INITIALIZED : ApiInitStatus.NOT_INITIALIZED;
			return success;
		}

		/// <summary>
		/// Returns the value of a given lip blendshape.
		/// </summary>
		/// <param name="shape">The shape to query.</param>
		/// <param name="saturate">If true, the returned value is saturated, meaning it is clamped between 0 and 1 (inclusive).</param>
		/// 
		/// <returns></returns>
		public float GetBlendshape(MouthShape shape, bool saturate = true) {
			if (!Initialized) throw new InvalidOperationException($"The API has not been initialized. Please call {nameof(Initialize)}");
			float shapeValue = _lipAPI.GetShapeIntensity(shape);
			if (saturate) shapeValue = MathF.Max(MathF.Min(shapeValue, 1.0f), 0.0f);
			return shapeValue;
		}

		/// <summary>
		/// Returns the value of a given lip blendshape.
		/// </summary>
		/// <param name="shape">The shape to query.</param>
		/// <param name="modifyIntensityCallback">This function receives the raw value, and returns an adjusted value.</param>
		/// <param name="saturate">If true, the returned value is saturated, meaning it is clamped between 0 and 1 (inclusive). For this function, the value is saturated both before and after being processed by <paramref name="modifyIntensityCallback"/>.</param>
		/// <returns></returns>
		public float GetBlendshape(MouthShape shape, Func<float, float> modifyIntensityCallback, bool saturate = true) {
			float shapeValue = GetBlendshape(shape, saturate);
			shapeValue = modifyIntensityCallback(shapeValue);
			if (saturate) shapeValue = MathF.Max(MathF.Min(shapeValue, 1.0f), 0.0f);
			return shapeValue;
		}

		public void Dispose() {
			SRAnipalAPI.Release(ANIPAL_TYPE_LIP);
			if (Initialized) {
				_lipAPI.Dispose();
				_lipAPI = null;
				InitializationStatus = ApiInitStatus.NOT_INITIALIZED;
			}
		}
	}
}
