using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreen : MonoBehaviour
{
    public float panSpeed = 2f; // ī�޶� �̵� �ӵ� ����
    public float zoomSpeed = 200f; // ī�޶� �� �ӵ� ����

    [SerializeField] private Camera cam; // ī�޶� ������Ʈ �������� Ȯ��
    private Vector2 lastPanPosition; // Vector2�� ����
    private int panFingerId; // ��ġ�� �̵��ϴ� �հ��� �ĺ���
    private bool wasZoomingLastFrame; // �� ���̾����� ����

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer)
        {
            HandleTouch();
        }
        else
        {
            // ���콺�� �̵� �� �� ó��
            HandleMouse();
        }
    }

    void HandleTouch()
    {
        switch (Input.touchCount)
        {
            case 1: // ī�޶� �̵�
                wasZoomingLastFrame = false;

                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    lastPanPosition = touch.position;
                    panFingerId = touch.fingerId;
                }
                else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
                {
                    PanCamera((touch.position - lastPanPosition) * panSpeed); // ���Ϳ� �̵� �ӵ��� ���Ͽ� �̵�
                    lastPanPosition = touch.position;
                }
                break;

            case 2: // ī�޶� ��
                Vector2[] touches = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                Vector2 currentVector = touches[0] - touches[1];
                Vector2 previousVector = (touches[0] - Input.GetTouch(0).deltaPosition) - (touches[1] - Input.GetTouch(1).deltaPosition);
                float touchDelta = currentVector.magnitude - previousVector.magnitude;

                ZoomCamera(touchDelta * zoomSpeed);
                wasZoomingLastFrame = true;
                break;

            default:
                wasZoomingLastFrame = false;
                break;
        }
    }

    void HandleMouse()
    {
        // ���콺�� ī�޶� �̵� �� ��
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll * zoomSpeed);
        
        if (Input.GetMouseButton(0)) // ���콺 ���� ��ư�� ���� ��
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            PanCamera(new Vector2(-mouseX, -mouseY) * panSpeed); // ������ z ��� ����
        }
    }

    void PanCamera(Vector2 delta)
    {
        // ī�޶� �̵�
        transform.Translate(delta * Time.deltaTime, Space.World);
    }

    void ZoomCamera(float deltaMagnitudeDiff)
    {
        // ī�޶� ��
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - deltaMagnitudeDiff, 10f, 100f); // �ʵ� ���� �� ����
    }
}


