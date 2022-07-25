using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{

    [SerializeField]
    private AudioClip _winAudioClip;

    [SerializeField]
    private AudioClip _failAudioClip;

    private UIManager _uiManager;

    private bool negotiating = false;

    private void OnTriggerStay(Collider other)
    {
        GameObject obj = other.gameObject;

        if (obj.tag == "Player")
        {
            Player p = obj.GetComponent<Player>();
            if (p != null)
            {
                if (Input.GetKeyDown(KeyCode.E) && !negotiating)
                {
                    negotiating = true;
                    if (p.haveCoin)
                    {
                        AudioSource.PlayClipAtPoint(_winAudioClip, Camera.main.transform.position);
                        StartCoroutine(Negotiate(this.gameObject, 1, true));
                        p.haveCoin = false;
                        p.ActiveWeapon();
                    }
                    else
                    {
                        AudioSource.PlayClipAtPoint(_failAudioClip, Camera.main.transform.position);
                        StartCoroutine(Negotiate(this.gameObject, 1, false));
                    }
                    
                }
            }
        }
    }

    IEnumerator Negotiate(GameObject obj, float secs, bool sucess)
    {
        yield return new WaitForSeconds(secs);
        negotiating = false;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager != null && sucess)
        {
            _uiManager.UpdateCoin(false);
        }
    }

}
