using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainController : MonoBehaviour
{
    /// <summary>
    /// 共通処理クラス
    /// </summary>
    private GlobalProc globalProc;

    /// <summary>
    /// 全体パネル
    /// </summary>
    [SerializeField]
    private GameObject pnlWrap;

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
    /// もっとよく見るボタン
    /// </summary>
    [SerializeField]
    private GameObject btnMoreHint;

    /// <summary>
    /// ショートストーリーパネル
    /// </summary>
    [SerializeField]
    private GameObject pnlStory;

    /// <summary>
    /// ショートストーリー表示エリア
    /// </summary>
    [SerializeField]
    private Text txtStory;

    /// <summary>
    /// 得点パネル
    /// </summary>
    [SerializeField]
    private GameObject pnlScore;

    /// <summary>
    /// 得点表示エリア
    /// </summary>
    [SerializeField]
    private Text txtScore;

    /// <summary>
    /// イベント進行順
    /// </summary>
    [SerializeField]
    private EventDataBase eventDataBase;

    /// <summary>
    /// 訪問者データベース
    /// </summary>
    [SerializeField]
    private VisitorDataBase visitorDataBase;

    /// <summary>
    /// 訪問者リスト
    /// </summary>
    private List<Visitor> visitorList = new List<Visitor>();

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
    /// ショートストーリー用BGM
    /// </summary>
    [SerializeField]
    private AudioSource storyAudio;

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

        //パネルを初期化
        PanelInit();

        //得点初期化
        StaticParam.totalScore = 0;

        //訪問者リストを生成
        visitorList = visitorDataBase.GetVisitors();

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
            int currentVisitorIndex = globalProc.CreateRandomValue(0, visitorList.Count - 1);
            Visitor currentVisitor = visitorList[currentVisitorIndex];

            //選択肢を初期化
            selection = GlobalConst.Selection.INIT;

            //パネルを初期化
            PanelInit();

            //イベントを順に実行する
            for(int j = 0; j < eventDataBase.GetEventOrder().Count; j++)
            {
                switch (eventDataBase.GetEventOrder()[j].GetKindOfEvent())
                {
                    case Event.KindOfEvent.DispMessage:
                        yield return StartCoroutine(DispMessage(eventDataBase.GetEventOrder()[j].GetMessage()));
                        break;

                    case Event.KindOfEvent.Selection:
                        yield return StartCoroutine(Selection(currentVisitor));
                        break;

                    case Event.KindOfEvent.SetImage:
                        yield return StartCoroutine(SetImage(eventDataBase.GetEventOrder()[j].GetSprite()));
                        break;

                    case Event.KindOfEvent.PlaySound:
                        yield return StartCoroutine(PlaySound(eventDataBase.GetEventOrder()[j].GetAudioClip()));
                        break;
                }
            }

            //今回の訪問者をリストから除外
            visitorList = globalProc.RemoveItem<Visitor>(visitorList, currentVisitorIndex);
        }

        //ゲーム終了後、メイン画面を表示
        GlobalProc.Callback callback = () => { SceneManager.LoadScene(GlobalConst.RANKING_SCENE); };
        StartCoroutine(globalProc.SlideOutPanel(pnlWrap, callback));
    }

    /// <summary>
    /// メッセージ表示イベント
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private IEnumerator DispMessage(string message)
    {
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
    /// <param name="visitor"></param>
    /// <returns></returns>
    private IEnumerator Selection(Visitor visitor)
    {
        int tmpScore = 1500;
        string tmpMark;
        AudioClip tmpAudio;

        //ウエイト
        yield return new WaitForSecondsRealtime(GlobalConst.SELECTION_WAIT);

        for(int i = 0; i < visitor.GetHints().Length; i++)
        {
            //選択状態を初期化
            isSelected = false;
            selection = GlobalConst.Selection.INIT;

            //選択肢パネルを非表示
            pnlSelection.SetActive(false);

            //ヒント表示
            yield return StartCoroutine(globalProc.DispFullMessage(new string[] { visitor.GetHints()[i] }, txtDialog, clickIcon));

            //ウエイト
            yield return new WaitForSecondsRealtime(GlobalConst.SELECTION_WAIT);

            //ヒントがラストの場合のみもっとよく見るボタンを非表示にする
            if (i == visitor.GetHints().Length - 1)
            {
                btnMoreHint.SetActive(false);
            }
            else
            {
                btnMoreHint.SetActive(true);
            }

            //選択肢パネルを表示
            pnlSelection.SetActive(true);

            //いずれかの選択肢が押されるまで待機
            while (!isSelected)
            {
                yield return null;
            }

            if (selection == GlobalConst.Selection.OPEN || selection == GlobalConst.Selection.REJECT)
            {
                //パネルを非表示
                pnlSelection.SetActive(false);

                //テキストを非表示
                txtDialog.text = "";

                //開けるか開けないを選択した場合はショートストーリーへ進行
                yield return StartCoroutine(PlayStory(visitor.GetStory(selection)));

                //得点を計算
                if ((selection == GlobalConst.Selection.OPEN && visitor.GetKindOfAnswer()==Visitor.KindOfAnswer.Open) ||
                    (selection == GlobalConst.Selection.REJECT && visitor.GetKindOfAnswer()==Visitor.KindOfAnswer.Reject))
                {
                    //正解時
                    tmpMark = GlobalConst.PLUS_MARK;
                    tmpAudio = visitorDataBase.GetCorrectSound();
                    StaticParam.totalScore += tmpScore;
                    txtScore.color = new Color(0.026f, 0.744f, 0.792f, 1.000f);
                }
                else
                {
                    //不正解時
                    tmpScore = -750;
                    tmpMark = GlobalConst.MINUS_MARK;
                    tmpAudio = visitorDataBase.GetIncorrectSound();
                    StaticParam.totalScore += tmpScore;
                    txtScore.color = new Color(0.792f, 0.026f, 0.026f, 1.000f);
                }

                //得点をセット
                txtScore.text = tmpMark + Math.Abs(tmpScore).ToString();

                //ウエイト
                yield return new WaitForSecondsRealtime(GlobalConst.SELECTION_WAIT);

                //得点パネル表示
                pnlScore.SetActive(true);

                //効果音
                yield return StartCoroutine(PlaySound(tmpAudio));

                //ウエイト
                yield return new WaitForSecondsRealtime(GlobalConst.SCORE_WAIT);

                //得点パネル非表示
                pnlScore.SetActive(false);

                //ショートストーリー非表示
                txtStory.text = "";

                //ウエイト
                yield return new WaitForSecondsRealtime(GlobalConst.SELECTION_FINISH_WAIT);

                //今回の訪問者を終了
                break;
            }
            else
            {
                //得点を減点
                tmpScore -= (i + 1) * 100;

                //もっとよく見るを選択した場合は次のヒントへ
            }
        }
    }

    /// <summary>
    /// ショートストーリー
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private IEnumerator PlayStory(string message)
    {
        //ショートストーリーパネルを表示
        pnlStory.SetActive(true);

        //BGMを再生
        storyAudio.Play();

        //メッセージを表示
        yield return StartCoroutine(globalProc.DispFullMessage(new string[] { message }, txtStory, clickIcon));

        //BGMを停止
        storyAudio.Stop();
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

    /// <summary>
    /// パネルを初期化
    /// </summary>
    public void PanelInit()
    {
        pnlSelection.SetActive(false);
        pnlStory.SetActive(false);
        pnlScore.SetActive(false);
        clickIcon.SetActive(false);
    }
}
