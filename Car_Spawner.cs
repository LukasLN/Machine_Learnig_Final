using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Spawner : MonoBehaviour
{
    [SerializeField] GameObject Car;
    [SerializeField] GameObject TraficLight;
    [SerializeField] bool carDir;
    [SerializeField] bool isSameLane;
    [SerializeField] float initalTime;
    float timeLeft;
    GameObject spawnedCar;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = initalTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (TraficLight.GetComponent<TraficLight>().IsGreenLight == true && isSameLane == false)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = initalTime;
                spawnedCar = Instantiate(Car, this.gameObject.transform.position, Quaternion.identity * Quaternion.Euler(new Vector3(0, 90, 0)));

                if (carDir == true)
                {
                    spawnedCar.GetComponent<Car>().isReverse = true;
                }
            }
        }
        if(TraficLight.GetComponent<TraficLight>().IsGreenLight == false && isSameLane == true)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                timeLeft = initalTime;
                spawnedCar = Instantiate(Car, this.gameObject.transform.position, Quaternion.identity * Quaternion.Euler(new Vector3(0, 0, 0)));
            }
        }
    }
}
