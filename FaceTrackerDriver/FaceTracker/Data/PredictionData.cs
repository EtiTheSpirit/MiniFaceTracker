using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerDriver.FaceTracker.Data {

	[StructLayout(LayoutKind.Sequential)]
	public struct PredictionData {
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)MouthShape.NUMBER_OF_SHAPES)]
		public float[] blendshapeWeights;
	}

}
