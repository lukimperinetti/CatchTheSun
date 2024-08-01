using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroller : MonoBehaviour
{
    [SerializeField] private Transform chunksContainer;    
    [SerializeField] private Transform collectablesContainer;


    public void Move(Vector3 velocity)
    {
        foreach (Transform child in chunksContainer)
            child.position += velocity;

        foreach (Transform child in collectablesContainer)
            child.position += velocity;
    }

}
