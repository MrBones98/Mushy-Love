using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InitiateDialogue : MonoBehaviour
{
	public static InitiateDialogue Instance;
	public GameFlagManager flagManager;

	private void Awake()
	{
		//Raycaster.conversationEngaged += OnConversationEngaged();
		Instance = Instance ?? this;

		Raycaster.conversationEngaged += OnConversationEngaged;

		flagManager = FindObjectOfType<GameFlagManager>();
	}

    private void OnConversationEngaged()
    {

		InitiateDialogueByID("MagicMush", "Opening");

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
			InitiateDialogueByID("MagicMush", "Opening");
        }
    }

    public void InitiateDialogueByID(string actorID, string subID)
	{
		// Get dialogueTrigger's two potential IDs + relevant flags. Search through dialogue dump collection.
		//Find Dialogue that matches by all flags, actorID and subID.


		DialogueEvent[] dialoguesIDFiltered = DialogueCollection.Instance.dialogueCollection
			.Where(_dialogue => _dialogue.actorID == actorID && _dialogue.subId == subID)
			.ToArray();

		DialogueEvent dialogueToRun = dialoguesIDFiltered
			.Single(_dialogue => _dialogue.gameFlags
				.All(i => flagManager.gameFlags.Contains(i))
			);
        if (dialogueToRun == null)
        {
			print("You done goofed");
        }
		StartCoroutine(DialogueManager.Instance.EnqueueDialogue(dialogueToRun));
		
	}

	public void InitiateDialogueDirectly(DialogueEvent dialogue)
	{
		StartCoroutine(DialogueManager.Instance.EnqueueDialogue(dialogue));
	}
    private void OnDisable()
    {
		Raycaster.conversationEngaged -= OnConversationEngaged;
	}
}
