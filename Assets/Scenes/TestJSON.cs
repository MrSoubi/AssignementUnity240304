using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJSON : MonoBehaviour
{

    public TextAsset MonJSON;


    // Start is called before the first frame update
    void Start()
    {
        /*
        SaveState state = new SaveState();
        state.Score = 42;
        state.LastLevel = 12;
        print(JsonUtility.ToJson(state));*/

        SaveState data = JsonUtility.FromJson<SaveState>(MonJSON.text);
        print(data.Score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class SaveState
{
    public int LastLevel = 0;
    public int Score = 0;
}
