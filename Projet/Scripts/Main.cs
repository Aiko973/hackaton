using Com.IsartDigital.Hackaton;
using Godot;
using System;
using System.Collections.Generic;

// Author : Tran Thomas

namespace Com.IsartDigital.ProjectName
{

	public partial class Main : Node2D
	{

		#region Singleton
		static private Main instance;

		private Main() { }

		static public Main GetInstance()
		{
			if (instance == null) instance = new Main();
			return instance;

		}
		#endregion

		[Export] ProgressBar socialBar;
		[Export] ProgressBar lifeBar;
		[Export] ProgressBar moneyBar;

		public override void _Ready()
		{
			#region Singleton Ready
			if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(Main) + " Instance already exist, destroying the last added.");
				return;
			}

			instance = this;
			#endregion

			List<Dilemma> dilemma = FileManager.GetDilemmaFromJson("res://Jsons/Choices.json");
			List<PlayerProfiles> players = FileManager.GetPlayersProfilesFromJson("res://Jsons/PlayerProfiles.json");

			GD.Print(players[0].meds.owned);
		}

		public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;

		}

		protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}
