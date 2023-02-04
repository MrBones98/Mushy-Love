using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class InputHandler : MonoBehaviour
{
    [SerializeField]private List <GameObject> _actors = new List<GameObject>();
    [SerializeField]private List<Vector3> _actorData = new List<Vector3>();
    public delegate void UpdateHandler();
    public event UpdateHandler inputHandled;
   private void Awake()
   {
        _actorData.Add(_actors[0].gameObject.transform.position);
        _actorData.Add(_actors[1].gameObject.transform.eulerAngles);
        //_actorData.Add(_actors[2].gameObject.transform.position);
        Debug.Log(_actorData[0]);
        Debug.Log(_actorData[1]);
        //Debug.Log(_actorData[3]);

        DOTween.Init();
   }
    private void Tweening()
    {

    }
    public void ConversationEngaged()
    {
        StartCoroutine(nameof(ConvoTurnTimer));
        Debug.Log("Sentence finished");
        _actors[0].transform.position = Vector3.Lerp(_actorData[0], new Vector3(_actorData[0].x * 1.2f, _actorData[0].y * 1.2f, _actorData[0].z * 1.1f),5.0f);
    }
    private IEnumerator ConvoTurnTimer()
    {
        //pass individual gameobject
        Debug.Log("Sentence starting");
        float timer = 0;
        Vector3 originalPos = _actorData[0];
        //while (timer <= 2.0f)
        //{
        //_actors[0].transform.position = Vector3.Lerp(originalPos, new Vector3(originalPos.x *1.2f, originalPos.y*1.2f, originalPos.z * 1.1f), 1/2.0f);
        //_actors[0].transform.DOPunchPosition(new Vector3(-0.9f,0,-0.6f) * Time.deltaTime, 2.0f);
        //_actors[0].transform.DOMove(new Vector3(0.9f, 0, 0.6f), 2.0f).SetEase(Ease.OutQuart);
        _actors[0].transform.DOMove(originalPos + Vector3.back,1.0f).SetEase(Ease.OutBack);
            
        //}
        yield return null;

    }
}
