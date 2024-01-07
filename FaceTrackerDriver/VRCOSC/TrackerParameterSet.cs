using FaceTrackerDriver.FaceTracker;
using FaceTrackerDriver.FaceTracker.Data;
using OSCVRC;
using OSCVRC.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerDriver.VRCOSC {
	using ParameterPair = ValueTuple<string, Variant<int, float, bool>>;

	public static class TrackerParameterSet {

		/// <summary>
		/// This path is prepended to all <see cref="MouthShape"/> names when sending a value over OSC.
		/// </summary>
		public const string PARAM_BASE_PATH = "Face/MouthShape/";

		private static readonly ParameterPair[] _lipParams;

		static TrackerParameterSet() {
			_lipParams = new ParameterPair[(int)MouthShape.NUMBER_OF_SHAPES];
			for (int index = 0; index < _lipParams.Length; index++) {
				MouthShape shape = (MouthShape)(index + 1);
				_lipParams[index] = new ParameterPair($"{PARAM_BASE_PATH}{shape}", 0f);
			}
		}

		/// <summary>
		/// Collects and sends every <see cref="MouthShape"/> over OSC.
		/// </summary>
		/// <param name="itf"></param>
		/// <param name="api"></param>
		/// <param name="smoothFunc"></param>
		/// <param name="paramPathBase"></param>
		public static void SendAllFaceShapes(this VRCAvatarParameterOSCInterface itf, FaceTrackerAPI api, Func<float, float> smoothFunc) {
			for (MouthShape shape = 0; shape < MouthShape.NUMBER_OF_SHAPES - 1; /**/) {
				shape++; // Add early here so that the value is offset by +1.
				_lipParams[(int)shape].Item2 = api.GetBlendshape(shape, smoothFunc, true);
			}
			itf.SetManyParameters(_lipParams);
		}

		/// <summary>
		/// Collects and sends every <see cref="MouthShape"/> over OSC.
		/// </summary>
		/// <param name="itf"></param>
		/// <param name="api"></param>
		/// <param name="smoothFunc"></param>
		/// <param name="paramPathBase"></param>
		public static void SendAllFaceShapes(this VRCAvatarParameterOSCInterface itf, FaceTrackerAPI api) {
			for (MouthShape shape = 0; shape < MouthShape.NUMBER_OF_SHAPES - 1; /**/) {
				shape++; // Add early here so that the value is offset by +1.
				_lipParams[(int)shape].Item2 = api.GetBlendshape(shape, true);
			}
			itf.SetManyParameters(_lipParams);
		}
	}
}
