using System;
using strange.extensions.signal.impl;

namespace BibaFramework.BibaTest
{
	public class TestModelResetSignal : Signal { };

	public class TestCheckForChartBoostCommandSignal : Signal { };
	public class TestCheckForSessionEndCommandSignal : Signal { };
	public class TestCheckToSkipTagScanCommandSignal : Signal { };
}