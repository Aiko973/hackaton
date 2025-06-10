using Godot;
using System;

// Author : Dorian Simon

namespace Com.IsartDigital.ProjectName {
	
	public partial class BarInformation : ProgressBar
	{
		[Export] Label barValue;
		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
			barValue.Text = Value.ToString();
		}
	}
}
