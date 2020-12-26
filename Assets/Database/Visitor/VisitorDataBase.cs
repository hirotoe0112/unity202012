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

    public List<Visitor> GetVisitors()
    {
        return visitorDataBase;
    }
}
