using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private bool bekleniyor = false;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
            bekleniyor = false;
        }


        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (CharacterHealth.instance.isDead == true && !bekleniyor)
            {
                StartCoroutine(Bekleme());
            }
        }

    }


    IEnumerator Bekleme()
    {
        bekleniyor = true;
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(0);
    }

}
