using Com.IsartDigital.Hackaton;
using Godot;
using System;
using System.Collections.Generic;

// Author : Thomas Tran

namespace Com.IsartDigital.Hackaton {
	
	public partial class Dilemma
	{
		public string dilemma { get; set; }
		public bool done { get; set; }
		public List<Choice> choices { get; set; }

		public Dilemma() { }
	}
}
