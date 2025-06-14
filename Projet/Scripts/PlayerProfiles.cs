using Com.IsartDigital.ProjectName;
using Godot;
using System;
using System.Collections.Generic;

// Author : Tran Thomas

namespace Com.IsartDigital.Hackaton
{

	public partial class PlayerProfiles
	{
		public Meds meds { get; set; }
		public CreditCard creditCard { get; set; }
		public Tent tent { get; set; }
		public Clothes clothes { get; set; }
		public Passport passport { get; set; }
		public Phone phone { get; set; }
		public Food food { get; set; }
		public Axe axe { get; set; }
		

		public List<Items> itemsList = new List<Items>();

		public int socialTies { get; set; }
		public int healthCondition { get; set; }
		public int purchasingPower { get; set; }

		public void CopyStuff(PlayerProfiles pPlayer)
		{
            meds = pPlayer.meds;
			creditCard = pPlayer.creditCard;
			tent = pPlayer.tent;
			clothes = pPlayer.clothes;
			passport = pPlayer.passport;
			phone = pPlayer.phone;
			food = pPlayer.food;
			axe = pPlayer.axe;

			itemsList = pPlayer.itemsList;
        }

    }
}
