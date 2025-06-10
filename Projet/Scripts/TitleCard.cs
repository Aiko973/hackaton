using Com.IsartDigital.Hackaton.Libraries;
using Godot;
using System;

// Author : Tran Thomas

namespace Com.IsartDigital.Hackaton
{

	public partial class TitleCard : Control
	{
		[Export] TextureButton button;
		#region Singleton
		static private TitleCard instance;

		private TitleCard() { }

		static public TitleCard GetInstance()
		{
			if (instance == null) instance = new TitleCard();
			return instance;

		}
		#endregion

		public override void _Ready()
		{
			#region Singleton Ready
			if (instance != null)
			{
				QueueFree();
				GD.Print(nameof(TitleCard) + " Instance already exist, destroying the last added.");
				return;
			}

			instance = this;
			#endregion

			button.Pressed += ChangeScene;

        }
		
		private void ChangeScene()
		{
			GetTree().ChangeSceneToFile(Path.PLAYER_SELEC);
		}

		protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}
