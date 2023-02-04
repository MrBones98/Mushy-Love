using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomLoader : MonoBehaviour
{
    [SerializeField] private List<Transform> _actorsPositioning = new List<Transform>();
    [SerializeField] private List<Mushroom> _mushrooms = new List<Mushroom>();
    private List<GameObject> _mushroomCharacters = new List<GameObject>();
    
    public MushroomLoader Instance;
    private void Awake()
    {
        Debug.Log("Hello one instance");
        //if(Instance == null)
        //{
        //    Instance = this;
        //}
        //else 
        //{ 
        //    Destroy(Instance); 
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

            Instantiate(spawnedMushroom, Vector3.zero, Quaternion.identity);

            
            _mushroomCharacters.Add(spawnedMushroom);
            spawnedMushroom.transform.name = mushroom.Name;
            spawnedMushroom.transform.SetParent(container.transform);
            i++;
            Debug.Log(i);
        }
    }
}
