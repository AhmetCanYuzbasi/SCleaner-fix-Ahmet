using System;
using UnityEngine;
using UnityEngine.UIElements;


public class Bullet : MonoBehaviour
{
    public LayerMask layerMask = 10;
    Rigidbody2D _rb2D;
    Vector3 _trajectory;
    float _speed;
    int _damage;
    float _size;
    float _lifeTime;
    
    public static event Action<Bullet> OnBulletDestroyed;

    void Awake(){
        _trajectory = transform.right;
        //FindObjectOfType<GameManager>().BulletStat();
    }

    public void SetupBulletParameters(float speed, float size, int damage, float lifeTime){
        _speed = speed;
        _size = size;
        _damage = damage;
        _lifeTime = lifeTime;
    }

    void Start()
    {
        _trajectory = transform.right;
        _rb2D = GetComponent<Rigidbody2D>();
    }

    void Update(){
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0){
            Destroy(gameObject);
            Debug.Log("Life time ran out!");
        }
    }

    void FixedUpdate(){
        Move();
    }

    private void Move(){
        _rb2D.velocity = _trajectory * _speed;
    }

    void OnCollisionEnter2D(Collision2D other){
        if((layerMask.value & (1 << other.transform.gameObject.layer)) > 0){
            Destroy(gameObject);
        }
        Debug.Log("westfalls");

    }
    void OnTriggerEnter2D(Collider2D col){

        GameObject parentObject = null;
        if(col.CompareTag("EnemyHitbox") && col.transform.parent.gameObject != null){
            parentObject = col.transform.parent.gameObject;
        }
        
        IDamageable damageable = null;
        IEnemy enemy = null;
        if (parentObject != null){
            damageable = parentObject.GetComponent<IDamageable>();
            enemy = parentObject.GetComponent<IEnemy>();
        }
        

        if(damageable != null && enemy != null){
            damageable.TakeDamage(_damage);
            enemy.SetProvoked();
            Destroy(gameObject);
        }        
    }

    void OnDestroy()
    {
        OnBulletDestroyed?.Invoke(this);
    }
}
