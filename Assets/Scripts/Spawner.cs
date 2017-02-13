using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    /*public GameObject prefab;
    public int numPooling;

    public GameObject[] spheres;
    int index;

    void Start()
    {
        index = 0;

        spheres = new GameObject[numPooling];
        for(int i = 0; i < numPooling; i++)
        {
            spheres[i] = Instantiate(prefab, this.transform.position, Quaternion.identity) as GameObject;
            spheres[i].name = i.ToString();
            spheres[i].SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ActivateBall();
        }     
    }

    void ActivateBall()
    {
        spheres[index].transform.position = this.transform.position;
        spheres[index].GetComponent<RigidBody>().velocity = Vector3.zero;
        spheres[index].SetActive(true);

        index ++;
        if (index >= spheres.Length) index = 0;
    }*/
}
