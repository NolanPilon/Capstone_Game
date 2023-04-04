using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{    
    [SerializeField] private Button[] levels;
    Sprite[] sprites;
    public Sprite locked;
    public Animator transition;

    // Start is called before the first frame update
    void Start()
    {
        sprites= new Sprite[levels.Length];

        for (int i = 0; i < levels.Length; i++)
        {
            sprites[i] = levels[i].image.sprite;
        }

        for (int i = 1; i < levels.Length; i++)
        {
            levels[i].image.sprite = locked;
        }
    }

    // Update is called once per frame
    public void OpenScene(int i)
    {
        if (levels[i-1] == locked) return;

        //SceneManager.LoadScene("Level" + i.ToString());
        StartCoroutine(Transition());
    }

    IEnumerator Transition() 
    {
        SoundManager.Instance.PlayMenu();
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("PrototypeLevel");
    }

}
