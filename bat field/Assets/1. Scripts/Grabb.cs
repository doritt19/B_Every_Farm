using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabb : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 touchOffset;

    private void OnMouseDown()
    {
        
        isDragging = true;
        touchOffset = transform.position - GetMouseWorldPos();
        Debug.Log(isDragging);
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + touchOffset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
 


    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
  
}
    

