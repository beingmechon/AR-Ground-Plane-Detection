using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class run : MonoBehaviour
{
    public Animator anime;
    public Button btn;
    // Start is called before the first frame update
    void Start()
    {
        anime = gameObject.GetComponent<Animator>();
        btn = btn.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    public void TaskOnClick()
    {
        anime.SetBool("stop", false);
        anime.SetBool("run", true);
        anime.SetBool("roundKick", false);
    }
}
