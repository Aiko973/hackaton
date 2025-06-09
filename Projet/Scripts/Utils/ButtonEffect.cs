using Com.IsartDigital.OBG.Utils;
using Godot;
using System;

public partial class ButtonEffect : Button
{
	public override void _Ready()
	{
        Pressed += ButtonSound_Pressed;
        MouseEntered += ButtonSound_MouseEntered;
        MouseExited += ButtonMouseExited;
    }

    private void ButtonSound_MouseEntered()
    {
        AnimUtils.AnimateScale(this, new Vector2(1.2f, 1.2f), 0.3f, Tween.TransitionType.Quart);
    }
    
    private void ButtonMouseExited()
    {
        AnimUtils.AnimateScale(this, Vector2.One, 0.3f, Tween.TransitionType.Quart);
    }

    private void ButtonSound_Pressed()
    {
        Tween lTween = CreateTween();
        AnimUtils.AnimateScale(this, new Vector2(0.7f, 0.7f), 0.1f, pTween: lTween);
        AnimUtils.AnimateScale(this, Vector2.One, 0.1f, pTween: lTween);
        lTween.Kill();
    }

}
