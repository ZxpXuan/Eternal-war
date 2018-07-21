using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityUtility
{
	public class SequenceEvent : MonoBehaviour
	{
		[System.Serializable]
		public class SequenceEventData
		{
			public string Label;
			public float BeforeTriggerDelay;
			public float AfterTriggerDelay;
			public UnityEvent Event;
		}

		[SerializeField]
		bool startAtBeginning;

		[SerializeField]
		SequenceEventData[] sequences;

		private void Start()
		{
			if (startAtBeginning)
				StartEvents();
		}

		public void StartEvents()
		{
			StartCoroutine(RunningEvents());
		}

		public void AbortEvents()
		{
			StopAllCoroutines();
		}

		IEnumerator RunningEvents()
		{
			foreach (var seq in sequences)
			{
				if (seq.BeforeTriggerDelay > 0)
					yield return new WaitForSeconds(seq.BeforeTriggerDelay);

				if (seq.Event != null)
					seq.Event.Invoke();

				if (seq.AfterTriggerDelay > 0)
					yield return new WaitForSeconds(seq.AfterTriggerDelay);
			}
		}
	}
}
