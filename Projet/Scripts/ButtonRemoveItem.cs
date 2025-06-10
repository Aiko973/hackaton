using Godot;
using System;

// Author : Clément PERSYN

namespace Com.IsartDigital.ProjectName
{
	public partial class ButtonRemoveItem : Button
	{
		[Export] private int index;

        public override void _Ready()
        {
            base._Ready();
            Pressed += DoPressed;
        }

        private void DoPressed()
        {
            PlayerSelec.instance.RemoveItemAt(index);
        }
    }
}
