using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private GameObject _uiBox;
    [SerializeField] private List<GameObject> _backgroundMushrooms;
    public delegate void ConversationEngaged();
    public static event ConversationEngaged conversationEngaged;

    private bool _canRaycast = true;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) &&_canRaycast)
        {
            ShootRay();
        }
    }
    private void ShootRay()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector3.forward);

        if (hit.collider.TryGetComponent(out SpawnedMushroom spawnedMushroom))
        {
            Debug.Log($"{spawnedMushroom.Name}");
            _canRaycast = !_canRaycast;
            for (int i = 0; i < _backgroundMushrooms.Count; i++)
            {
                _backgroundMushrooms[i].gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            }
                
            
            _uiBox.SetActive(true);
            conversationEngaged();
            spawnedMushroom.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
            return;
    }
}
