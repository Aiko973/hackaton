using Com.IsartDigital.Hackaton;
using Com.IsartDigital.OBG.Utils;
using Godot;
using System;

// Author : Dorian Simon

namespace Com.IsartDigital.ProjectName {
	
	public partial class ButtonEffectTitle : TextureButton
	{
        public override void _Ready()
        {
            Pressed += ButtonSound_Pressed;
            MouseEntered += ButtonSound_MouseEntered;
            MouseExited += ButtonMouseExited;
        }

        private void ButtonSound_MouseEntered()
        {
            AnimUtils.AnimateScale(this, new Vector2(1.2f,1.2f), 0.3f, Tween.TransitionType.Quart);
            SoundManager.GetInstance().PlaySound(Hackaton.Libraries.SFX.hover);
        }

        private void ButtonMouseExited()
        {
            AnimUtils.AnimateScale(this, Vector2.One, 0.3f, Tween.TransitionType.Quart);
        }

        private void ButtonSound_Pressed()
        {
            Tween lTween = CreateTween();
            //AnimUtils.AnimateScale(this, new Vector2(0.22f, 0.22f), 0.1f, pTween: lTween);
            AnimUtils.AnimateScale(this, Vector2.One, 0.1f, pTween: lTween);
            lTween.Kill();
            SoundManager.GetInstance().PlaySound(Hackaton.Libraries.SFX.click);

        }
    }
}
