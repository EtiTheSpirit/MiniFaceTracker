using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerDriver.FaceTracker.Data {
	public enum ApiInitStatus {
		/// <summary>
		/// The API has not been initialized.
		/// </summary>
		NOT_INITIALIZED,

		/// <summary>
		/// The API is currently busy initializing, please wait.
		/// </summary>
		INITIALIZING,

		/// <summary>
		/// The API has finished initializing.
		/// </summary>
		INITIALIZED

	}
}
