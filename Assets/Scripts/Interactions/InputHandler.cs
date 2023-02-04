using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]private List <GameObject> _actors = new List<GameObject>();
    [SerializeField] private List<Vector3> _actorData = new List<Vector3>();
    public delegate void UpdateHandler();
    public event UpdateHandler inputHandled;
   private void Awake()
   {
        _actorData.Add(_actors[0].gameObject.transform.position);
        _actorData.Add(_actors[1].gameObject.transform.eulerAngles);
        _actorData.Add(_actors[2].gameObject.transform.localPosition);
   }
    public void ConversationEngaged()
    {
        StartCoroutine(nameof(ConvoTurnTimer));
        Debug.Log("Sentence finished");
    }
    private IEnumerator ConvoTurnTimer(GameObject currentActor)
    {
        Debug.Log("Sentence starting");
        float timer = 0;
        Vector3 originalPos = _actorData[0];
        while (timer <= 2.0f)
        {
            //This is for the poping in front part   
            //_actors[0].
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        yield return null;

    }
}
