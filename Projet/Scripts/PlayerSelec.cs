using Com.IsartDigital.Hackaton;
using Com.IsartDigital.Hackaton.Libraries;
using Godot;
using System;
using System.Collections.Generic;
using static Godot.Projection;

// Author : Dorian Simon

namespace Com.IsartDigital.ProjectName {
	
	public partial class PlayerSelec : Control
	{
        [Export] TextureRect[] takenObjects;
        [Export] TextureButton[] itemsButtons;
        [Export] Items[] items;
        [Export] TextureButton rightArrow;
        [Export] TextureButton leftArrow;

		[Export] Button playButton;

        [Export] TextureRect P1;
        [Export] TextureRect P2;
        [Export] TextureRect P3;

        [Export] TextureProgressBar socialBar, lifeBar, moneyBar;

        [Export] Label characterNameLabel;

        int currentPlayerIndex = 0;

        static public PlayerProfiles currentPlayer;
        static public Sprite2D currentCharacterSprite = new Sprite2D();
        public static string characterName = "Camille";

		List<Items> listItems = new List<Items>();

        List<PlayerProfiles> players = FileManager.GetPlayersProfilesFromJson(Path.PLAYER_PROFILES);

        public static PlayerSelec instance;

        public override void _Ready()
		{
            instance = this;
			currentPlayer = players[currentPlayerIndex];
            currentCharacterSprite.Texture = P1.Texture;
            AddItemsIntoPlayer(currentPlayer);
            UpdateBalancebar(currentPlayer);

			for (int i = 0; i < itemsButtons.Length; i++)
			{
				int index = i;
				itemsButtons[i].Pressed += () => ButtonPressed(index);
            }

            leftArrow.Pressed += LeftArrowPressed;
            rightArrow.Pressed += RightArrowPressed;
            playButton.Pressed += PlayButtonPressed;

            LockPlayerButtonPressed();
        }

        private void PlayButtonPressed()
        {
            for (int i = 0; i < currentPlayer.itemsList.Count; i++)
            {
                foreach (Items itemSelected in listItems)
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
            //rightArrow.Disabled = leftArrow.Disabled = true;
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
                players[currentPlayerIndex].CopyStuff(currentPlayer);
                currentPlayer = players[currentPlayerIndex];
                UpdateBalancebar(currentPlayer);
                UpdatePlayerSprite("right");
                if (currentPlayer.itemsList.Count == 0) AddItemsIntoPlayer(currentPlayer);
            }
        }

        private void LeftArrowPressed()
        {
            if (currentPlayerIndex > 0)
            {                    
                currentPlayerIndex--;
                players[currentPlayerIndex].CopyStuff(currentPlayer);
                currentPlayer = players[currentPlayerIndex];
                UpdateBalancebar(currentPlayer);
                UpdatePlayerSprite("left");
            }
        }

        private void UnselectButtonPressed()
        {
            if (listItems.Count > 0)
			{
                int lastIndex = listItems.Count - 1;
                takenObjects[lastIndex].Texture = null;
                listItems.RemoveAt(lastIndex);
            }
        }

        public void RemoveItemAt(int pIndex)
        {
            if (listItems.Count - 1 != pIndex) return;
    
            takenObjects[pIndex].Texture = null;
            listItems.RemoveAt(pIndex);
        }

        private void ButtonPressed(int pIndex)
        {
            if (listItems.Count < 4) listItems.Add(items[pIndex]);
            for (int i = 0; i < listItems.Count; i++)
            {
                takenObjects[i].Texture = listItems[i].TextureNormal;
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

        private void UpdatePlayerSprite(string pDirection)
        {
            if (currentPlayerIndex == 0)
            {
                if (pDirection == "left")
                {
                    Tween lTween = CreateTween().SetParallel();
                    lTween.TweenProperty(P1, "position", new Vector2(190, 70), 1f).FromCurrent();
                    lTween.TweenProperty(P1, "modulate", new Color(1, 1, 1, 1), 1f);
                    lTween.TweenProperty(P2, "position", new Vector2(250,50), 1f).FromCurrent();
                    lTween.TweenProperty(P2, "modulate", new Color(1, 1, 1, 0), 1f);
                }

                currentCharacterSprite.Texture = P1.Texture;
                characterName = "Camille";
            }
            if (currentPlayerIndex == 1)
            {
                if (pDirection == "right")
                {
                    Tween lTween = CreateTween().SetParallel();
                    lTween.TweenProperty(P1, "position", new Vector2(-10, 70), 1f).FromCurrent();
                    lTween.TweenProperty(P1, "modulate", new Color(1,1,1,0),1f);
                    lTween.TweenProperty(P2, "position", new Vector2(100, 50), 1f).FromCurrent();
                    lTween.TweenProperty(P2, "modulate", new Color(1, 1, 1, 1), 1f);

                    lTween.Finished += lTween.Kill;
                }

                if (pDirection == "left")
                {
                    Tween lTween = CreateTween().SetParallel();

                    lTween.TweenProperty(P2, "position", new Vector2(100, 50), 1f).FromCurrent();
                    lTween.TweenProperty(P2, "modulate", new Color(1, 1, 1, 1), 1f);
                    lTween.TweenProperty(P3, "position", new Vector2(200, 80), 1f).FromCurrent();
                    lTween.TweenProperty(P3, "modulate", new Color(1, 1, 1, 0), 1f);
                    lTween.Finished += lTween.Kill;

                }

                currentCharacterSprite.Texture = P2.Texture;
                characterName = "Claude";
            }
            if (currentPlayerIndex == 2)
            {
                if (pDirection == "right")
                {
                    Tween lTween = CreateTween().SetParallel();
                    lTween.TweenProperty(P2, "position", new Vector2(-10, 50), 1f).FromCurrent();
                    lTween.TweenProperty(P2, "modulate", new Color(1, 1, 1, 0), 1f);
                    lTween.TweenProperty(P3, "position", new Vector2(100, 80), 1f).FromCurrent();
                    lTween.TweenProperty(P3, "modulate", new Color(1, 1, 1, 1), 1f);
                    lTween.Finished += lTween.Kill;
                }

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
