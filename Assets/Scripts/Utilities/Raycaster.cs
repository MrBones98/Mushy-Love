using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private GameObject _uiBox;
    public delegate void ConversationEngaged();
    public event ConversationEngaged conversationEngaged;

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

        if(hit.collider.TryGetComponent(out SpawnedMushroom spawnedMushroom)) 
        {
            Debug.Log($"{spawnedMushroom.Name}");
            _canRaycast = !_canRaycast;
            _uiBox.SetActive(true);
            conversationEngaged();
            spawnedMushroom.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
