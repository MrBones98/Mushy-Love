using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MushroomLoader : MonoBehaviour
{
    [SerializeField] private List<Transform> _actorsPositioning = new List<Transform>();
    [SerializeField] private List<Mushroom> _mushrooms = new List<Mushroom>();
    [SerializeField] private List<GameObject> _mushroomCharacters = new List<GameObject>();
    [SerializeField] private int _mushroomNameDisplayFontSize = 8;
    
    [HideInInspector] public MushroomLoader Instance;
    private void Awake()
    {
        Debug.Log("Hello one instance");
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
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

            canvaInteraction.name = $"{mushroom.Name}Interactable";
            canvaInteraction.transform.SetParent(spawnedMushroom.transform);

            button.name = $"{mushroom.Name}Button";
            button.transform.SetParent(canvaInteraction.transform);
            button.AddComponent<Button>();
            button.GetComponent<Button>().onClick.AddListener(() => DebugTest());

            spawnedMushroom.AddComponent<SpriteRenderer>();
            spawnedMushroom.GetComponent<SpriteRenderer>().sprite = mushroom.Image;

            //spawnedMushroom.AddComponent<Canvas>();
            canvaInteraction.AddComponent<Canvas>();
            canvaInteraction.AddComponent<TextMeshPro>();

            //interactable.GetComponent<Button>().image = null;
            
            canvaInteraction.GetComponent<TextMeshPro>().text = mushroom.Name;
            canvaInteraction.GetComponent<TextMeshPro>().fontSize = _mushroomNameDisplayFontSize;
            canvaInteraction.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Top;

            //button.GetComponent<Button>().onClick.AddListener(() =>InitiateDialogue.Instance.
            //button.GetComponent<Button>().onClick.AddListener(InitiateDialogue.Instance.InitiateDialogueByID("John", "help"));
            //button.GetComponent<Button>().onClick.AddListener(() => InitiateDialogue.Instance.InitiateDialogueByID("John", "help"));

            //spawnedMushroom.AddComponent<TextMeshPro>();
            //print(tmpName.text);
            //Instantiate(spawnedMushroom, Vector3.zero, Quaternion.identity);

            
            _mushroomCharacters.Add(spawnedMushroom);
            spawnedMushroom.transform.name = mushroom.Name;
            spawnedMushroom.transform.SetParent(container.transform);
            spawnedMushroom.transform.position = _actorsPositioning[i].position;
            i++;
            Debug.Log(i);
        }
    }

    public void DebugTest()
    {
        print("Button Pressed!");
    }
}
