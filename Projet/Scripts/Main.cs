using Com.IsartDigital.Hackaton;
using Com.IsartDigital.Hackaton.Libraries;
using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

		[Export] Node itemSpriteList;

		private Dilemma currentDilemma;
		private PlayerProfiles currentPlayer;

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

			List<Dilemma> dilemma = FileManager.GetDilemmaFromJson(Path.DILEMMA);
			List<PlayerProfiles> players = FileManager.GetPlayersProfilesFromJson(Path.PLAYER_PROFILES);

			currentDilemma = dilemma[0];
			currentPlayer = players[0];
			AddItemsIntoPlayer(currentPlayer);
			
			contextLabel.Text = currentDilemma.dilemma;

			firstChoiceButton.Text = currentDilemma.choices[0].name + "   "+ currentDilemma.choices[0].item + " besoin";

			secondChoiceButton.Text = currentDilemma.choices[1].name + ", Besoin de \n" +
				 + currentDilemma.choices[1].socialTiesNeeded + " Vie Sociale \n"+
				 + currentDilemma.choices[1].healthConditionsNeeded + " Conditions de vie \n"+
				 + currentDilemma.choices[1].purchasingPowerNeeded + " Pouvoir d'achat";

			thirdChoiceButton.Text = currentDilemma.choices[2].name;

            firstChoiceButton.Pressed += FirstChoiceButtonPressed;
            secondChoiceButton.Pressed += SecondChoiceButtonPressed;
            thirdChoiceButton.Pressed += ThirdChoiceButtonPressed;

			UpdateBalancebar(currentPlayer);
			UpdateItemsVisible(currentPlayer);
		}

        private void FirstChoiceButtonPressed()
        {
            for (int i = 0; i < currentPlayer.itemsList.Count; i++)
			{
				string lItem = char.ToLower(currentPlayer.itemsList[i].GetType().Name[0]) + currentPlayer.itemsList[i].GetType().Name.Substring(1);

                if (currentDilemma.choices[0].item == lItem)
				{
					if (currentPlayer.itemsList[i].owned)
					{
						UpdatePlayerStat(currentPlayer, 0);

						currentPlayer.itemsList[i].owned = false;
						UpdateItemsVisible(currentPlayer);
					}
					else break;
				}
			}
        }

        private void SecondChoiceButtonPressed()
        {
			if (currentPlayer.socialTies >= currentDilemma.choices[1].socialTiesNeeded)
			{
				if (currentPlayer.healthCondition >= currentDilemma.choices[1].healthConditionsNeeded)
				{
					if (currentPlayer.purchasingPower >= currentDilemma.choices[1].purchasingPowerNeeded)
					{
						UpdatePlayerStat(currentPlayer, 1);
                    }
				}
			}
        }

        private void ThirdChoiceButtonPressed()
        {
			UpdatePlayerStat(currentPlayer, 2);
        }

		private void UpdateItemsVisible(PlayerProfiles pPlayer)
		{
			for (int i = 0; i < pPlayer.itemsList.Count; i++)
			{
				Items lItems = (Items) itemSpriteList.GetChild(i);
				lItems.Visible = currentPlayer.itemsList[i].owned;
			}
		}

		private void UpdateBalancebar(PlayerProfiles pPlayer)
		{
			socialBar.Value = pPlayer.socialTies;
			lifeBar.Value = pPlayer.healthCondition;
			moneyBar.Value = pPlayer.purchasingPower;
		}

		private void UpdatePlayerStat(PlayerProfiles pPlayer, int pChoiceIndex)
		{
            currentPlayer.socialTies = Math.Clamp(currentPlayer.socialTies + currentDilemma.choices[pChoiceIndex].socialTies, -5, 5);
            currentPlayer.healthCondition = Math.Clamp(currentPlayer.healthCondition + currentDilemma.choices[pChoiceIndex].healthCondition, -5, 5);
            currentPlayer.purchasingPower = Math.Clamp(currentPlayer.purchasingPower + currentDilemma.choices[pChoiceIndex].purchasingPower, -5, 5);

			UpdateBalancebar(pPlayer);
        }

		private void AddItemsIntoPlayer(PlayerProfiles pPlayer)
		{
            pPlayer.itemsList.Add(pPlayer.meds);
            pPlayer.itemsList.Add(pPlayer.creditCard);
            pPlayer.itemsList.Add(pPlayer.tent);
            pPlayer.itemsList.Add(pPlayer.clothes);
            pPlayer.itemsList.Add(pPlayer.passport);
            pPlayer.itemsList.Add(pPlayer.phone);
            pPlayer.itemsList.Add(pPlayer.food);
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
