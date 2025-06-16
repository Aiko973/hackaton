using Com.IsartDigital.Hackaton;
using Com.IsartDigital.Hackaton.Libraries;
using Com.IsartDigital.OBG.Utils;
using Godot;
using System;

// Author : Dorian Simon

namespace Com.IsartDigital.Hackaton {
	
	public partial class ItemButton : TextureButton
	{
        public override void _Ready()
        {
            MouseEntered += MouseHover;
            Pressed += ItemButtonPressed;
            MouseExited += MouseExit;
        }

        private void ItemButtonPressed()
        {
            SoundManager.GetInstance().PlaySound(SFX.itemClick);
        }

        protected virtual void MouseHover()
        {
            SoundManager.GetInstance().PlaySound(SFX.hover);
            Tween lTween = CreateTween();
            lTween.TweenProperty(GetParent(), "scale", new Vector2(1.2f, 1.2f), 0.2f);
            lTween.Finished += lTween.Kill;
        }

        protected virtual void MouseExit()
        {
            Tween lTween = CreateTween();
            lTween.TweenProperty(GetParent(), "scale", Vector2.One, 0.2f);
            lTween.Finished += lTween.Kill;
        }
    }
}
