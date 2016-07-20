﻿using System;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
	public class BibaProfileSession
	{
		public DateTime LScoreStart { get; set; }
		public DateTime MScoreStart { get; set; }
		public DateTime VScoreStart { get; set; }

		public float LScore { get; set; }
		public float MScore { get; set; }
		public float VScore { get; set; }

		public List<BibaEquipment> SessionEquipments { get; set; }

		public BibaProfileSession ()
		{
			SessionEquipments = BibaGameConstants.DEFAULT_EQUIPENT_LIST;
		}
	}
}