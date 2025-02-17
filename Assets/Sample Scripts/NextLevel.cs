using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            anim.SetBool("passedLevel", true);

            StartCoroutine(toNextLevel());
        }
    }

    IEnumerator toNextLevel()
    {
        yield return new WaitForSeconds(1f);

        int toLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(toLoad);
    }
}
