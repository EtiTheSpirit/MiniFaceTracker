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
		/// <summary>
		/// You dropped your face! Ha! I'll get you a new one!
		/// </summary>
		Invalid					= 0,

		/// <summary>
		/// Sliding your jaw to the right.
		/// </summary>
		JawRight				= 1 + 0, // +JawX

		/// <summary>
		/// Sliding your jaw to the left.
		/// </summary>
		JawLeft					= 1 + 1, // -JawX

		/// <summary>
		/// Pushing your jaw forward, like having an underbite.
		/// </summary>
		JawForward				= 1 + 2,

		/// <summary>
		/// Opening your mouth. It's pretty self explanatory.
		/// </summary>
		JawOpen					= 1 + 3,

		/// <summary>
		/// Opening your jaw without separating your lips. This creates the "ape"
		/// look, in that their mouths appear relatively low on their face compared
		/// to humans.
		/// </summary>
		MouthApeShape			= 1 + 4,

		/// <summary>
		/// Sliding your upper lip to the right.
		/// </summary>
		MouthUpperRight         = 1 + 5, // +MouthUpper

		/// <summary>
		/// Sliding your upper lip to the left.
		/// </summary>
		MouthUpperLeft          = 1 + 6, // -MouthUpper

		/// <summary>
		/// Sliding your lower lip to the right.
		/// </summary>
		MouthLowerRight         = 1 + 7, // +MouthLower

		/// <summary>
		/// Sliding your lower lip to the left.
		/// </summary>
		MouthLowerLeft          = 1 + 8, // -MouthLower

		/// <summary>
		/// Pushing your upper lip forward and out so that it curls up.
		/// </summary>
		MouthUpperOverturn		= 1 + 9,

		/// <summary>
		/// Pushing your lower lip forward so that it curls down.
		/// </summary>
		MouthLowerOverturn		= 1 + 10,

		/// <summary>
		/// Perhaps better described as pursing your lips, like when you are
		/// about to kiss someone.
		/// </summary>
		MouthPout				= 1 + 11,

		/// <summary>
		/// The right side of the mouth curving upward.
		/// </summary>
		MouthSmileRight			= 1 + 12, // +SmileSadRight

		/// <summary>
		/// The left side of the mouth curving upward.
		/// </summary>
		MouthSmileLeft			= 1 + 13, // +SmileSadLeft

		/// <summary>
		/// The right side of the mouth curving downward.
		/// </summary>
		MouthSadRight			= 1 + 14, // -SmileSadRight

		/// <summary>
		/// The left side of the mouth curving downward.
		/// </summary>
		MouthSadLeft			= 1 + 15, // -SmileSadLeft

		/// <summary>
		/// Puffing your right cheek out.
		/// </summary>
		CheekPuffRight			= 1 + 16,

		/// <summary>
		/// Puffing your left cheek out.
		/// </summary>
		CheekPuffLeft			= 1 + 17,

		/// <summary>
		/// Sucking your cheeks inward, like a ghoul.
		/// </summary>
		CheekSuck				= 1 + 18,

		/// <summary>
		/// Lifting the right side of the top lip up, showing the top row of teeth.
		/// </summary>
		MouthUpperUpRight       = 1 + 19,

		/// <summary>
		/// Lifting the left side of the top lip up, showing the top row of teeth.
		/// </summary>
		MouthUpperUpLeft        = 1 + 20,

		/// <summary>
		/// Pushing the right side of the bottom lip down, showing the bottom row of teeth.
		/// </summary>
		MouthLowerDownRight     = 1 + 21,

		/// <summary>
		/// Pushing the left side of the bottom lip down, showing the bottom row of teeth.
		/// </summary>
		/// <remarks>
		/// Sheeeeesh.
		/// </remarks>
		MouthLowerDownLeft      = 1 + 22,

		/// <summary>
		/// Curling your upper lip into your mouth to hide it.
		/// </summary>
		MouthUpperInside        = 1 + 23,

		/// <summary>
		/// Curling your lower lip into your mouth to hide it.
		/// </summary>
		MouthLowerInside        = 1 + 24,

		/// <summary>
		/// Pulling your bottom lip up over your top lip. Unfortunately there is
		/// no upper overlay counterpart to this.
		/// </summary>
		MouthLowerOverlay		= 1 + 25,

		/// <summary>
		/// Combines with <see cref="TongueLongStep2"/>. See that for more information.
		/// </summary>
		TongueLongStep1			= 1 + 26,

		/// <summary>
		/// Sticking your tongue out forward. Step 1 handles the tongue lifting itself
		/// due to tensing the muscle, and step 2 here handles actually pushing it forward.
		/// </summary>
		TongueLongStep2			= 1 + 32,

		/// <summary>
		/// Curving your tongue down. This should have little effect unless
		/// <see cref="TongueLongStep1"/> and <see cref="TongueLongStep2"/>
		/// are strongly activated.
		/// </summary>
		TongueDown              = 1 + 30, // -TongueY

		/// <summary>
		/// Curving your tongue up. This should have little effect unless
		/// <see cref="TongueLongStep1"/> and <see cref="TongueLongStep2"/>
		/// are strongly activated.
		/// </summary>
		TongueUp                = 1 + 29, // +TongueY

		/// <summary>
		/// Curving your tongue to the right. This should have little effect unless
		/// <see cref="TongueLongStep1"/> and <see cref="TongueLongStep2"/>
		/// are strongly activated.
		/// </summary>
		TongueRight             = 1 + 28, // +TongueX

		/// <summary>
		/// Curving your tongue to the left. This should have little effect unless
		/// <see cref="TongueLongStep1"/> and <see cref="TongueLongStep2"/>
		/// are strongly activated.
		/// </summary>
		TongueLeft              = 1 + 27, // -TongueX
		
		/// <summary>
		/// Curling your tongue into a U shape.
		/// </summary>
		TongueRoll				= 1 + 31,

		/// <summary>
		/// Layers <strong>on top of</strong> <see cref="TongueUp"/> such that
		/// the tongue is going up and to the right.
		/// </summary>
		TongueUpRightMorph      = 1 + 33,

		/// <summary>
		/// Layers <strong>on top of</strong> <see cref="TongueUp"/> such that
		/// the tongue is going up and to the left.
		/// </summary>
		TongueUpLeftMorph       = 1 + 34,

		/// <summary>
		/// Layers <strong>on top of</strong> <see cref="TongueDown"/> such that
		/// the tongue is going down and to the right.
		/// </summary>
		TongueDownRightMorph    = 1 + 35,

		/// <summary>
		/// Layers <strong>on top of</strong> <see cref="TongueDown"/> such that
		/// the tongue is going down and to the left.
		/// </summary>
		TongueDownLeftMorph     = 1 + 36,

		/// <summary>
		/// The number of possible <see cref="MouthShape"/>s that are valid.
		/// </summary>
		NUMBER_OF_SHAPES		= 37
	}

}
