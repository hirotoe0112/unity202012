using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    /// <summary>
    /// 共通処理クラス
    /// </summary>
    private GlobalProc globalProc;

    /// <summary>
    /// クリックを促すアイコン
    /// </summary>
    [SerializeField]
    private GameObject clickIcon;

    /// <summary>
    /// 選択肢パネル
    /// </summary>
    [SerializeField]
    private GameObject pnlSelection;

    /// <summary>
    /// イベント進行順
    /// </summary>
    [SerializeField]
    private EventDataBase eventDataBase;

    /// <summary>
    /// 訪問者リスト
    /// </summary>
    [SerializeField]
    private VisitorDataBase visitorDataBase;

    /// <summary>
    /// ダイアログ表示エリア
    /// </summary>
    [SerializeField]
    private Text txtDialog;

    /// <summary>
    /// 背景画像表示エリア
    /// </summary>
    [SerializeField]
    private Image imgBackground;

    /// <summary>
    /// オーディオソース
    /// </summary>
    [SerializeField]
    private AudioSource audioSource;

    /// <summary>
    /// 選択肢をクリックしたかどうか
    /// </summary>
    private bool isSelected;

    /// <summary>
    /// 選んだ選択肢
    /// </summary>
    private GlobalConst.Selection selection;

    // Start is called before the first frame update
    void Start()
    {
        //共通処理クラスの生成
        globalProc = new GlobalProc();

        //クリックアイコン非表示
        clickIcon.SetActive(false);

        //選択肢パネル非表示
        pnlSelection.SetActive(false);

        //ゲーム開始
        StartCoroutine(EventProc());
    }

    /// <summary>
    /// ゲームの進行
    /// </summary>
    /// <returns></returns>
    private IEnumerator EventProc()
    {
        //指定した数だけイベントセットを繰り返す
        for(int i = 0; i < GlobalConst.PLAY_COUNT; i++)
        {
            //今回の訪問者を決定する


            //選択肢を初期化
            selection = GlobalConst.Selection.INIT;

            //イベントを順に実行する
            for(int j = 0; j < eventDataBase.GetEventOrder().Count; j++)
            {
                switch (eventDataBase.GetEventOrder()[j].GetKindOfEvent())
                {
                    case Event.KindOfEvent.DispMessage:
                        yield return StartCoroutine(DispMessage(eventDataBase.GetEventOrder()[j].GetMessage()));
                        break;

                    case Event.KindOfEvent.Selection:
                        yield return StartCoroutine(Selection());
                        break;

                    case Event.KindOfEvent.SetImage:
                        yield return StartCoroutine(SetImage(eventDataBase.GetEventOrder()[j].GetSprite()));
                        break;

                    case Event.KindOfEvent.PlaySound:
                        yield return StartCoroutine(PlaySound(eventDataBase.GetEventOrder()[j].GetAudioClip()));
                        break;
                }
            }
        }
        yield return null;
    }

    /// <summary>
    /// メッセージ表示イベント
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private IEnumerator DispMessage(string message)
    {
        GlobalProc.Callback callback = () => { SceneManager.LoadScene(GlobalConst.MAIN_SCENE); };
        yield return StartCoroutine(globalProc.DispFullMessage(new string[] { message }, txtDialog, clickIcon));
        txtDialog.text = "";
    }

    /// <summary>
    /// 背景画像設定イベント
    /// </summary>
    /// <param name="sprite"></param>
    /// <returns></returns>
    private IEnumerator SetImage(Sprite sprite)
    {
        yield return StartCoroutine(globalProc.SetImage(sprite, imgBackground));
    }

    /// <summary>
    /// 効果音再生イベント
    /// </summary>
    /// <param name="audioClip"></param>
    /// <returns></returns>
    private IEnumerator PlaySound(AudioClip audioClip)
    {
        yield return StartCoroutine(globalProc.PlaySound(audioClip, audioSource));
    }

    /// <summary>
    /// 選択肢イベント
    /// </summary>
    /// <returns></returns>
    private IEnumerator Selection()
    {
        //選択肢パネルを表示
        pnlSelection.SetActive(true);

        //いずれかの選択肢が押されるまで待機
        while (!isSelected)
        {
            yield return null;
        }

        if(selection == GlobalConst.Selection.OPEN || selection == GlobalConst.Selection.REJECT)
        {
            //開けるか開けないを選択した場合はショートストーリーへ進行

        }
        else
        {
            //もっとよく見るを選択した場合は次のヒントへ

        }

    }

    /// <summary>
    /// 選択肢クリック時処理
    /// </summary>
    /// <param name="selectionIndex"></param>
    public void ClickSelection(int selectionIndex)
    {
        isSelected = true;

        //選択したボタンの内容をセット
        switch (selectionIndex)
        {
            case 0:
                selection = GlobalConst.Selection.OPEN;
                break;
            case 1:
                selection = GlobalConst.Selection.REJECT;
                break;
            case 2:
                selection = GlobalConst.Selection.MOREHINT;
                break;
        }
    }
}
