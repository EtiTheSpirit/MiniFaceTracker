﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceTrackerDriver.FaceTracker.Data {
	public enum ApiStatus : int {
		RUNTIME_NOT_FOUND			= -3,
		NOT_INITIAL					= -2,
		FAILED						= -1,
		SUCCESS						= 0,
		INVALID_INPUT				= 1,
		FILE_NOT_FOUND				= 2,
		DATA_NOT_FOUND				= 13,
		UNDEFINED					= 319,
		INITIAL_FAILED				= 1001,
		NOT_IMPLEMENTED				= 1003,
		NULL_POINTER				= 1004,
		OVER_MAX_LENGTH				= 1005,
		FILE_INVALID				= 1006,
		UNINSTALL_STEAM				= 1007,
		MEMCPY_FAIL					= 1008,
		NOT_MATCH					= 1009,
		NODE_NOT_EXIST				= 1010,
		UNKNOWN_MODULE				= 1011,
		MODULE_FULL					= 1012,
		UNKNOWN_TYPE				= 1013,
		INVALID_MODULE				= 1014,
		INVALID_TYPE				= 1015,
		OUT_OF_MEMORY				= 1016,
		BUSY						= 1017,
		NOT_SUPPORTED				= 1018,
		INVALID_VALUE				= 1019,
		COMING_SOON					= 1020,
		INVALID_CHANGE				= 1021,
		TIMEOUT						= 1022,
		DEVICE_NOT_FOUND			= 1023,
		INVALID_DEVICE				= 1024,
		NOT_AUTHORIZED				= 1025,
		ALREADY						= 1026,
		INTERNAL					= 1027,
		CONNECTION_FAILED			= 1028,
		ALLOCATION_FAILED			= 1029,
		OPERATION_FAILED			= 1030,
		NOT_AVAILABLE				= 1031,
		CALLBACK_IN_PROGRESS		= 1032,
		SERVICE_NOT_FOUND			= 1033,
		DISABLED_BY_USER			= 1034,
		EULA_NOT_ACCEPTED			= 1035,
		RUNTIME_NOT_RESPONDING		= 1036,
		OPENCL_NOT_SUPPORTED		= 1037,
		EYE_TRACKING_NOT_SUPPORTED	= 1038,
		CONNECTION_FAULT			= 1051
	};
}
