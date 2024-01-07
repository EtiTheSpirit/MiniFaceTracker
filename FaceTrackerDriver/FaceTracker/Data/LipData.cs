using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerDriver.FaceTracker.Data {

	[StructLayout(LayoutKind.Sequential)]
	public struct LipData {
		public int frame;
		public int time;
		public IntPtr image;
		public PredictionData predictionData;
	}
}
