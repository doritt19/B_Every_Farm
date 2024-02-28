using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrobSeed : MonoBehaviour
{
    public Grabb grab;
    public Image[] images;
    public GameObject circle;    
    public SpriteRenderer spriteRen;
    public GameObject testObj;
    int count = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        grab.isDragging = false;
        count = 1;
    }

    // Update is called once per frame
    void Update()
    {
        grab.isDragging = grab.GetIsDragging();
        Debug.Log(grab.GetIsDragging());
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (count > 0)
            {
                count--;
                GameObject testSeed = Instantiate(circle);
                Grabb grabb = testSeed.GetComponent<Grabb>();        
                grabb.isDragging = grab.GetIsDragging();
                Debug.Log("복사 "+grab.isDragging);
            }
            else
            {
                return;
            }
            
        }
    }
    /// <summary>
    ///드랍시 씨앗 삭제 매서드
    /// </summary>
    public void DrodDel()
    {
        if (grab.GetIsDragging() == true)
        {
            Destroy(circle);
        }
        else
        {
            return;
        }

    }
    public void DrodChangeImage()
    {
        if (grab.GetIsDragging() == true)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].sprite = spriteRen.sprite;
                StartCoroutine(DelaySeed());
            }
        }
        else
        {
            return;
        }

    }
    public void OnButtonClick1()
    {
        StartCoroutine(SpawnObjectsWithCooldown(2.5f, 2f));
    }
    public void OnButtonClick2()
    {
        StartCoroutine(SpawnObjectsWithCooldown(2f, 1.75f));
    }

    public void OnButtonClick3()
    {
        StartCoroutine(SpawnObjectsWithCooldown(1.5f, 1.5f));
    }
    public void OnButtonClick4()
    {
        StartCoroutine(SpawnObjectsWithCooldown(1f, 1.25f));
    }
    public void OnButtonClick5()
    {
        StartCoroutine(SpawnObjectsWithCooldown(0.5f, 1f));
    }
    public void OnButtonClick6()
    {
        StartCoroutine(SpawnObjectsWithCooldown(0f, 0.75f));
    }
    public void OnButtonClick7()
    {
        StartCoroutine(SpawnObjectsWithCooldown(-0.5f, 0.5f));
    }
    public void OnButtonClick8()
    {
        StartCoroutine(SpawnObjectsWithCooldown(-1f, 0.25f));
    }
    IEnumerator SpawnObjectsWithCooldown(float x, float y)
    {

       
        if (grab.GetIsDragging() == true)
        {
            for (int i = 0; i < 8; i++)
            {
                //Debug.Log($"x,y = ({x},{y})");
                Vector3 newPosition = new Vector3(x, y, 0);
                Instantiate(testObj, newPosition, Quaternion.identity);

                x += 0.5f;
                y -= 0.25f;
                yield return new WaitForSeconds(0.25f);
            }
        }


    }

    IEnumerator DelaySeed()
    {
        yield return new WaitForSeconds(0.25f);
    }
}
