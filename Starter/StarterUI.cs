using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarterUI : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("Canvas/Button").gameObject.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene((int)ENUM_SCENE.MAP));
    }
}
