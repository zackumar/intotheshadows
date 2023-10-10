using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cursor = UnityEngine.Cursor;


public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance;
    public Texture2D InteractCursor;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnApplicationFocus(bool hasFocus)
    {
        Cursor.SetCursor(Instance.InteractCursor, Vector2.zero, CursorMode.Auto);
    }

    public static void SetCursor(Texture2D texture2D)
    {
        Cursor.SetCursor(texture2D, Vector2.zero, CursorMode.Auto);

    }
}
