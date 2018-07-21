using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityUtility;

namespace EternalWar.Control
{
	public class AxisUnit : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
	{
		[SerializeField]
		Area area;

		public void OnPointerDown(PointerEventData eventData)
		{
			throw new System.NotImplementedException();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			throw new System.NotImplementedException();
		}
	}
}

