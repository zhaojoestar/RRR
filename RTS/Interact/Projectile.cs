using UnityEngine;

public class Projectile : Sensor
{
    Rigidbody _rigidbody;
    public ENUM_PROJECTILE Type;
    public int LifeTime;
    float _lifeTime = 0f;
    public int Speed;
    public Transform Target;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        if (Target)
        {
            transform.LookAt(Target);
        }
    }

    void Update()
    {
        _lifeTime += Time.deltaTime;
        if (_lifeTime > LifeTime)
        {
            Destroy(gameObject);
            return;
        }
        if (Target && Type == ENUM_PROJECTILE.HOMING)
        {
            transform.LookAt(Target);
        }
        _rigidbody.velocity = transform.forward * Speed;
    }
}
