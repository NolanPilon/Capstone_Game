using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DeleteControl : MonoBehaviour
{
    public static bool IsDelete;
    public void OnClickDelete()
    {
        IsDelete = true;
    }
}
