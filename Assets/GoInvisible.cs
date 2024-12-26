using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoInvisible : MonoBehaviour
{
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpecialAbility(){
        print("Görünmez oldu!");
        Color color = sr.color;
        color.a = 0.1f;
        sr.color = color;
        StartCoroutine(WaitAndExecute());
        IEnumerator WaitAndExecute()
        {
            yield return new WaitForSeconds(0.8f);
            MakeVisible();
        }
    }

    public void MakeVisible(){
        Color color = sr.color;
        color.a = 1f;
        sr.color = color;
    }
}