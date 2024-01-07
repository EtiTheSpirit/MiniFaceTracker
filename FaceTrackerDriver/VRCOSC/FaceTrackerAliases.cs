using FaceTrackerDriver.FaceTracker;
using FaceTrackerDriver.FaceTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerDriver.VRCOSC {
	public static class FaceTrackerAliases {

		/// <summary>
		/// A special value that reads <see cref="MouthShape.JawOpen"/>, <see cref="MouthShape.MouthUpperOverturn"/>, and <see cref="MouthShape.MouthLowerOverturn"/>
		/// to try to compute a generalized value for avatars with jaw flap bones. The default values are tailored to my Chimera ECLIPSE.
		/// </summary>
		/// <param name="faceTracker"></param>
		/// <param name="jawOpenWeight"></param>
		/// <param name="avgLipWeight"></param>
		/// <returns></returns>
		public static float GetJawFlapOpenFactor(this FaceTrackerAPI faceTracker, float jawOpenWeight = 1.1f, float avgLipWeight = 0.6f) {
			float lipTop = faceTracker.GetBlendshape(MouthShape.MouthUpperOverturn);
			float lipBot = faceTracker.GetBlendshape(MouthShape.MouthLowerOverturn);

			float lip = (lipTop + lipBot) * avgLipWeight;
			float jaw = faceTracker.GetBlendshape(MouthShape.JawOpen) * jawOpenWeight;

			return Saturate(jaw + lip);
		}

		private static float Saturate(float value) => MathF.Min(MathF.Max(value, 0.0f), 1.0f);

	}
}
