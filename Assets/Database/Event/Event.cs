using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
[CreateAssetMenu(fileName ="Event",menuName ="CreateEvent")]
public class Event : ScriptableObject
{
    /// <summary>
    /// イベントの種類
    /// </summary>
    public enum KindOfEvent
    {
        DispMessage,
        Selection,
        SetImage,
        PlaySound
    }

    /// <summary>
    /// イベント種類
    /// </summary>
    [SerializeField]
    private KindOfEvent kindOfEvent;

    /// <summary>
    /// メッセージ内容
    /// </summary>
    [SerializeField]
    [Multiline(10)]
    private string message;

    /// <summary>
    /// 画像ファイル
    /// </summary>
    [SerializeField]
    private Sprite sprite;

    /// <summary>
    /// 音声ファイル
    /// </summary>
    [SerializeField]
    private AudioClip audioClip;

    public KindOfEvent GetKindOfEvent()
    {
        return kindOfEvent;
    }

    public string GetMessage()
    {
        return message;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public AudioClip GetAudioClip()
    {
        return audioClip;
    }
}
