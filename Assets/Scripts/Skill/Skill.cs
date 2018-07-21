using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EternalWar.Control;

namespace EternalWar.Skill
{
	public class Skill : ITimeAxisAction
	{
		[SerializeField] protected int prepare;
		[SerializeField] protected int casting;
		[SerializeField] protected int cooling;
		[SerializeField] protected int cooldown;
		[SerializeField] protected bool[,] areaOfEffect;
		[SerializeField]

		public int Prepare
		{
			get { return prepare; }
		}

		public int Casting
		{
			get { return casting; }
		}

		public int Cooling
		{
			get { return cooling; }
		}


	}
}
