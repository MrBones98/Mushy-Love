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

        while (timer <= 2.0f)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        yield return null;

    }
}
