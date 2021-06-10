using Assets.Scripts.Lib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    private bool _isHit;
    // Start is called before the first frame update

    public Material hitColor;
    public Material defaultColor;

    void Start()
    {
        _isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHit)
        {
            GetComponent<Renderer>().material = hitColor;
        }
        else
        {
            GetComponent<Renderer>().material = defaultColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (HasInterface(other, typeof(IAttacker)))
        //{
            _isHit = true;
        //}
        //if (typeof(IAttacker).IsAssignableFrom(other.gameObject.GetType()))
        //{
        //    _isHit = true;
        //}
    }

    private bool HasInterface(Collider obj, System.Type type)
    {
        foreach (var inter in obj.gameObject.GetType().GetInterfaces())
        {
            if (inter == type)
            {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerExit(Collider other)
    {
        //if (typeof(IAttacker).IsAssignableFrom(other.gameObject.GetType()))
        //{
            _isHit = false;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }
}
