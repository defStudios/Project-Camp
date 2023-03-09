using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMovement : MonoBehaviour
{
    [SerializeField] private Transform[] wisps;
    [SerializeField] private Transform rotationTarget;
    [SerializeField] private float moveSpeed;

    private int _currentWispId;
    
    void Start()
    {
        
    }

    private void Update()
    {
        foreach (var wisp in wisps)
        {
            wisp.RotateAround (rotationTarget.position, Vector3.up, moveSpeed * Time.deltaTime);
            var pos = wisp.transform.position;
            pos.y = rotationTarget.position.y;
            wisp.position = pos;
        }
    }

    public void EnableWisp()
    {
        wisps[_currentWispId++].gameObject.SetActive(true);
    }
}
