using Com.IsartDigital.OBG.Utils;
using Godot;
using System;

public partial class ButtonEffect : TextureButton
{
	public override void _Ready()
	{
        Pressed += ButtonSound_Pressed;
        MouseEntered += ButtonSound_MouseEntered;
        MouseExited += ButtonMouseExited;
    }

    private void ButtonSound_MouseEntered()
    {
        AnimUtils.AnimateScale(this, new Vector2(0.11f, 0.11f), 0.3f, Tween.TransitionType.Quart);
    }
    
    private void ButtonMouseExited()
    {
        AnimUtils.AnimateScale(this, new Vector2(0.09f,0.09f), 0.3f, Tween.TransitionType.Quart);
    }

    private void ButtonSound_Pressed()
    {
        Tween lTween = CreateTween();
        lTween.TweenProperty(this, "modulate", new Color(0.6f,0.6f,0.6f), 0.1f);
        lTween.TweenProperty(this, "modulate", new Color(1f, 1f, 1f), 0.1f);
        lTween.Kill();
    }

}
