using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Dialogue Event", menuName = "Dialogue/Event")]
public class DialogueEvent : ScriptableObject
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

	[Header("Identification")]
	public string actorID;
	public string subId;

	[SerializeField]
	public List<GameFlag> gameFlags = new List<GameFlag>();

	[Header("Insert dialogue content here")]
	public DialogueChunk[] dialogueChunks;

	public DialogueEvent directDialogueSegue;

	public List<DialogueResponse> responses;

}

[System.Serializable]
public class DialogueResponse
{
	public string Response;

	public bool loadOnId;

	[SerializeField]
	[HideIf("loadOnId")]
	public DialogueEvent NewDialogue;

	[ShowIf("loadOnId", true)]
	public string ActorID;

	[ShowIf("loadOnId", true)]
	public string subID;

	public bool isIncremental;

	[SerializeField]
	public List<GameFlag> FlagUpdate = new List<GameFlag>();


}
