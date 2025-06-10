using Com.IsartDigital.Hackaton;
using Com.IsartDigital.Hackaton.Libraries;
using Godot;
using System;
using System.Collections.Generic;

// Author : Dorian Simon

namespace Com.IsartDigital.ProjectName {
	
	public partial class PlayerSelec : Control
	{
        [Export] TextureRect[] takenObjects;
        [Export] TextureButton[] itemsButtons;
        [Export] Items[] items;
        [Export] TextureButton rightArrow;
        [Export] TextureButton leftArrow;

		[Export] Button UnselectButton, lockPlayerButton, playButton;

        [Export] Sprite2D P1;
        [Export] Sprite2D P2;
        [Export] Sprite2D P3;

        [Export] TextureProgressBar socialBar, lifeBar, moneyBar;

        [Export] Label characterNameLabel;

        int currentPlayerIndex = 0;

        static public PlayerProfiles currentPlayer;
        static public Sprite2D currentCharacterSprite = new Sprite2D();
        public static string characterName;

		List<Items> textureButton = new List<Items>();

        List<PlayerProfiles> players = FileManager.GetPlayersProfilesFromJson(Path.PLAYER_PROFILES);

        public override void _Ready()
		{
			currentPlayer = players[currentPlayerIndex];
            currentCharacterSprite.Texture = P1.Texture;
            AddItemsIntoPlayer(currentPlayer);
            UpdateBalancebar(currentPlayer);

			for (int i = 0; i < itemsButtons.Length; i++)
			{
				int index = i;
				itemsButtons[i].Pressed += () => ButtonPressed(index);
            }

            UnselectButton.Pressed += UnselectButtonPressed;

            leftArrow.Pressed += LeftArrowPressed;
            rightArrow.Pressed += RightArrowPressed;
            lockPlayerButton.Pressed += LockPlayerButtonPressed;
            playButton.Pressed += PlayButtonPressed;
            
		}

        private void PlayButtonPressed()
        {
            for (int i = 0; i < currentPlayer.itemsList.Count; i++)
            {
                foreach (Items itemSelected in textureButton)
                {
                    if (currentPlayer.itemsList[i].GetType().Name == itemSelected.GetType().Name)
                    {
                        currentPlayer.itemsList[i].owned = true;
                    }
                }
            }

            GetTree().ChangeSceneToFile(Path.MAIN);
        }

        private void LockPlayerButtonPressed()
        {
            rightArrow.Disabled = leftArrow.Disabled = true;
            foreach (TextureButton itemButton in itemsButtons)
            {
                itemButton.Disabled = false;
            }
        }

        private void RightArrowPressed()
        {
            if (currentPlayerIndex < 2)
            {
                currentPlayerIndex++;
                currentPlayer = players[currentPlayerIndex];
                UpdateBalancebar(currentPlayer);
                UpdatePlayerSprite();
                if (currentPlayer.itemsList.Count == 0) AddItemsIntoPlayer(currentPlayer);
            }
        }

        private void LeftArrowPressed()
        {
            if (currentPlayerIndex > 0)
            {                    
                currentPlayerIndex--;
                currentPlayer = players[currentPlayerIndex];
                UpdateBalancebar(currentPlayer);
                UpdatePlayerSprite();
            }
        }

        private void UnselectButtonPressed()
        {
            if (textureButton.Count > 0)
			{
                int lastIndex = textureButton.Count - 1;
                takenObjects[lastIndex].Texture = null;
                textureButton.RemoveAt(lastIndex);
            }
        }

        private void ButtonPressed(int pIndex)
        {
            if (textureButton.Count < 4) textureButton.Add(items[pIndex]);
            for (int i = 0; i < textureButton.Count; i++)
            {
                takenObjects[i].Texture = textureButton[i].TextureNormal;
            }
        }

        public override void _Process(double pDelta)
		{
			float lDelta = (float)pDelta;
		}

        private void UpdateBalancebar(PlayerProfiles pPlayer)
        {
            socialBar.Value = pPlayer.socialTies;
            lifeBar.Value = pPlayer.healthCondition;
            moneyBar.Value = pPlayer.purchasingPower;
        }

        private void UpdatePlayerSprite()
        {
            if (currentPlayerIndex == 0)
            {
                P1.Show();
                P2.Hide();
                P3.Hide();
                currentCharacterSprite.Texture = P1.Texture;
                characterName = "Camille";
            }
            if (currentPlayerIndex == 1)
            {
                P1.Hide();
                P2.Show();
                P3.Hide();
                currentCharacterSprite.Texture = P2.Texture;
                characterName = "Claude";
            }
            if (currentPlayerIndex == 2)
            {
                P1.Hide();
                P2.Hide();
                P3.Show();
                currentCharacterSprite.Texture = P3.Texture;
                characterName = "Amira";
            }
            characterNameLabel.Text = characterName;
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
            pPlayer.itemsList.Add(pPlayer.axe);
        }
    }
}
