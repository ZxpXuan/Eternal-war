using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EternalWar.Control
{
	public interface ITimeAxisAction
	{
		int Prepare { get; }
		int Casting { get; }
		int Cooling { get; }
	}
}
