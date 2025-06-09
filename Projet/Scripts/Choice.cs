using Godot;
using System;

// Author : Thomas Tran

namespace Com.IsartDigital.Hackaton
{
	
	public partial class Choice
	{
		public string name { get; set; }
		public string item { get; set; }
		public string description { get; set; }
        public int socialTiesNeeded { get; set; }
        public int healthConditionsNeeded { get; set; }
        public int purchasingPowerNeeded { get; set; }
        public int socialTies { get; set; }
		public int healthCondition { get; set; }
		public int purchasingPower { get; set; }
	}
}
