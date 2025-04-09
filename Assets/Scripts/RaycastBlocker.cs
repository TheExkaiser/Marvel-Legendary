using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBlocker : MonoBehaviour, IClickable
{
    public void OnClick() { Debug.Log("raycast block clicked"); }
}
