using Com.IsartDigital.OBG.Utils;
using Godot;
using System;

// Author : Thomas Tran

namespace Com.IsartDigital.Hackaton
{
	public partial class Items : TextureButton
	{
		[Export] protected TextureRect textTexture;

        [Export] protected float scaleUpValue = 1.2f;
        [Export] protected float selectionDuration = 0.5f;
        public bool owned { get; set; }

		protected Vector2 currentScale;
        protected Color currentColor;
        public override void _Ready()
        {
			currentScale = textTexture.Scale;
			currentColor = textTexture.Modulate;

            MouseEntered += MouseHover;
			MouseExited += MouseExit;
        }
        protected virtual void MouseHover()
		{
			Tween lTween = CreateTween().SetParallel();
			AnimUtils.AnimateScale(textTexture, currentScale * scaleUpValue, selectionDuration, pTween: lTween);
			AnimUtils.AnimateModulate(textTexture, new Color(currentColor, 1), selectionDuration, pTween: lTween);
			lTween.Finished += lTween.Kill; 
        }

		protected virtual void MouseExit()
		{
            Tween lTween = CreateTween().SetParallel();
            AnimUtils.AnimateScale(textTexture, currentScale, selectionDuration, pTween: lTween);
            AnimUtils.AnimateModulate(textTexture, new Color(currentColor, 0), selectionDuration, pTween: lTween);
            lTween.Finished += lTween.Kill;
        }
	}
}
