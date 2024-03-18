using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class WaterPlant : MonoBehaviour
{
    public string targetTag = "Seed"; // 실행할 대상 오브젝트의 태그
    public Text weatherText;

    public void FixedUpdate()
    {
        // 비오는 날 스킬 무료 텍스트로 바꾸기
        if (GameManager.weather)
        {
            weatherText.text = "비 오는 날 1회 무료";

        }
    }
    public void WaterSkill()
        {
            // 해당 태그가 지정된 모든 게임 오브젝트를 가져옴
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(targetTag);

            // 각 게임 오브젝트에 있는 ExampleScript 컴포넌트의 ExampleMethod 실행
            foreach (GameObject obj in taggedObjects)
            {
                GrowManager script = obj.GetComponent<GrowManager>();
                if (script != null)
                {
                // 모든 태그의 물주기 실행
                script.Waterplant();


                // 스킬이 사용된다면
                // 누를때마다 100골드씩 차감
                // 만약 비오는날이면 한번 무료
                if (GameManager.weather)
                {
                    GameManager.weather = false;
                    weatherText.text = "100골드 소모";

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
