using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelctor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel1()
    {
        SceneManager.LoadSceneAsync("scene");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadSceneAsync("level2_scene");
    }
}
