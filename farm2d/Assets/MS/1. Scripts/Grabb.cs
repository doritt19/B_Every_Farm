using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabb : MonoBehaviour
{
    public bool isDragging = false;
    
    private Vector3 touchOffset;

    public void Start()
    {
        isDragging = false;
    }
    private void Update()
    {
        Debug.Log("Grab" + isDragging);
    }
    private void OnMouseDown()
    {
        
        isDragging = true;
       
        touchOffset = transform.position - GetMouseWorldPos();
        
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
    public bool GetIsDragging()
    {
        return isDragging;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    private void OnDestroy()
    {
        isDragging=false;
    }


}
    

