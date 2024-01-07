using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerDriver.FaceTracker.Data {

	/// <summary>
	/// The lip shape enum, by index. Note that all values are offset by +1 so that <see cref="Invalid"/> is <see langword="default"/>
	/// </summary>
	public enum MouthShape {
		//None = -1,
		Invalid					= 0,
		JawRight				= 1 + 0, // +JawX
		JawLeft					= 1 + 1, // -JawX
		JawForward				= 1 + 2,
		JawOpen					= 1 + 3,
		MouthApeShape			= 1 + 4,
		MouthUpperRight			= 1 + 5, // +MouthUpper
		MouthUpperLeft			= 1 + 6, // -MouthUpper
		MouthLowerRight			= 1 + 7, // +MouthLower
		MouthLowerLeft			= 1 + 8, // -MouthLower
		MouthUpperOverturn		= 1 + 9,
		MouthLowerOverturn		= 1 + 10,
		MouthPout				= 1 + 11,
		MouthSmileRight			= 1 + 12, // +SmileSadRight
		MouthSmileLeft			= 1 + 13, // +SmileSadLeft
		MouthSadRight			= 1 + 14, // -SmileSadRight
		MouthSadLeft			= 1 + 15, // -SmileSadLeft
		CheekPuffRight			= 1 + 16,
		CheekPuffLeft			= 1 + 17,
		CheekSuck				= 1 + 18,
		MouthUpperUpRight		= 1 + 19,
		MouthUpperUpLeft		= 1 + 20,
		MouthLowerDownRight		= 1 + 21,
		MouthLowerDownLeft		= 1 + 22,
		MouthUpperInside		= 1 + 23,
		MouthLowerInside		= 1 + 24,
		MouthLowerOverlay		= 1 + 25,
		TongueLongStep1			= 1 + 26,
		TongueLongStep2			= 1 + 32,
		TongueDown				= 1 + 30, // -TongueY
		TongueUp				= 1 + 29, // +TongueY
		TongueRight				= 1 + 28, // +TongueX
		TongueLeft				= 1 + 27, // -TongueX
		TongueRoll				= 1 + 31,
		TongueUpRightMorph      = 1 + 33,
		TongueUpLeftMorph		= 1 + 34,
		TongueDownRightMorph    = 1 + 35,
		TongueDownLeftMorph		= 1 + 36,

		/// <summary>
		/// The number of possible <see cref="MouthShape"/>s that are valid.
		/// </summary>
		NUMBER_OF_SHAPES		= 37
	}

}
