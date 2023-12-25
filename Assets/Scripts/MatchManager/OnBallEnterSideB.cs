using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBallEnterSideB : MonoBehaviour
{
    [Header("Mediator")]
    [SerializeField] private Mediator _mediator;
    private int _point = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6) return;
        _point += 1;
        other.gameObject.GetComponent<BallController>().StartParticles(other.transform.position.x);
        Destroy(other.gameObject);
        _mediator.ChangeValueSideB();
        _mediator.PlayDestroy();
    }

    public int GetPoints()
    {
        return _point;
    }
}
