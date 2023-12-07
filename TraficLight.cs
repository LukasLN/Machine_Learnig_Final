using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficLight : MonoBehaviour
{
    [SerializeField] private GameObject RedLight_Wall;
    [SerializeField] private GameObject GreenLight_Wall;

    [SerializeField] private bool isLightFrozen;

    [SerializeField] float initalTime;
    float timeLeft;
    public bool IsGreenLight;
    public int IsGreenLight_Int;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = initalTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLightFrozen == false)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                switch (IsGreenLight)
                {
                    case true:
                        //RedLight_Wall.SetActive(false);
                        //GreenLight_Wall.SetActive(true);
                        IsGreenLight = false;
                        Debug.Log("Green Light");
                        break;
                    case false:
                        //RedLight_Wall.SetActive(true);
                        //GreenLight_Wall.SetActive(false);
                        IsGreenLight = true;
                        Debug.Log("Red Light");
                        break;
                }
                timeLeft = initalTime;
            }
        }
        if (IsGreenLight == true)
        {
            IsGreenLight_Int = 0;
        }
        if (IsGreenLight == false)
        {
            IsGreenLight_Int = 1;
        }
    }
}
