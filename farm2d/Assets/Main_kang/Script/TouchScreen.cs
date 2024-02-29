using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreen : MonoBehaviour
{
    public float panSpeed = 2f; // 카메라 이동 속도 조절
    public float zoomSpeed = 200f; // 카메라 줌 속도 조절

    [SerializeField] private Camera cam; // 카메라 컴포넌트 담을공간 확보
    private Vector2 lastPanPosition; // Vector2로 변경
    private int panFingerId; // 터치로 이동하는 손가락 식별자
    private bool wasZoomingLastFrame; // 줌 중이었는지 여부

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
            // 마우스로 이동 및 줌 처리
            HandleMouse();
        }
    }

    void HandleTouch()
    {
        switch (Input.touchCount)
        {
            case 1: // 카메라 이동
                wasZoomingLastFrame = false;

                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    lastPanPosition = touch.position;
                    panFingerId = touch.fingerId;
                }
                else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
                {
                    PanCamera((touch.position - lastPanPosition) * panSpeed); // 벡터에 이동 속도를 곱하여 이동
                    lastPanPosition = touch.position;
                }
                break;

            case 2: // 카메라 줌
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
        // 마우스로 카메라 이동 및 줌
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll * zoomSpeed);
        
        if (Input.GetMouseButton(0)) // 마우스 왼쪽 버튼을 누를 때
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            PanCamera(new Vector2(-mouseX, -mouseY) * panSpeed); // 벡터의 z 요소 제거
        }
    }

    void PanCamera(Vector2 delta)
    {
        // 카메라 이동
        transform.Translate(delta * Time.deltaTime, Space.World);
    }

    void ZoomCamera(float deltaMagnitudeDiff)
    {
        // 카메라 줌
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - deltaMagnitudeDiff, 10f, 100f); // 필드 오브 뷰 조정
    }
}


