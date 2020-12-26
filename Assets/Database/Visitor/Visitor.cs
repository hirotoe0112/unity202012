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
    /// 開けた時のショートストーリー
    /// </summary>
    [SerializeField]
    [Multiline(10)]
    private string openStory;

    /// <summary>
    /// 開けない時のショートストーリー
    /// </summary>
    [SerializeField]
    [Multiline(10)]
    private string rejectStory;

    public KindOfAnswer GetKindOfAnswer()
    {
        return kindOfAnswer;
    }

    public string[] GetHints()
    {
        return hints;
    }

    public string GetStory(GlobalConst.Selection selection)
    {
        if (selection == GlobalConst.Selection.OPEN)
        {
            return openStory;
        }
        else
        {
            return rejectStory;
        }
    }
}
