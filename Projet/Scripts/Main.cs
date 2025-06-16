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

		[Export] Button restartButton;

		[Export] Label firstChoice, secondChoice, thirdChoice, characterName;

		[Export] Sprite2D characterSprite;

		[Export] Label contextLabel, yearCount;

		[Export] Node itemSpriteList;

		[Export] Sprite2D itemNeeded;

		[Export] TextureRect background, backgroundChoice;

		[Export] ColorRect transitionPanel;

		private int nbDilemma;
		private int nbDilemmaDone;
		private Dilemma currentDilemma;
		private PlayerProfiles currentPlayer;

		private List<Texture2D> backgrounds = new List<Texture2D>();


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

			nbDilemma = dilemma.Count;

			characterSprite.Texture = PlayerSelec.currentCharacterSprite.Texture;

			backgrounds.Add((Texture2D)GD.Load("res://Assets/Background/PAYSAGE_1_STAGE_1.png"));
			backgrounds.Add((Texture2D)GD.Load("res://Assets/Background/PAYSAGE_1_STAGE_2.png"));
			backgrounds.Add((Texture2D)GD.Load("res://Assets/Background/PAYSAGE_1_STAGE_3.png"));
			backgrounds.Add((Texture2D)GD.Load("res://Assets/Background/fondbus.png"));
			backgrounds.Add((Texture2D)GD.Load("res://Assets/Background/fondbus2.png"));
			backgrounds.Add((Texture2D)GD.Load("res://Assets/Background/Hackathon3.1.png"));
			backgrounds.Add((Texture2D)GD.Load("res://Assets/Background/Fond3.3.png"));

            ResetDilemma(-1);
			currentPlayer = PlayerSelec.currentPlayer;

            firstChoiceButton.Pressed += FirstChoiceButtonPressed;
            secondChoiceButton.Pressed += SecondChoiceButtonPressed;
            thirdChoiceButton.Pressed += ThirdChoiceButtonPressed;
			restartButton.Pressed += RestartButtonPressed;

			UpdateBalancebar(currentPlayer);
			UpdateItemsVisible(currentPlayer);

			characterName.Text = PlayerSelec.characterName;
		}

		private void RestartButtonPressed()
		{
			GetTree().ChangeSceneToFile(Path.PLAYER_SELEC);
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
						currentPlayer.itemsList[i].owned = false;
						UpdateItemsVisible(currentPlayer);
						ResetDilemma(0);

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
						ResetDilemma(1);
                    }
				}
			}

        }

        private void ThirdChoiceButtonPressed()
        {
			ResetDilemma(2);
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

        public void ResetDilemma(int pChoiceIndex)
		{
            nbDilemmaDone++;
            currentDilemma = dilemma[rand.RandiRange(0, dilemma.Count-1)];


            if (nbDilemmaDone < nbDilemma)
			{
				if (currentDilemma.done)
				{
					while (currentDilemma.done)
					{
						currentDilemma = dilemma[rand.RandiRange(0, dilemma.Count - 1)];
                    }
				}
			}
			else
			{
				backgroundChoice.Hide();
				firstChoiceButton.Hide();
				secondChoiceButton.Hide();
				thirdChoiceButton.Hide();
				contextLabel.Hide();
				characterName.Hide();
				firstChoice.Hide(); secondChoice.Hide(); thirdChoice.Hide();
				itemNeeded.Hide();
				restartButton.Show();
			}
			transitionPanel.Show();
			Tween tween = CreateTween();
			tween.TweenProperty(transitionPanel, "modulate", new Color(1, 1, 1, 1), 0.5f);
			tween.Finished += () => transitionFinished(pChoiceIndex);

            currentDilemma.done = true;
        }

		private void transitionFinished(int pChoiceIndex)
		{
            yearCount.Text = "Année :   " + (2050 + nbDilemmaDone) + "  /  2061";
            if (nbDilemmaDone < backgrounds.Count + 1) background.Texture = backgrounds[nbDilemmaDone - 1];

			if (pChoiceIndex>=0)
			{
				UpdatePlayerStat(currentPlayer, pChoiceIndex);
			}

			Tween tween = CreateTween();
			tween.TweenProperty(transitionPanel, "modulate", new Color(1, 1, 1, 0), 2f);
			tween.Finished += transitionPanel.Hide;

            Tween lTween = CreateTween();
            lTween.TweenProperty(contextLabel, "visible_ratio", 1, 3).From(0);
            lTween.Finished += lTween.Kill;

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
