using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    //マップ生成用のCSVファイルの読み込み用変数
    [SerializeField]
    private StreamReader mapFragment;

    //生成したマップのマス目をまとめるリスト
    [SerializeField]
    private List<SpriteRenderer> MapGrid;

    private SpriteRenderer MapSkin;

    //生成したマップのスキンをまとめる配列
    //[SerializeField]
    //private List<>

    // Start is called before the first frame update
    void Start()
    {
        //マップ生成用のCSVファイルを読み込む
        mapFragment = new StreamReader(@"Assets/WorkFolder/Toshima/CSV/mapAlpha.csv");

        // CSVの末尾まで繰り返す
        while (!mapFragment.EndOfStream)
        {
            // CSVファイルの一行を読み込む
            string line = mapFragment.ReadLine();
            ///Debug.Log(line);
            // 読み込んだ一行をカンマ毎に分けて配列に格納する
            string[] values = line.Split(',');
            //Debug.Log(values[0]);

            // 配列からリストに格納する
            List<string> lists = new List<string>();
            lists.AddRange(values);
        }
    }

    public void GridGenerate(int gridIndex, int xnum,int ynum)
    {
        MapGrid[gridIndex] = Instantiate(MapSkin, new Vector3(ynum * 1 + 0.0f, 0.0f, xnum * 1 + 0.0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
