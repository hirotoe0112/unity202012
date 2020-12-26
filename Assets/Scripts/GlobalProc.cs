using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 共通関数
/// </summary>
public class GlobalProc
{
    /// <summary>
    /// コールバック用デリゲード
    /// </summary>
    public delegate void Callback();

    /// <summary>
    /// スライドアウト
    /// </summary>
    /// <returns></returns>
    public IEnumerator SlideOutPanel(GameObject pnlWrap, Callback callback = null)
    {
        //開始時間
        float startTime = Time.time;

        //指定の秒数経過するまで移動
        while ((Time.time - startTime) < GlobalConst.BG_SLIDE_DURATIOIN)
        {
            pnlWrap.GetComponent<RectTransform>().transform.Translate(GlobalConst.BG_SLIDE_UNIT, 0, 0);
            yield return 0;
        }

        //スライドアウト後の処理
        if (callback != null)
        {
            callback();
        }
    }

    /// <summary>
    /// 画面全体メッセージ表示
    /// </summary>
    /// <returns></returns>
    public IEnumerator DispFullMessage(string[] msgList, Text txtMessageArea, GameObject clickIcon, Callback callback = null)
    {
        string[] str;

        //ウエイト
        yield return new WaitForSecondsRealtime(GlobalConst.MESSAGE_INIT_WAIT);

        //存在するメッセージ分だけループする
        foreach (var line in msgList)
        {
            //改行で配列に格納
            str = line.Replace("\n", ",").Split(',');

            //改行ごとに処理
            for (int i = 0; i < str.Length; i++)
            {
                if (i != 0)
                {
                    //マウス入力待ちではない場合は改行してテキストを追加
                    txtMessageArea.text += "\n" + str[i];
                }
                else
                {
                    //マウス入力後の場合はテキストを更新
                    txtMessageArea.text = str[i];
                }

                //配列の最後だったらマウス入力待ちをする
                if (str.Length - 1 == i)
                {
                    //マウスクリックアイコンの表示
                    yield return new WaitForSecondsRealtime(GlobalConst.MESSAGE_WAIT);
                    clickIcon.SetActive(true);

                    yield return new WaitUntil(() => Input.GetMouseButton(0));
                    clickIcon.SetActive(false);
                }
                else
                {
                    //マウス入力待ちしない場合はウエイト
                    yield return new WaitForSecondsRealtime(GlobalConst.MESSAGE_WAIT);
                }
            }

            yield return new WaitUntil(() => Input.GetMouseButton(0));
        }

        //メッセージ表示後の処理
        if(callback != null)
        {
            callback();
        }
    }

    /// <summary>
    /// 背景画像設定
    /// </summary>
    /// <returns></returns>
    public IEnumerator SetImage(Sprite sprite, Image imgBackGround, Callback callback = null)
    {
        imgBackGround.sprite = sprite;
        yield return null;
    }

    /// <summary>
    /// 効果音再生
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="audioSource"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public IEnumerator PlaySound(AudioClip audioClip, AudioSource audioSource,Callback callback = null)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        yield return null;
    }

}
