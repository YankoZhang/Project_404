using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    UnityEngine.Ray ray;
    RaycastHit hit;

    public GameObject computeOutline;
    public GameObject gitUI;
    //��Ļ�ο����λ��
    Vector3 pos = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //��������
        ray = Camera.main.ScreenPointToRay(pos);
        //������������ײ
        if(Physics.Raycast(ray,out hit, 100.0f))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            if(hit.transform.name == "computer")
            {
                Debug.Log("���߼�⵽���� ");
                computeOutline.SetActive(true);
                gitUI.SetActive(true);
            }
            if (hit.transform.name != "computer")
            {
                Debug.Log("���߼�⵽���� ");
                computeOutline.SetActive(false);
                gitUI.SetActive(false);
            }
        }
        
    }
}
