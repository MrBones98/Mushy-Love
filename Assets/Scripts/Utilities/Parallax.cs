using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Parallax : MonoBehaviour
{

    [SerializeField][Range(2,5)] private float _shakeFactor;
    [SerializeField] private GameObject _shakingObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shake();
        }
    }
    private void Shake()
    {
        if (_shakingObject != null)
            _shakingObject.transform.DOShakePosition(2);
    }
}
