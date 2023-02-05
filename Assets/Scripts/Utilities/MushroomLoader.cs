using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MushroomLoader : MonoBehaviour
{
    [SerializeField] private List<Transform> _actorsPositioning = new List<Transform>();
    [SerializeField] private List<Mushroom> _mushrooms = new List<Mushroom>();
    [SerializeField] private List<GameObject> _mushroomCharacters = new List<GameObject>();
    [SerializeField] private int _mushroomNameDisplayFontSize = 8;

    [SerializeField] private GameObject _responseBox;
    [SerializeField] private List<GameObject> _responesButtons;


    [HideInInspector] public MushroomLoader Instance;
    
    private void Awake()
    {

        Raycaster.conversationEngaged += ConversationEngaged;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    private void Update()
    {
        if(DialogueManager.Instance.inResponse)
        {
            if(DialogueManager.Instance.dialogueEvent.responses.Count == 0)
            {
                _responesButtons[0].SetActive(true);
                
            }
            else if(DialogueManager.Instance.dialogueEvent.responses.Count-1 == 1)
            {
                _responesButtons[0].SetActive(true);
                _responesButtons[2].SetActive(true);
               

            }
            else if(DialogueManager.Instance.dialogueEvent.responses.Count-1 == 2)
            {
                _responesButtons[0].SetActive(true);
                _responesButtons[1].SetActive(true); 
                _responesButtons[2].SetActive(true);

            }

            UpdateResponseUI();
        }
        else
        {
            _responesButtons[0].SetActive(false);
            _responesButtons[1].SetActive(false);
            _responesButtons[2].SetActive(false);
        }

        


    }
    private void ConversationEngaged()
    {
        
        _mushroomCharacters[0].transform.DOMove(new Vector3(-5.34f, -1.16f, 0.42f),1.0f).SetEase(Ease.OutBack);
    }

    void UpdateResponseUI()
    {

        if (DialogueManager.Instance.dialogueEvent.responses.Count-1 == 0)
        {
            _responesButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = DialogueManager.Instance.dialogueEvent.responses[0].Response;
        }
        else if(DialogueManager.Instance.dialogueEvent.responses.Count-1 == 1)
        {
            _responesButtons[0].GetComponentInChildren <TextMeshProUGUI>().text = DialogueManager.Instance.dialogueEvent.responses[0].Response;
            _responesButtons[2].GetComponentInChildren <TextMeshProUGUI>().text = DialogueManager.Instance.dialogueEvent.responses[1].Response;
        }
        else if (DialogueManager.Instance.dialogueEvent.responses.Count -1 == 2)
        {
            _responesButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = DialogueManager.Instance.dialogueEvent.responses[0].Response;
            _responesButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = DialogueManager.Instance.dialogueEvent.responses[1].Response;
            _responesButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = DialogueManager.Instance.dialogueEvent.responses[2].Response;
        }


    }

    private void Start()
    {
        GameObject container = new GameObject();
        container.name = "Mushroom Container";
        //Instantiate(container,Vector3.zero, Quaternion.identity);
        int i = 0;
        foreach(Mushroom mushroom in _mushrooms)
        {
            GameObject spawnedMushroom = new GameObject();
            GameObject canvaInteraction = new GameObject();
            GameObject button = new GameObject();

            BoxCollider2D boxCollider2D = new BoxCollider2D();

            canvaInteraction.name = $"{mushroom.Name}Interactable";
            canvaInteraction.transform.SetParent(spawnedMushroom.transform);

            button.name = $"{mushroom.Name}Button";
            button.transform.SetParent(canvaInteraction.transform);
            //button.AddComponent<Image>();
            //button.AddComponent<Button>();
            //button.GetComponent<Button>().onClick.AddListener(() => DebugTest());
            

            spawnedMushroom.AddComponent<SpriteRenderer>();
            spawnedMushroom.GetComponent<SpriteRenderer>().sprite = mushroom.Image;

            boxCollider2D = spawnedMushroom.AddComponent<BoxCollider2D>();
            //spawnedMushroom.GetComponent<Collider2D>().isTrigger = true;
            spawnedMushroom.AddComponent<SpawnedMushroom>();
            spawnedMushroom.GetComponent<SpawnedMushroom>().Name = mushroom.Name;


            spawnedMushroom.AddComponent<Canvas>();
            //canvaInteraction.AddComponent<Canvas>();
            //canvaInteraction.AddComponent<TextMeshPro>();

            ////interactable.GetComponent<Button>().image = null;
            
            //canvaInteraction.GetComponent<TextMeshPro>().text = mushroom.Name;
            //canvaInteraction.GetComponent<TextMeshPro>().fontSize = _mushroomNameDisplayFontSize;
            //canvaInteraction.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Top;

            //button.GetComponent<Button>().onClick.AddListener(() =>InitiateDialogue.Instance.
            //button.GetComponent<Button>().onClick.AddListener(InitiateDialogue.Instance.InitiateDialogueByID("John", "help"));
            //button.GetComponent<Button>().onClick.AddListener(() => InitiateDialogue.Instance.InitiateDialogueByID("John", "help"));

            //spawnedMushroom.AddComponent<TextMeshPro>();
            //print(tmpName.text);
            //Instantiate(spawnedMushroom, Vector3.zero, Quaternion.identity);

            
            spawnedMushroom.transform.name = mushroom.Name;
            spawnedMushroom.transform.SetParent(container.transform);
            spawnedMushroom.transform.position = _actorsPositioning[i].position;
            
            //
            spawnedMushroom.AddComponent<Button>();
            spawnedMushroom.GetComponent<Button>().onClick.AddListener(() => DebugTest("wow"));
            //

            _mushroomCharacters.Add(spawnedMushroom);
            i++;
            //Debug.Log(i);
        }
    }

    public void DebugTest(string blah)
    {
        print($"Button {blah} Pressed!");
    }
}
