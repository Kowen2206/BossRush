using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    // Coordenada de referencia
    [SerializeField] Vector3 referenceCoordinate;
    // Tamaño del área de movimiento
    [SerializeField] Vector2 movementAreaSize = new Vector2(10,15);
    // Velocidad de movimiento
    [SerializeField] float movementSpeed = 1f;

    [SerializeField] private Vector3 targetPosition;
    [SerializeField] float _distanceBetweenTargetAndReferencePoint = 4;

    void Start()
    {
        SetRandomTargetPosition();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            SetRandomTargetPosition();
        }
    }

    private void SetRandomTargetPosition()
    {
        targetPosition = new Vector2(Random.Range(referenceCoordinate.x - movementAreaSize.x / 2, referenceCoordinate.x + movementAreaSize.x / 2),
                                    Random.Range(referenceCoordinate.y - movementAreaSize.y / 2, referenceCoordinate.y + movementAreaSize.y / 2));

                                    int i = 0;
        // Asegura que la distancia del game object con la posición objetivo es mayor a el margen (_distanceBetweenTargetAndReferencePoint)
        while (targetPosition == referenceCoordinate)
        {
            i++;
            if(i > 52)
            {
                Debug.Log("Ooops");
                break;
            }
            targetPosition = new Vector2(Random.Range(referenceCoordinate.x - movementAreaSize.x / 2, referenceCoordinate.x + movementAreaSize.x / 2),
                                        Random.Range(referenceCoordinate.y - movementAreaSize.y / 2, referenceCoordinate.y + movementAreaSize.y / 2));
        }
    }
}
