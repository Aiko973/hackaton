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

		[Export] Button firstChoiceButton;
		[Export] Button secondChoiceButton;
		[Export] Button thirdChoiceButton;

		[Export] Label contextLabel;

		[Export] Node Items;

		private Dilemma currentDilemma;

		RandomNumberGenerator rand = new RandomNumberGenerator();

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

			rand.Randomize();

			List<Dilemma> dilemma = FileManager.GetDilemmaFromJson("res://Jsons/Choices.json");
			List<PlayerProfiles> players = FileManager.GetPlayersProfilesFromJson("res://Jsons/PlayerProfiles.json");

			currentDilemma = dilemma[rand.RandiRange(0, dilemma.Count-1)];

			contextLabel.Text = currentDilemma.dilemma;

			firstChoiceButton.Text = currentDilemma.choices[0].name;
			secondChoiceButton.Text = currentDilemma.choices[1].name;
			thirdChoiceButton.Text = currentDilemma.choices[2].name;

            firstChoiceButton.Pressed += FirstChoiceButtonPressed;
            secondChoiceButton.Pressed += SecondChoiceButtonPressed;
            thirdChoiceButton.Pressed += ThirdChoiceButtonPressed;
		}

        private void FirstChoiceButtonPressed()
        {
            
        }
        private void SecondChoiceButtonPressed()
        {

        }

        private void ThirdChoiceButtonPressed()
        {

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

		private void UpdateItems()
		{
			
		}
	}
}
