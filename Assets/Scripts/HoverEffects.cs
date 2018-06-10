using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HoverEffects : MonoBehaviour {

    public Text teext;

    public Camera cam;
    public Transform cross;
    public RawImage crosshair;
    public float fadeRate = 30;


    private Color green;
    private Color white;
    public bool lookingAt = false;
	void Start()
	{
        teext.enabled = false;
        white = new Color(1, 1, 1, 1);
        green = new Color(0.4348337f, 0.990566f, 0.3224012f, 1f);
        crosshair.color = white;
	}


    void Update()
    {
        Vector3 pos = new Vector3(cross.position.x, cross.position.y, cross.position.z);
        Ray ray = cam.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == ("Stone"))
            {
                teext.text = "Stone";
                teext.enabled = true;
                lookingAt = true;
                StartCoroutine(FadeToGreen());
                 
            }
            else if (hit.transform.tag == ("Key"))
            {
                teext.text = "Key";
                teext.enabled = true;
                lookingAt = true;
                StartCoroutine(FadeToGreen());

            }
            else if (hit.transform.tag == ("Bucket"))
            {
                teext.text = "Bucket";
                teext.enabled = true;
                lookingAt = true;
                StartCoroutine(FadeToGreen());

            }
            else
            {
                lookingAt = false;
                teext.enabled = false;
                StartCoroutine(FadeToWhite());
            }
        }
    }

    IEnumerator FadeToGreen()
    {
        for (float f = 0; f < 1; f = f + (1/fadeRate))
        {
            if (lookingAt) crosshair.color = Color.Lerp(white, green, f);
            else {
                StartCoroutine(FadeToWhite());
                yield break; }
            yield return new WaitForSeconds(1 / 10000);
        }
    }
    IEnumerator FadeToWhite()
    {
        for (float f = 0; f < 1; f = f + (1 / fadeRate))
        {
            if (!lookingAt) crosshair.color = Color.Lerp(green, white, f);
            else {
                StartCoroutine(FadeToGreen());
                yield break; }
            yield return new WaitForSeconds(1 / 10000);
        }
    }
}
