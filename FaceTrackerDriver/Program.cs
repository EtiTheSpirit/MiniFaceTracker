using FaceTrackerDriver.FaceTracker;
using FaceTrackerDriver.FaceTracker.Data;
using FaceTrackerDriver.VRCOSC;
using OSCVRC;
using System.Diagnostics;
using System.Security.Principal;

namespace FaceTrackerDriver {
	public static class Program {

		private static int Main(string[] args) {
			int millisDelay = 50;

			if (args.Length >= 1) {
				string arg = args[0].ToLowerInvariant();
				if (arg.Length >= 2 && (arg[0] == '/' || arg[0] == '-')) {
					arg = arg[1..];
					if (arg == "?" || arg == "help" || arg == "h") {
						Console.ForegroundColor = ConsoleColor.White;
						Console.WriteLine(":: MINI MOUTH TRACKER DRIVER :: HELP FILE");
						Console.WriteLine("::     > The Mini Mouth Tracker is a very small piece of software that");
						Console.WriteLine("::       interfaces with Vive's SRanipal system to drive their lip tracker.");
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
								Console.WriteLine($"All lip tracking parameters will be sent once every {millisDelay} milliseconds.");
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


			Console.WriteLine("Connecting OSC system...");
			using VRCAvatarParameterOSCInterface osc = new VRCAvatarParameterOSCInterface();
			Console.WriteLine("Preparing face tracker...");
			using FaceTrackerAPI faceTracker = new FaceTrackerAPI();

			if (!Process.GetProcessesByName("sr_runtime").Any()) {
				Console.WriteLine("The system is launching sr_runtime on your behalf, hold on just a moment.");

				if (!IsAdmin()) {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("sr_runtime needs administrator permissions to work. Please manually start sr_runtime.exe as administrator");
					Console.WriteLine("or run this program as administrator, ultimately your choice.");
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.WriteLine("Press any key to quit . . .");
					Console.ForegroundColor = ConsoleColor.White;
					Console.ReadKey(true);
					return -1;
				}
				Process.Start(@".\lib\SRanipalSystem\sr_runtime.exe");
				Thread.Sleep(1000); // Give it some time to start up.
			}
			Console.WriteLine("Initializing face tracker (this might take a moment, please be patient)...");

			if (!faceTracker.Initialize(out ApiStatus apiStatus)) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Failed to initialize Face Tracker: {apiStatus}");
				Console.ForegroundColor = ConsoleColor.DarkRed;
				if (apiStatus == ApiStatus.DEVICE_NOT_FOUND) {
					Console.WriteLine(" => Is the face tracker plugged in? This error indicates that the system can't find");
					Console.WriteLine("    your tracker it in any USB port.");
				} else if (apiStatus == ApiStatus.RUNTIME_NOT_RESPONDING) {
					Console.WriteLine(" => sr_runtime is having difficulty replying to this app's request to initialize it.");
					Console.WriteLine("    This is known to happen on newer versions of sr_runtime (such as the download on Vive's site)");
					Console.WriteLine("    Using the bundled version is recommended.");
				} else if (apiStatus == ApiStatus.RUNTIME_NOT_FOUND) {
					Console.WriteLine(" => sr_runtime either crashed or wasn't properly opened; the system cannot find its process.");
				}
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine("Press any key to quit . . .");
				Console.ForegroundColor = ConsoleColor.White;
				Console.ReadKey(true);
				return (int)apiStatus;
			}

			Console.WriteLine("Face Tracker initialized successfully.");
			while (true) {
				float jawOpenness = faceTracker.GetJawFlapOpenFactor(1.1f, 0.2f);
				osc.SetAvatarParameter("Face/MouthShape/Custom/JawOpen", jawOpenness);
				osc.SendAllFaceShapes(faceTracker);

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