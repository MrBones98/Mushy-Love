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

	public bool stopTriggerBuffer;

	DialogueResponse currentResponse;

	void Start()
    {
        
    }

    
    void Update()
    {
		

        if (inResponse)
        {
			Responding();
        }
        else
        {
			ManageDialogue();
		}
	}

	void ManageDialogue()
    {
		//if (Input.GetKeyDown(KeyCode.Space) && !inDialogueEvent)
		//	StartCoroutine(EnqueueDialogue(dialogueEvent));

		if (Input.GetMouseButtonDown(0) && inDialogueEvent && !inResponse && !stopTriggerBuffer)
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

            if (dialogueChunks.Count == 0 && dialogueEvent.responses.Count > 0)
            {
				inResponse = true;
				return;
            }

			if (dialogueChunks.Count == 0)
			{

				if (dialogueEvent.responses.Count > 0)
				{
					inResponse = true;
				}
                else
                {
					EndOfDialogue();
					print("FinishEndOfDialogue");
				}


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
		

		//Show buttons
    }

    public void SelectResponse(int responseIndex)
    {
		inResponse = false;
		EndOfDialogue();
		print("ResponseEndOfDialogue");

		print(dialogueEvent.responses.Count);
		currentResponse = dialogueEvent.responses[responseIndex];

        if (!isCurrentlyTyping && !stopTriggerBuffer)
        {
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
			else
			{

				InitiateDialogue.Instance.InitiateDialogueDirectly(currentResponse.NewDialogue);
			}
        }



		//StartCoroutine("WaitAframeForNewDialogue");
		StartCoroutine(DialougeStopTriggerBuffer());
	}


	public void EndOfDialogue()
    {

        if (inResponse)
        {
			return;
        }


		inDialogueEvent = false;
		print("Is done");

        if (dialogueEvent.directDialogueSegue != null)
        {
			InitiateDialogue.Instance.InitiateDialogueDirectly(dialogueEvent.directDialogueSegue);
        }

		dialogueEvent.directDialogueSegue = null;

	}

	IEnumerator WaitAframeForNewDialogue()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		InitiateDialogue.Instance.InitiateDialogueDirectly(dialogueEvent.directDialogueSegue);
	}

	IEnumerator DialougeStopTriggerBuffer()
	{
		stopTriggerBuffer = true;

		yield return new WaitForSeconds(.2f);

		stopTriggerBuffer = false;
	}
}
