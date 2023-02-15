using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Loading1;
    public GameObject Loading2;
    public GameObject Loading3;
    public GameObject Loading4;

    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    void Start()
    {
        StartCoroutine(LoadScene2());
        StartCoroutine(Texts());
        StartCoroutine(LoadingsF());
    }

    IEnumerator LoadScene2()
    {
        yield return new WaitForSeconds(10.0f);
        SceneManager.LoadScene(2);
    }
    IEnumerator Texts()
    {
        yield return new WaitForSeconds(0.0f);
        Text1.SetActive(true);
        Text2.SetActive(false);
        Text3.SetActive(false);
        StartCoroutine (Texts1());
    }
    IEnumerator Texts1()
    {
        yield return new WaitForSeconds(3.0f);
        Text1.SetActive(false);
        Text2.SetActive(true);
        Text3.SetActive(false);
        StartCoroutine (Texts2());
    }
    IEnumerator Texts2()
    {
        yield return new WaitForSeconds(3.0f);
        Text1.SetActive(false);
        Text2.SetActive(false);
        Text3.SetActive(true);

    }


    IEnumerator LoadingsF()
    {
        yield return new WaitForSeconds(.0f);
        Loading1.SetActive(true);
        Loading2.SetActive(false);
        Loading3.SetActive(false);
        Loading4.SetActive(false);
        StartCoroutine (Loadings());
    }
    IEnumerator Loadings()
    {
        yield return new WaitForSeconds(.5f);
        Loading1.SetActive(false);
        Loading2.SetActive(true);
        Loading3.SetActive(false);
        Loading4.SetActive(false);
        StartCoroutine (Loadings1());
    }
    IEnumerator Loadings1()
    {
        yield return new WaitForSeconds(.5f);
        Loading1.SetActive(false);
        Loading2.SetActive(false);
        Loading3.SetActive(true);
        Loading4.SetActive(false);
        StartCoroutine (Loadings2());
    }
    IEnumerator Loadings2()
    {
        yield return new WaitForSeconds(.5f);
        Loading1.SetActive(false);
        Loading2.SetActive(false);
        Loading3.SetActive(false);
        Loading4.SetActive(true);
        StartCoroutine (Loadings());
    }
    






    // Update is called once per frame
    void Update()
    {
        
    }
}
