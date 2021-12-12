using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasAnimatorManager : MonoBehaviour
{
    void EnterGame()
    {
        SceneManager.LoadScene("level1_prototype");
    }
}
