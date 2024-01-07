#define USE_XAN_SPECIAL_VERSION

using FaceTrackerDriver.FaceTracker;
using FaceTrackerDriver.FaceTracker.Data;
using FaceTrackerDriver.VRCOSC;
using OSCVRC;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;

namespace FaceTrackerDriver {
	public static class Program {

		public static readonly AvatarSet CHIMERA_ECLIPSE = new AvatarSet(
			new Guid("a0b15170-b5a1-4322-9026-db9011eb9e00")
		);

		private static int Main(string[] args) {
			int millisDelay = 50;

			if (args.Length >= 1) {
				string arg = args[0].ToLowerInvariant();
				if (arg.Length >= 2 && (arg[0] == '/' || arg[0] == '-')) {
					arg = arg[1..];
					if (arg == "?" || arg == "help" || arg == "h") {
						Console.ForegroundColor = ConsoleColor.White;
						Console.WriteLine(":: FAST MOUTH TRACKER DRIVER :: HELP FILE");
						Console.WriteLine(":: The Fast Mouth Tracker is a very small piece of software that");
						Console.WriteLine(":: interfaces with Vive's SRanipal system to drive their lip tracker.");
						Console.WriteLine("::");
						Console.WriteLine(":: ARGUMENTS (NOT CASE SENSITIVE)");
						Console.WriteLine(":: -h, -?, -help, etc.");
						Console.WriteLine("::     > Display this file.");
						Console.WriteLine("::");
						Console.WriteLine(":: -localUpdateSpeed <milliseconds>");
						Console.WriteLine("::     > The parameters relevant to the system will be sent over OSC");
						Console.WriteLine("::       once every <milliseconds>ms. Note that VRChat will only update other");
						Console.WriteLine("::       players' screens 10 times per second (100ms) so anything faster will");
						Console.WriteLine("::       only display locally. This is good for things like video recording but");
						Console.WriteLine("::       may not always display properly to other players.");
						Console.WriteLine("::     > The default value is 50.");
						return 0;
					} else if (arg == "localupdatespeed") {
						if (args.Length > 1) {
							string speedStr = args[1];
							if (uint.TryParse(speedStr, out var speed) && speed > 0 && speed < int.MaxValue) {
								millisDelay = unchecked((int)speed);
							} else {
								Console.WriteLine($"Failed to parse speed value. It should be a whole number greater than 0 and less than {int.MaxValue}.");
							}
						} else {
							Console.WriteLine("Missing value for parameter -localUpdateSpeed. Please execute this app with -h for more info.");
						}
					} else {
						Console.WriteLine("Unknown argument. Please execute this app with -h for more info.");
					}
				}
			}


			Console.WriteLine("Loading OSC system...");
			using VRCAvatarParameterOSCInterface osc = new VRCAvatarParameterOSCInterface();
			Console.WriteLine("Preparing face tracker...");
			using FaceTrackerAPI faceTracker = new FaceTrackerAPI();
			Console.WriteLine("Initializing Face Tracker (this might take a moment)...");

			if (!Process.GetProcessesByName("sr_runtime").Any()) {
				Console.WriteLine("Notice: sr_runtime is not open! It will be launched automatically. Hold on...");

				if (!IsAdmin()) {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Please manually start sr_runtime.exe (as administrator), or run this program as administrator.");
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.WriteLine("Press any key to quit . . .");
					Console.ReadKey(true);
					return -1;
				}
				Process.Start(@".\lib\SRanipalSystem\sr_runtime.exe");
				Thread.Sleep(1000); // Give it some time to start up.
				Console.WriteLine("Runtime has been launched. Back to initialization...");
			}

			if (!faceTracker.Initialize(out ApiStatus apiStatus)) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Failed to initialize Face Tracker: {apiStatus}");
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine("Press any key to quit . . .");
				Console.ReadKey(true);
				return (int)apiStatus;
			}

			Console.WriteLine("Face Tracker initialized successfully.");
			while (true) {
#if USE_XAN_SPECIAL_VERSION
				float jawOpenness = faceTracker.GetJawFlapOpenFactor(1.1f, 0.2f);
				osc.SetAvatarParameter("Face/MouthShape/Custom/JawOpen", jawOpenness);
#else
				osc.SendAllFaceShapes(faceTracker);
#endif

				Thread.Sleep(millisDelay);
			}
		}

		private static bool IsAdmin() {
			try {
				WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
				WindowsPrincipal principal = new WindowsPrincipal(currentUser);
				return principal.IsInRole(WindowsBuiltInRole.Administrator);
			} catch {
				return false;
			}
		}
	}
}