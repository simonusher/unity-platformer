using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LoopMoveable : MonoBehaviour
{
    [SerializeField] Vector3[] positionOffsets;
    [SerializeField] float speed;
    [SerializeField] float timeToWait;

    private Vector3[] positions;

    private int nextPointIndex;
    private Vector3 nextPoint;
    private bool isMoving;

    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Vector3 startPosition = transform.localPosition;
        positions = new Vector3[positionOffsets.Length];
        for(int i = 0; i < positionOffsets.Length; i++)
        {
            positions[i] = startPosition + positionOffsets[i];
        }
        nextPoint = positions[0];
        nextPointIndex = 0;
        isMoving = true;
    }

    void FixedUpdate()
    {
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        if (isMoving)
        {
            body.MovePosition(Vector2.MoveTowards(transform.localPosition, nextPoint, speed));

            if (Vector2.Distance(transform.localPosition, nextPoint) <= 0.1)
            {
                StartCoroutine("waitThenSetNextPoint");
            }
        }
    }
    IEnumerator waitThenSetNextPoint()
    {
        isMoving = false;
        yield return new WaitForSeconds(timeToWait);
        SetNextPoint();
        isMoving = true;
    }

    private void SetNextPoint()
    {
        nextPointIndex = (nextPointIndex + 1) % positions.Length;
        nextPoint = positions[nextPointIndex];
    }

}
