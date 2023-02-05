using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomLoader : MonoBehaviour
{
    [SerializeField] private List<Transform> _actorsPositioning = new List<Transform>();
    [SerializeField] private List<Mushroom> _mushrooms = new List<Mushroom>();
    [SerializeField]private List<GameObject> _mushroomCharacters = new List<GameObject>();
    
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
        //for (int i = 0; i < _actorsPositioning.Count-1; i++)
        //{
        //    Debug.Log(_actorsPositioning[i]);
        //}
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
            spawnedMushroom.AddComponent<SpriteRenderer>();
            spawnedMushroom.GetComponent<SpriteRenderer>().sprite = mushroom.Image;

            //Instantiate(spawnedMushroom, Vector3.zero, Quaternion.identity);

            
            _mushroomCharacters.Add(spawnedMushroom);
            spawnedMushroom.transform.name = mushroom.Name;
            spawnedMushroom.transform.SetParent(container.transform);
            spawnedMushroom.transform.position = _actorsPositioning[i].position;
            i++;
            Debug.Log(i);
        }
    }
}
