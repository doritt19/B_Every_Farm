using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class WaterPlant : MonoBehaviour
{
    public string targetTag = "Seed"; // ������ ��� ������Ʈ�� �±�
    public Text weatherText;

    public void FixedUpdate()
    {
        // ����� �� ��ų ���� �ؽ�Ʈ�� �ٲٱ�
        if (GameManager.weather)
        {
            weatherText.text = "�� ���� �� 1ȸ ����";

        }
    }
    public void WaterSkill()
        {
            // �ش� �±װ� ������ ��� ���� ������Ʈ�� ������
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(targetTag);

            // �� ���� ������Ʈ�� �ִ� ExampleScript ������Ʈ�� ExampleMethod ����
            foreach (GameObject obj in taggedObjects)
            {
                GrowManager script = obj.GetComponent<GrowManager>();
                if (script != null)
                {
                // ��� �±��� ���ֱ� ����
                script.Waterplant();


                // ��ų�� ���ȴٸ�
                // ���������� 100��徿 ����
                // ���� ����³��̸� �ѹ� ����
                if (GameManager.weather)
                {
                    GameManager.weather = false;
                    weatherText.text = "100��� �Ҹ�";

                }
                else
                {
                    PlayerPrefs.SetInt(GameManager.goldCountKey, PlayerPrefs.GetInt(GameManager.goldCountKey) - 100);
                    PlayerPrefs.Save();
                }
            }
            }
        }
  
}
