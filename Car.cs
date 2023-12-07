using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public bool isReverse;
    [SerializeField] int speed;
    [SerializeField] float destroySpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestoryActor());
    }

    // Update is called once per frame
    void Update()
    {
        if(isReverse == true)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        if (isReverse == false)
        {
            transform.position += -transform.forward * Time.deltaTime * speed;
        }
    }

    IEnumerator DestoryActor()
    {
        yield return new WaitForSeconds(destroySpeed);
        Destroy(this.gameObject);
    }
}
