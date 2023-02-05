using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

	int displayTextLength;

	[Header("General")]
	public bool inDialogueEvent = false;
	public float defaultDelay;
	float delay;
	public bool isCurrentlyTyping;
	public bool isCurrentlyAnimatedSpeaking;
	public bool inResponse = false;


	private GameObject player;
	DialogueEvent.DialogueChunk chunk;
	public TextMeshProUGUI dialogueTitle;
	public TextMeshProUGUI dialogueText;
	public Queue<DialogueEvent.DialogueChunk> dialogueChunks = new Queue<DialogueEvent.DialogueChunk>();

	
	private string completeText;
	public DialogueEvent dialogueEvent;

	int responseSelect;

	void Start()
    {
        
    }

    
    void Update()
    {
		ManageDialogue();
	}

	void ManageDialogue()
    {
		if (Input.GetKeyDown(KeyCode.Space) && !inDialogueEvent)
			StartCoroutine(EnqueueDialogue(dialogueEvent));
		else if (Input.GetKeyDown(KeyCode.Space) && inDialogueEvent && inResponse == false)
			DequeueDialogue();
	}

	public IEnumerator EnqueueDialogue(DialogueEvent db)
	{
		Instance.inDialogueEvent = true;
		dialogueEvent = db;
		dialogueChunks.Clear();
		inDialogueEvent = true;

		foreach (DialogueEvent.DialogueChunk chunk in db.dialogueChunks)
		{
			dialogueChunks.Enqueue(chunk);
		}
		dialogueText.maxVisibleCharacters = 0;

		yield return new WaitForSeconds(0.2f);

		DequeueDialogue();
	}

	public void DequeueDialogue()
	{
		if (inDialogueEvent)
		{
			if (isCurrentlyTyping && !chunk.canTextBeSkipped)
			{
				return;
			}

			if (isCurrentlyTyping)
			{
				StopAllCoroutines();
				CompleteText();
				isCurrentlyTyping = false;
				return;
			}

			if (dialogueChunks.Count == 0)
			{

				EndOfDialogue();

				return;
			}

			chunk = dialogueChunks.Dequeue();
			GetTextDisplayLength();
			dialogueTitle.text = chunk.profile?.title;

			dialogueText.maxVisibleCharacters = 0;
			completeText = chunk.dialText;
			dialogueText.text = chunk.dialText;

			StartCoroutine(DisplayText(chunk));
		}

	}



	private void CompleteText()
	{
		dialogueText.maxVisibleCharacters = displayTextLength;
	}

	public void GetTextDisplayLength()
	{
		bool isCounting = true;
		foreach (char c in chunk.dialText.ToCharArray())
		{
			if (c == '<')
				isCounting = false;

			if (isCounting)
				displayTextLength++;

			if (c == '>')
				isCounting = true;
		}
	}

	IEnumerator DisplayText(DialogueEvent.DialogueChunk chunk)
	{
		delay = defaultDelay;
		char[] fullTextArray = chunk.dialText.ToCharArray();
		isCurrentlyTyping = true;

		for (int i = 0; i < fullTextArray.Length; i++)
		{

			char c = fullTextArray[i];
			char nextC = 'c';

			if (i < fullTextArray.Length - 1)
			{
				nextC = fullTextArray[i + 1];
			}

			if (dialogueText.maxVisibleCharacters < displayTextLength)
			{
				dialogueText.maxVisibleCharacters++;
			}

			yield return new WaitForSeconds(delay);
		}

		yield return null;
		isCurrentlyTyping = false;
	}

	void Responding()
    {
		

    }

    public void SelectResponse(int responseIndex)
    {
		inResponse = false;
		print("stfu");
		//responseIndex = responseIndex
		print(dialogueEvent.responses.Count);
		DialogueResponse currentResponse = dialogueEvent.responses[responseIndex];

		if (currentResponse.loadOnId)
        {

			foreach (var dialogueFlag in currentResponse.FlagUpdate)
			{
				foreach (var managerFlag in GameFlagManager.instance.gameFlags)
				{
					if (dialogueFlag.flagID == managerFlag.flagID)
					{
                        if (currentResponse.isIncremental)
                        {
							managerFlag.flagValue += dialogueFlag.flagValue;
						}
                        else
                        {
							managerFlag.flagValue = dialogueFlag.flagValue;
                        }
	
					}
				}
			}

			InitiateDialogue.Instance.InitiateDialogueByID(currentResponse.ActorID, currentResponse.subID);
		}
    }


	public void EndOfDialogue()
    {
		inDialogueEvent = false;
		print("Is done");

    }
}
