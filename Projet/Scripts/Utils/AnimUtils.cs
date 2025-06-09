using Com.IsartDigital.OBG.Libraries;
using Godot;
using System;

// Author : Thomas Tran

namespace Com.IsartDigital.OBG.Utils 
{	
	public static class AnimUtils
	{
		public static Tween AnimateScale(Node pObject, Vector2 pTargetScale, float pDuration, Tween.TransitionType pTrans = Tween.TransitionType.Elastic, Tween.EaseType pEase = Tween.EaseType.Out, Tween pTween = null)
		{
			Tween lTween = pTween ?? pObject.CreateTween();
            lTween.TweenProperty(pObject, Properties.SCALE, pTargetScale, pDuration).SetTrans(pTrans).SetEase(pEase);

			if (pTween == null) lTween.Finished += lTween.Kill;
			return lTween;
        }

		public static Tween AnimateModulate(Node pObject, Color pTargetColor, float pDuration, Tween pTween = null)
		{
            Tween lTween = pTween ?? pObject.CreateTween();
            lTween.TweenProperty(pObject, Properties.MODULATE, pTargetColor, pDuration);

            if (pTween == null) lTween.Finished += lTween.Kill;
            return lTween;
        }

		public static Tween AnimatePosition(Node pObject, Vector2 pTargetPosition, float pDuration, Tween.TransitionType pTrans = Tween.TransitionType.Sine, Tween.EaseType pEase = Tween.EaseType.Out, Tween pTween = null)
		{
            Tween lTween = pTween ?? pObject.CreateTween();
            lTween.TweenProperty(pObject, Properties.POSITION, pTargetPosition, pDuration).SetTrans(pTrans).SetEase(pEase);

            if (pTween == null) lTween.Finished += lTween.Kill;
            return lTween;
        }
	}
}
