using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Visitor", menuName = "CreateVisitor")]
public class Visitor : ScriptableObject
{
    /// <summary>
    /// どちらの選択肢が正解か
    /// </summary>
    public enum KindOfAnswer
    {
        Open,
        Reject
    }

    /// <summary>
    /// 正解
    /// </summary>
    [SerializeField]
    private KindOfAnswer kindOfAnswer;

    /// <summary>
    /// 説明
    /// </summary>
    [SerializeField]
    private string explain;

    /// <summary>
    /// ヒント
    /// </summary>
    [SerializeField]
    private string[] hints = new string[6];

    /// <summary>
    /// 正解時ショートストーリー
    /// </summary>
    [SerializeField]
    [Multiline(10)]
    private string correctStory;

    /// <summary>
    /// 不正解時ショートストーリー
    /// </summary>
    [SerializeField]
    [Multiline(10)]
    private string incorrectStory;

    public KindOfAnswer GetKindOfAnswer()
    {
        return kindOfAnswer;
    }

    public string[] GetHints()
    {
        return hints;
    }

    public string GetCorrectStory()
    {
        return correctStory;
    }

    public string GetIncorrectStory()
    {
        return incorrectStory;
    }
}
