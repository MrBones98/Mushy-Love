using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DialogueEvent : MonoBehaviour
{

	[System.Serializable]
	public class DialogueChunk
	{
		public DialogueProfile profile;     //Sepparate calss that contains all personal info for the dialogue speaker. e.g. Name of speaker, voice of speaker, object of speaker etc.

		[TextArea(4, 8)]
		public string dialText;

		public bool updateBehaviour = false;
		public bool otherUpdates = false;

		[Header("Behaviour")]
		[ShowIf("updateBehaviour", true)]
		//public ActorMood actorMood;

		[ShowIf("updateBehaviour", true)]
		//public BeepPitch beepPitch = BeepPitch.Normal;

		[Header("Other")]
		[ShowIf("otherUpdates", true)]
		public bool canTextBeSkipped = true;

		[ShowIf("otherUpdates", true)]
		public float continueTime;


	}
}
