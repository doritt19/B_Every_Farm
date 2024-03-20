using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public Image IntroImage;
    public Text IntroText;
    public float maxXSize = 6.6f; // 이미지 최대 크기(x)
    public float maxYSize = 4.6f; // 이미지 최대 크기(y)
    public float maxZSize = 1f; // 이미지 최대 크기(z)
    public float minSizeX = 6.5f; // 이미지 최소 크기(x)
    public float minSizeY = 4.5f; // 이미지 최소 크기(y)
    public float minSizeZ = 1f; // 이미지 최소 크기(z)
    public float speed = 0.2f; // 애니메이션 속도
    private bool isGrowing = true;

    /// <summary>
    ///  한나 수정 touch to screen 색깔 변경위한 선언
    /// </summary>
    public Text textComponent;
    public Color[] colors; // 변경할 색상 배열
    public float changeInterval = 1.0f; // 색상 변경 간격

    private int currentIndex = 0; // 현재 색상 인덱스

    void Update()
    {
        Vector3 scale = IntroImage.transform.localScale;

        if (isGrowing)
        {
            scale += Vector3.one * Time.deltaTime * speed;
            if (scale.x >= maxXSize && scale.y >= maxYSize && scale.z >= maxZSize)
            {
                scale = new Vector3(maxXSize, maxYSize, maxZSize);
                isGrowing = false;
            }
        }
        else
        {
            scale -= Vector3.one * Time.deltaTime * speed;
            if (scale.x <= minSizeX && scale.y <= minSizeY && scale.z <= minSizeZ)
            {
                scale = new Vector3(minSizeX, minSizeY, minSizeZ);
                isGrowing = true;
            }
        }

        IntroImage.transform.localScale = scale;

        // 한나 수정 모바일 및 컴퓨터에서의 입력으로 씬 전환 변경
        if( Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            SceneManager.LoadScene("Main");
        }
    }
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeInterval);

            // 현재 색상 인덱스 증가 및 범위 초과 시 0으로 초기화
            currentIndex = (currentIndex + 1) % colors.Length;

            // 텍스트 색상 변경
            textComponent.color = colors[currentIndex];
        }
    }
}
