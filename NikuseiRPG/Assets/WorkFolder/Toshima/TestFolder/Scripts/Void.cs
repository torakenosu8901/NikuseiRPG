using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Void : MonoBehaviour
{
    //[SerializeField]
    //private List<GameObject> Icon;

    [SerializeField]
    private List<Image> Icon;

    public static Void Instance;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void Dead(int i)
    {
        Image sr = Icon[i];
        sr.color *= new Color(0.5f, 0.5f, 0.5f, 1f);
        Debug.Log(sr.color);
    }

    public IEnumerator Move(int i)
    {
        bool Awei = false;
        
        float time = 0f;

        if (i == 0)
        {
            while(true)
            {
                switch(Awei)
                {
                    case true:
                        Icon[0].transform.position += new Vector3(200 * Time.deltaTime, 0, 0);
                        break;

                    case false:
                        Icon[0].transform.position -= new Vector3(200 * Time.deltaTime, 0, 0);
                        break;
                }

                if(time >= 0.5f)
                {
                    Awei = false;
                }
                else if(time >= 1f)
                {
                    yield break;
                }

                time += 1 * Time.deltaTime;
                yield return null;
            }
        }
        else if(i == 1)
        {
            while (true)
            {
                switch (Awei)
                {
                    case true:
                        Icon[1].transform.position -= new Vector3(200 * Time.deltaTime, 0, 0);
                        break;

                    case false:
                        Icon[1].transform.position += new Vector3(200 * Time.deltaTime, 0, 0);
                        break;
                }

                if (time >= 0.5f)
                {
                    Awei = false;
                }
                else if (time >= 1f)
                {
                    yield break;
                }

                time += 1 * Time.deltaTime;
                yield return null;
            }
        }
    }
}
