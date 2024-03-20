using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public Image IntroImage;
    public Text IntroText;
    public float maxXSize = 6.6f; // �̹��� �ִ� ũ��(x)
    public float maxYSize = 4.6f; // �̹��� �ִ� ũ��(y)
    public float maxZSize = 1f; // �̹��� �ִ� ũ��(z)
    public float minSizeX = 6.5f; // �̹��� �ּ� ũ��(x)
    public float minSizeY = 4.5f; // �̹��� �ּ� ũ��(y)
    public float minSizeZ = 1f; // �̹��� �ּ� ũ��(z)
    public float speed = 0.2f; // �ִϸ��̼� �ӵ�
    private bool isGrowing = true;

    /// <summary>
    ///  �ѳ� ���� touch to screen ���� �������� ����
    /// </summary>
    public Text textComponent;
    public Color[] colors; // ������ ���� �迭
    public float changeInterval = 1.0f; // ���� ���� ����

    private int currentIndex = 0; // ���� ���� �ε���

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

        // �ѳ� ���� ����� �� ��ǻ�Ϳ����� �Է����� �� ��ȯ ����
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

            // ���� ���� �ε��� ���� �� ���� �ʰ� �� 0���� �ʱ�ȭ
            currentIndex = (currentIndex + 1) % colors.Length;

            // �ؽ�Ʈ ���� ����
            textComponent.color = colors[currentIndex];
        }
    }
}
