using Godot;
using System;

// Author : Tran Thomas

namespace Com.IsartDigital.ProjectName
{

	public partial class Player : Node2D
	{

		#region Singleton
		static private Player instance;

		private Player() { }

		static public Player GetInstance()
		{
			if (instance == null) instance = new Player();
			return instance;

		}
		#endregion

		public override void _Ready()
		{
			#region Singleton Ready
			if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(Player) + " Instance already exist, destroying the last added.");
				return;
			}

			instance = this;
			#endregion


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
