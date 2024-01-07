using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerDriver.VRCOSC {
	/// <summary>
	/// Contains one or more variants of an avatar, providing the <see cref="IsIDThis(Guid)"/> to detect when a player
	/// changes into a variation of an avatar.
	/// </summary>
	public class AvatarSet {

		private Guid[] _ids;

		public AvatarSet(params Guid[] ids) {
			_ids = new Guid[ids.Length];
			Array.Copy(ids, _ids, _ids.Length);
		}

		/// <summary>
		/// Returns whether or not the provided <paramref name="id"/> is one of the ones representing a variant of this avatar.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool IsIDThis(Guid id) => _ids.Contains(id);

	}
}
