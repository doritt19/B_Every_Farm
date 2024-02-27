using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrobSeed : MonoBehaviour
{
    public Image[] images;
    public GameObject circle;
    public SpriteRenderer spriteRen;
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    ///µå¶ø½Ã ¾¾¾Ñ »èÁ¦ ¸Å¼­µå
    /// </summary>
    public void  DrodDel()
    {

        Destroy(circle);
    }
   public void DrodChangeImage() 
{
    for(int i = 0; i < images.Length; i++)
    {
        images[i].sprite = spriteRen.sprite;
        StartCoroutine(DelaySeed());
    }
}

IEnumerator DelaySeed()
{
    yield return new WaitForSeconds(0.25f);
}
}
