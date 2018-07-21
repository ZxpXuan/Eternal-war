using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EternalWar.Control
{
	public class TimeAxis : MonoBehaviour
	{
		#region Fields

		[Range(6, 20)]
		[SerializeField] int t_axisLength = 20;

		[SerializeField] Transform[] axes;
		[SerializeField] GameObject AxisUnitPrefab;

		protected int axisLength = 20;

		#endregion

		#region Properties

		public int AxisLength
		{
			get { return axisLength; }
			set { axisLength = value; }
		}

		#endregion

		#region Private Methods
		#endregion

		#region Public Methods

		public void Resize(int newLength)
		{
			if (newLength == axisLength) return;

			var diff = newLength - axisLength;

			if (diff > 0)
			{
				foreach (var a in axes)
				{
					for (int i = 0; i < diff; i ++)
					{
						Instantiate(AxisUnitPrefab, a);
					}
				}
			}
			else
			{
				diff *= -1;
				foreach (var a in axes)
				{
					for (int i = 1; i <= diff; i ++)
					{
						var childToDestroy = a.GetChild(a.childCount - i);
						Destroy(childToDestroy.gameObject);
					}
				}
			}

			axisLength = newLength;
		}

		#endregion

		#region Unity Messages

		private void Awake()
		{
			axes = new Transform[transform.childCount];
			for (int i = 0; i < transform.childCount; i++)
			{
				axes[i] = transform.GetChild(i);
			}

			axisLength = axes[0].childCount;
		}

		private void Update()
		{
			Resize(t_axisLength);
		}

		#endregion
	}
}
