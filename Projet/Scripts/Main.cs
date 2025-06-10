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

		[Export] TextureProgressBar socialBar, lifeBar, moneyBar;

		[Export] TextureButton firstChoiceButton, secondChoiceButton, thirdChoiceButton;

		[Export] Label firstChoice, secondChoice, thirdChoice, characterName;

		[Export] Sprite2D characterSprite;

		[Export] Label contextLabel;

		[Export] Node itemSpriteList;

		[Export] TextureRect itemNeeded;

		private Dilemma currentDilemma;
		private PlayerProfiles currentPlayer;


		RandomNumberGenerator rand = new RandomNumberGenerator();
        List<Dilemma> dilemma = FileManager.GetDilemmaFromJson(Path.DILEMMA);

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

			characterSprite.Texture = PlayerSelec.currentCharacterSprite.Texture;

			ResetDilemma();
			currentPlayer = PlayerSelec.currentPlayer;
			

            firstChoiceButton.Pressed += FirstChoiceButtonPressed;
            secondChoiceButton.Pressed += SecondChoiceButtonPressed;
            thirdChoiceButton.Pressed += ThirdChoiceButtonPressed;

			UpdateBalancebar(currentPlayer);
			UpdateItemsVisible(currentPlayer);

			characterName.Text = PlayerSelec.characterName;
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
						ResetDilemma();

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
						ResetDilemma();
                    }
				}
			}

        }

        private void ThirdChoiceButtonPressed()
        {
			UpdatePlayerStat(currentPlayer, 2);
			ResetDilemma();
        }

		private void UpdateItemsVisible(PlayerProfiles pPlayer)
		{
			for (int i = 0; i < pPlayer.itemsList.Count; i++)
			{
				Items lItems = (Items)itemSpriteList.GetChild(i);
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

        public void ResetDilemma()
		{
            currentDilemma = dilemma[rand.RandiRange(0, dilemma.Count-1)];
			foreach (Items item in itemSpriteList.GetChildren())
			{
				string lItem = char.ToLower(item.Name.ToString()[0]) + item.Name.ToString().Substring(1);

				if (lItem == currentDilemma.choices[0].item) itemNeeded.Texture = item.TextureNormal;
			}

            contextLabel.Text = currentDilemma.dilemma;

            firstChoice.Text = currentDilemma.choices[0].name + "   " + currentDilemma.choices[0].item + " besoin";

            secondChoice.Text = currentDilemma.choices[1].name + "\n Besoin de ";

            if (currentDilemma.choices[1].socialTiesNeeded != -5) secondChoice.Text += currentDilemma.choices[1].socialTiesNeeded + " Vie Sociale";
            if (currentDilemma.choices[1].healthConditionsNeeded != -5) secondChoice.Text += currentDilemma.choices[1].healthConditionsNeeded + " Conditions de vie";
            if (currentDilemma.choices[1].purchasingPowerNeeded != -5) secondChoice.Text += currentDilemma.choices[1].purchasingPowerNeeded + " Pouvoir d'achat";

            thirdChoice.Text = currentDilemma.choices[2].name;

        }

        protected override void Dispose(bool pDisposing)
		{
			instance = null;
			base.Dispose(pDisposing);
		}
	}
}
