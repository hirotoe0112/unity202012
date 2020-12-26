using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VisitorDataBase", menuName = "CreateVisitorDataBase")]
public class VisitorDataBase : ScriptableObject
{
    /// <summary>
    /// 訪問者データベース
    /// </summary>
    [SerializeField]
    private List<Visitor> visitorDataBase = new List<Visitor>();

    //正解時SE
    [SerializeField]
    private AudioClip correctSound;

    //不正解時SE
    [SerializeField]
    private AudioClip incorrectSound;

    public List<Visitor> GetVisitors()
    {
        var list = new List<Visitor>(visitorDataBase);
        return list;
    }

    public AudioClip GetCorrectSound()
    {
        return correctSound;
    }

    public AudioClip GetIncorrectSound()
    {
        return incorrectSound;
    }
}
