using FaceTrackerDriver.FaceTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerDriver.FaceTracker.API {
	internal static class SRAnipalAPI {
		/// <summary>
		/// Gets version 2 lip data from anipal's Lip module.
		/// </summary>
		/// <param name="data">ViveSR.anipal.Lip.LipData_v2</param>
		/// <returns>Indicates the resulting ViveSR.Error status of this method.</returns>
		[DllImport("./lib/SRanipal", EntryPoint = "GetLipData_v2")]
		public static extern ApiStatus GetLipData(ref LipData data);

		/// <summary>
		/// Invokes an anipal module.
		/// </summary>
		/// <param name="anipalType">The index of an anipal module.</param>
		/// <param name="config">Indicates the resulting ViveSR.Error status of this method.</returns>
		/// <returns>Indicates the resulting ViveSR.Error status of this method.</returns>
		[DllImport("./lib/SRanipal", EntryPoint = "Initial")]
		public static extern ApiStatus Initialize(int anipalType, nint config);

		/// <summary>
		/// Terminates an anipal module.
		/// </summary>
		/// <param name="anipalType">The index of an anipal module.</param>
		/// <returns>Indicates the resulting ViveSR.Error status of this method.</returns>
		[DllImport("./lib/SRanipal", EntryPoint = "Release")]
		public static extern ApiStatus Release(int anipalType);

	}
}
