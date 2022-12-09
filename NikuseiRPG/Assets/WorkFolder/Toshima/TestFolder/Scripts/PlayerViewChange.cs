using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewChange : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Icon;

    public static PlayerViewChange Instance;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void Dead(int i)
    {
        SpriteRenderer sr = Icon[i].GetComponent<SpriteRenderer>();
        sr.color *= new Color(0.5f, 0.5f, 0.5f, 1f);
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
