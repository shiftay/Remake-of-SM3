using UnityEngine;
using System.Collections;

public class hitBlock : MonoBehaviour {

    private GameManager gm;
    private Animator anim;
    public AudioSource m_blockSource;
    public AudioClip m_blockHit;
    public AudioClip m_blockBreak;
    

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        gm = GameObject.Find("GameManager"). GetComponent<GameManager>();
	}
	
    // Update is called once per frame
	void Update () {

    }


    void OnCollisionEnter2D (Collision2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "RaccoonTail" || other.gameObject.tag == "Shell")
        {

            if(other.gameObject.tag == "Player" && gm.PlayerState == 1)
            {
                // need animation or contents potentially?
                m_blockSource.clip = m_blockHit;
                m_blockSource.Play();
            }

            if (other.gameObject.tag == "Player" && gm.PlayerState > 1)
            {
                m_blockSource.clip = m_blockBreak;
                m_blockSource.Play();
                Destroy(gameObject);
            }
            
            if(other.gameObject.tag == "Shell")
            {
                m_blockSource.clip = m_blockBreak;
                m_blockSource.Play();
                Destroy(gameObject);
            }

        }
     }


}
