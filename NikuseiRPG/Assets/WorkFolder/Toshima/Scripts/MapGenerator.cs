using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //csvファイルを読み込むための変数
    [SerializeField]
    private TextAsset mapCSV;

    //マスに貼り付ける画像を持っておく
    [SerializeField]
    private List<Sprite> gridSprite;

    //CSVファイルの中身を保持するリスト
    private string[] mapIndex;

    //生成したマス目を保持するリスト
    [SerializeField]
    private List<GameObject> mapGrid;

    [SerializeField]
    private GameObject respawnGrid;

    //エディターをすっきりさせるためのゲームオブジェクト
    [SerializeField]
    private GameObject MapGrids;

    //シングルトン化
    public static MapGenerator instance;

    //現在のマップが横に何マスあるか
    private int HorizontalGridNum;

    //現在のマップが縦に何マスあるか
    private int VerticalGridNum;

    private void MapGenerate()
    {
        //CSVファイルをStringReaderに変換
        StringReader reader = new StringReader(mapCSV.text);

        //先頭の解説行をスキップする
        reader.ReadLine();

        //CSVファイルの全文を読み込む
        string csv = reader.ReadToEnd();

        //読み込んだ全文を一行に切り分ける(要素数が縦列の総数)
        string[] line = csv.Split('\n');

        //一行に切り分けたものを一文字に切り分ける(要素数が横列の総数)
        string[] value = line[1].Split(',');

        //読み込んだ全文を一文字に切り分ける
        mapIndex = csv.Split(new[] { '\n', '\r', ',' }, System.StringSplitOptions.RemoveEmptyEntries);

        //マップの縦横総数を持つ変数を更新する
        VerticalGridNum = line.Count()-1;
        HorizontalGridNum = value.Count();

        //動作確認用Debug(動作確認済み)
        //Debug.Log("ワールドの横マスは " + HorizontalGridNum);
        //Debug.Log("ワールドの縦マスは " + VerticalGridNum);
        //Debug.Log("ワールドの総マスは " + mapIndex.Count());
        //for (int i = 0; i < mapIndex.Count(); i++)
        //{
        //    Debug.Log(mapIndex[i]);
        //}

        //縦列の個数分回すループ処理
        for (int y = 0; y < VerticalGridNum; y++)
        {
            //横列の個数分回すループ処理
            for (int x = 0; x < HorizontalGridNum; x++)
            {
                    //SpriteRendererを持つゲームオブジェクトを生成
                    var obj = new GameObject().AddComponent<SpriteRenderer>();

                    //生成したゲームオブジェクトをリストに格納して管理しやすくする
                    mapGrid.Add(obj.gameObject);

                    //ゲームオブジェクトを「MapGrids」の子オブジェクトにしてヒエラルキー上で一斉に隠すことができるようにする
                    obj.GameObject().transform.parent = MapGrids.transform;

                    //ゲームオブジェクトの名前を変更してナンバリングをつける
                    obj.name = new String("MapGrid" + (y * HorizontalGridNum + x));

                    //ゲームオブジェクトの位置を変更する(CSVの最初の一行は解説行のためy=0の時は無視する)              
                    obj.transform.position = new Vector3(x * 1, -(y * 1), 0);

                    //一文字の内容によって処理を分岐する
                    switch (mapIndex[y * HorizontalGridNum + x])
                    {
                        //取り出した一文字が「0」(進行不可)だった場合
                        case "0":
                        //当たり判定をつける(サイズの調整も行う)
                        obj.AddComponent<BoxCollider2D>().size = new Vector2(1,1);
                        //進行不可用のスプライトを貼り付ける(仮)
                        obj.GetComponent<SpriteRenderer>().sprite = gridSprite[0];
                        break;

                        //取り出した一文字が「1」(上部進行可)だった場合
                        case "1":
                        //上部進行可用のスプライトを貼り付ける(仮)
                        obj.GetComponent<SpriteRenderer>().sprite = gridSprite[1];
                        break;

                        //取り出した一文字が「2」(下部進行可)だった場合
                        case "2":
                        //下部進行可用のスプライトを貼り付ける(仮)
                        obj.GetComponent<SpriteRenderer>().sprite = gridSprite[1];
                        break;

                        //取り出した一文字が「3」(エンカウントマス)だった場合
                        case "3":
                        //エンカウント用のトリガーを設定する(サイズの調整も行う)
                        obj.AddComponent<BoxCollider2D>().isTrigger = true;
                        obj.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                        //エンカウント用のスプライトを貼り付ける(仮)
                        obj.GetComponent<SpriteRenderer>().sprite = gridSprite[3];
                        //エンカウント用のソースコードを張り付ける
                        obj.AddComponent<GridEncount>();
                        break;

                        //取り出した一文字が「4」(初期地点)だった場合
                        case "4":
                        //初期地点用のスプライトを貼り付ける
                        obj.GetComponent<SpriteRenderer>().sprite = gridSprite[4];
                        respawnGrid = obj.GameObject();
                        break;

                        case "5":
                        //初期地点用のスプライトを貼り付ける
                        obj.GetComponent<SpriteRenderer>().sprite = gridSprite[4];
                        //エンカウント用のトリガーを設定する(サイズの調整も行う)
                        obj.AddComponent<BoxCollider2D>().isTrigger = true;
                        obj.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                        obj.AddComponent<CleaGrid>();
                        respawnGrid = obj.GameObject();
                        break;

                    //想定外の挙動を取った場合(もしくは値がレンジ外だった場合)
                    default:
                        Debug.Log("レンジ外の文字列が混入 : " + mapIndex[y * HorizontalGridNum + x]);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// プレイヤーが登場する初期座標を返す関数
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPlayerRespownGridPos()
    {
        return respawnGrid.transform.position;
    }

    /// <summary>
    /// マップを削除する関数
    /// </summary>
    public void MapDestroy()
    {
        for(int i=0;i< mapGrid.Count();i++)
        {
            Destroy(mapGrid[i]);
        }
    }

    /// <summary>
    /// 不必要な当たり判定をつけないための処理(未完成)
    /// </summary>
    /// <param name="index">判定を取るグリッドの情報</param>
    /// <returns></returns>
    private bool GridScrutinyAssistance(int index)
    {
        
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        //マップを生成する
        MapGenerate();

        //プレイヤーの位置調整
        if(AdventureIndex.Instance.GetOmen())
        {
            TestPlayer.Instance.InitPos(respawnGrid.transform.position);
            AdventureIndex.Instance.ChangeOmenBool(false);
        }
        else
        {
            TestPlayer.Instance.InitPos(AdventureIndex.Instance.GetAdventurePosition());
        }
    }
}
