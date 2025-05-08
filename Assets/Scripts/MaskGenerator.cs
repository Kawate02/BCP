using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public partial class MaskGenerator : MonoBehaviour
{
    //////////////////////////////////////////////
    private const int screenWidth = 1920;
    private const int screenHeight = 1080;

    // 使用する色の数
    private const int colorValue = 12;
    // 背景の分割数
    private const int imageCount = 41;

    // 色名のリスト（フォルダ名と一致）
    private readonly string[] str = { "Black", "Blue", "Brown", "Green", "Orange", "PeaGreen", "Pink", "Purple", "Red", "Skyblue", "White", "Yellow" };

    [SerializeField] private int _radius = 100;

    public int selectColor = 0;

    private int allWidth, allHeight;
    private int imageWidth, imageHeight;

    // 画像やテクスチャなどを管理する構造体
    struct TextureArray
    {
        public GameObject[] objects;
        public Sprite[] sprites;
        public Color[][] colors;
        public Texture2D[] textures;
    }

    // 各色ごとのデータを格納
    private TextureArray[] arrays = new TextureArray[colorValue];

    // 透明色情報
    private Color[][] emptys;

    // レイヤーマスク情報
    private SpriteMask[][] masks;

    private Vector2 preMousePos = Vector2.zero;
    private bool isInit = false;
    private bool isTouch = false;

    // 各画像ごとに塗られたポイントを記録
    private List<int>[] points;

    // アクティブな画像番号のリスト
    private List<int> activeNum = new();

    public void OnStart()
    {
        // 各画像ごとに空のリストを初期化
        points = new List<int>[imageCount];
        for (int i = 0, length = imageCount; i < length; i++)
        {
            points[i] = new List<int>();
        }

        // 最初の色のスプライトサイズを取得（全ての画像で共通と仮定）
        imageWidth = (int)Resources.Load("Colors/" + str[0] + "/" + str[0] + " Variant").GetComponent<SpriteRenderer>().bounds.size.x;
        imageHeight = (int)Resources.Load("Colors/" + str[0] + "/" + str[0] + " Variant").GetComponent<SpriteRenderer>().bounds.size.y;

        // 全体サイズを計算
        allWidth = imageWidth * imageCount;
        allHeight = imageHeight;

        // 配列初期化
        emptys = new Color[imageCount][];
        masks = new SpriteMask[colorValue][];

        GameObject[] obj;
        GameObject[] parents = new GameObject[colorValue];

        for (int i = 0, isize = colorValue; i < isize; i++)
        {
            // 各色の配列初期化
            arrays[i].objects = new GameObject[imageCount];
            arrays[i].sprites = new Sprite[imageCount];
            arrays[i].colors = new Color[imageCount][];
            arrays[i].textures = new Texture2D[imageCount];
            masks[i] = new SpriteMask[imageCount];

            for (int j = 0, jsize = imageCount; j < jsize; j++)
            {
                // ピクセルカラー配列初期化
                arrays[i].colors[j] = new Color[imageWidth * imageHeight];
                emptys[j] = new Color[imageWidth * imageHeight];
            }

            // ResourcesからPrefabをロード
            obj = Resources.LoadAll<GameObject>("Colors/" + str[i]);
            parents[i] = new GameObject(str[i]);
            parents[i].transform.parent = transform;

            for (int j = 0, jsize = imageCount; j < jsize; j++)
            {
                // 画像を配置
                arrays[i].objects[j] = Instantiate(obj[j], 
                    (Vector3.zero - new Vector3(screenWidth / 2 - imageWidth / 2, 0, 0)) + new Vector3(imageWidth, 0, 0) * j, 
                    Quaternion.identity);
                arrays[i].objects[j].transform.parent = parents[i].transform;
                masks[i][j] = arrays[i].objects[j].GetComponent<SpriteMask>();
            }
        }

        // テクスチャとカラーを再初期化
        for (int i = 0; i < colorValue; i++)
        {
            for (int j = 0; j < imageCount; j++)
            {
                arrays[i].textures[j] = new Texture2D(imageWidth, imageHeight);
            }
            for (int j = 0; j < imageCount; j++)
            {
                arrays[i].colors[j] = new Color[imageWidth * imageHeight];
            }
        }
    }

    public void OnUpdate()
    {
        // マウスボタンの状態によって塗り/クリア処理を分岐
        if (Input.GetMouseButtonDown(0)) isTouch = true;
        if (Input.GetMouseButtonUp(0)) isTouch = false;

        if (isTouch)
        {
            SetSprite(); // マウスが押されている間はスプライト更新
        }
        else if (isInit)
        {
            Clear(); // 初期化時のみ呼ばれる
        }
    }

    // 初期化時のリセット処理
    private void Clear()
    {
        isInit = false;
        activeNum.Clear();
        for (int i = 0, length = points.Length; i < length; i++)
        {
            points[i].Clear();
        }
    }

    // スプライトをマスクに反映
    private void SetSprite()
    {
        arrays[selectColor].sprites = GenerateSprite();

        for (int i = 0, size = activeNum.Count; i < size; i++)
        {
            for (int l = 0, colorlength = colorValue; l < colorlength; l++)
            {
                // マスクにスプライトを設定
                if (masks[l][activeNum[i]].sprite == null || arrays[l].sprites[activeNum[i]] != masks[l][activeNum[i]].sprite)
                {
                    masks[l][activeNum[i]].sprite = arrays[l].sprites[activeNum[i]];
                }
            }
        }
    }
    //////////////////////////////////////////////////////
    /// 2
    // テクスチャからスプライト生成
    private Sprite[] GenerateSprite()
    {
        arrays[selectColor].textures = GenerateTexture(Adjust(MousePos()));

        for (int i = 0, size = activeNum.Count; i < size; i++)
        {
            for (int l = 0, colorlength = colorValue; l < colorlength; l++)
            {
                if (arrays[l].sprites[activeNum[i]] == null || arrays[l].sprites[activeNum[i]].texture != arrays[l].textures[activeNum[i]])
                {
                    arrays[l].sprites[activeNum[i]] = Sprite.Create(
                        arrays[l].textures[activeNum[i]],
                        new Rect(0, 0, imageWidth, imageHeight),
                        new Vector2(0.5f, 0.5f),
                        1f,
                        0,
                        SpriteMeshType.FullRect
                    );
                }
            }
        }
        return arrays[selectColor].sprites;
    }
    /////////////////////////////////////////////////////////////////
    // テクスチャ生成処理
    private Texture2D[] GenerateTexture(Vector2 mousePos)
    {
        int radius = _radius / 2;

        if (!isInit)
        {
            isInit = true;
            preMousePos = mousePos;
        }

        arrays[selectColor].colors = DrawPoint(preMousePos, mousePos, radius);
        preMousePos = mousePos;

        for (int i = 0, size = activeNum.Count; i < size; i++)
        {
            for (int l = 0, colorlength = colorValue; l < colorlength; l++)
            {
                arrays[l].textures[activeNum[i]].SetPixels(arrays[l].colors[activeNum[i]]);
                arrays[l].textures[activeNum[i]].Apply();
            }
        }

        return arrays[selectColor].textures;
    }

    // マウス位置を取得し、ワールド座標 → ローカルに補正
    private Vector2 MousePos()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.x -= transform.root.gameObject.transform.position.x - allWidth / 2;
        mousePos.y -= transform.root.gameObject.transform.position.y - allHeight / 2;

        // 範囲外の補正
        if (mousePos.x > allWidth) mousePos.x = allWidth;
        if (mousePos.y > allHeight) mousePos.y = allHeight;
        if (mousePos.x < 0) mousePos.x = 0;
        if (mousePos.y < 0) mousePos.y = 0;

        return new Vector2((int)mousePos.x, (int)mousePos.y);
    }

    // 描画する座標を決定
    private Color[][] DrawPoint(Vector2 _preMousePos, Vector2 mousePos, int radius)
    {
        points = SetPoint(_preMousePos, mousePos);

        for (int i = 0, length = activeNum.Count; i < length; i++)
        {
            for (int j = 0, size = points[activeNum[i]].Count; j < size; j++)
            {
                for (int k = -radius, ksize = radius; k < ksize; k++)
                {
                    if (points[activeNum[i]][j] + (k * imageWidth) <= imageWidth * imageHeight)
                    {
                        // 選択中の色を白で塗る
                        arrays[selectColor].colors[activeNum[i]][points[activeNum[i]][j] + (k * imageWidth)] = Color.white;
                        for (int l = 0, colorlength = colorValue; l < colorlength; l++)
                        {
                            if (l != selectColor && arrays[l].colors[activeNum[i]] != null)
                            {
                                // 他の色は透明に
                                arrays[l].colors[activeNum[i]][points[activeNum[i]][j] + (k * imageWidth)] = Color.clear;
                            }
                        }
                    }
                }
            }
        }
        return arrays[selectColor].colors;
    }

    // 直前の実行時の座標との間を補完する
    private List<int>[] SetPoint(Vector2 preMousePos, Vector2 mousePos)
    {
        activeNum.Clear();
        for (int i = 0, length = points.Length; i < length; i++) points[i].Clear();

        int prex = (int)preMousePos.x;
        int prey = (int)preMousePos.y;
        int x = (int)mousePos.x;
        int y = (int)mousePos.y;

        float dirx = x - prex;
        float diry = y - prey;
        int max = (int)Mathf.Max(Mathf.Abs(dirx), Mathf.Abs(diry));

        dirx /= (max == 0 ? 1 : max);
        diry /= (max == 0 ? 1 : max);

        Vector2 temp;

        for (int i = 0, size = max; i <= size; ++i)
        {
            temp = new Vector2(prex + Mathf.Floor(i * dirx), prey + Mathf.Floor(i * diry));

            activeNum.Add((int)Mathf.Floor(temp.x / imageWidth));

            temp.x -= activeNum[i] * imageWidth;
            points[activeNum[i]].Add((int)(temp.x + temp.y * imageWidth - imageWidth));

            for (int j = -_radius / 2, length = _radius / 2; j < length; j++)
            {
                if (temp.x + j > 0 && temp.x + j < imageWidth)
                {
                    points[activeNum[i]].Add((int)((temp.x + j) + temp.y * imageWidth - imageWidth));
                }
            }
        }

        // 重複排除
        activeNum = activeNum.Distinct().ToList();
        for (int i = 0, length = activeNum.Count; i < length; i++)
        {
            points[activeNum[i]] = points[activeNum[i]].Distinct().ToList();
        }

        return points;
    }

    // マウス位置を描画可能な範囲に補正
    private Vector2 Adjust(Vector2 mousePos)
    {
        int x = (int)mousePos.x;
        int y = (int)mousePos.y;

        if (x < 0) x = 0;
        if (y < 0) y = 0;
        if (x > allWidth) x = allWidth;
        if (y > allHeight) y = allHeight;

        return new Vector2(x, y);
    }
}
