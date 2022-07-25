using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioSource _coinAudioSource;

    private UIManager _uiManager;

    private bool gotIt = false;


    private void OnTriggerStay(Collider other)
    {
        GameObject obj = other.gameObject;

        if(obj.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) && !gotIt)
            {
                Player p = obj.GetComponent<Player>();
                if(p != null)
                {
                    p.haveCoin = true;
                    _coinAudioSource.Play();
                    gotIt = true;
                    StartCoroutine(GetCoin(this.gameObject, 1));
                }
                
            }
        }
    }

    IEnumerator GetCoin(GameObject obj, float secs)
    {
        yield return new WaitForSeconds(secs);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager != null)
        {
            _uiManager.UpdateCoin(true);
        }
        Destroy(obj);
    }
}
