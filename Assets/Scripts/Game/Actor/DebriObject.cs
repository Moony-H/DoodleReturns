using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class DebriObject : MonoBehaviour
{
    public Rigidbody rigid;
    public Collider  col;
    public SpriteRenderer spriteRenderer;
    public float timer = 0f;
    public float lifeTime;
    public BehaviourJob fadeOutJob;
    private bool isFadeOut = false;
    public float fadeOutDuration = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= lifeTime)
        {
            if(!isFadeOut)
            {
                isFadeOut = true;
                fadeOutJob = BehaviourJob.Make(IFadeOut(),true);
            }
        }
        else
            timer += Time.deltaTime;
    }
    private void OnFadeOut()
    {
        fadeOutJob.Kill();
        if(null != gameObject)
            DestroyImmediate(gameObject);
    }
    private IEnumerator IFadeOut()
    {
        var fadeTimer = 0f;
        while(fadeTimer <= fadeOutDuration)
        {
            if(spriteRenderer != null)
            {
                spriteRenderer.color = Color.Lerp(Color.white, new Color(0,0,0,0), fadeTimer/fadeOutDuration);            
                fadeTimer += Time.deltaTime;
            }
            yield return null;
        }
        yield return null;
        OnFadeOut();
    }
}
