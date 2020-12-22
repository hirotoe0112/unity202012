using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    /// <summary>
    /// 共通処理クラス
    /// </summary>
    private GlobalProc globalProc;

    /// <summary>
    /// イントロダクションテキスト
    /// </summary>
    [SerializeField]
    private Text txtIntro;

    /// <summary>
    /// メッセージリスト（テスト）
    /// </summary>
    [SerializeField]
    [Multiline(10)]
    private string[] arrIntro;

    private string[] str;

    GameObject clickIcon;

    // Start is called before the first frame update
    void Start()
    {
        //共通処理クラスの生成
        globalProc = new GlobalProc();

        //クリックアイコン非表示
        clickIcon = GameObject.Find("clickIcon");
        clickIcon.SetActive(false);

        //イントロ後、メイン画面を表示
        GlobalProc.Callback callback = () => { SceneManager.LoadScene(GlobalConst.MAIN_SCENE); };
        StartCoroutine(globalProc.DispFullMessage(arrIntro, txtIntro,clickIcon,callback));
    }
}
