using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum UnitType { Enemy, Ally }

public abstract class UnitEntity : MonoBehaviour
{
    public UnitType Type;
    public UnitDamage DamageSettings;
    public UnitHealth HealthSettings;
    public UnitMovement MovementSettings;

    private Slider HealthBar;
    protected SpriteRenderer spriteRenderer;

    private UnitCollisionHandler CollisionHandler;
    private UnitTriggerHandler TriggerHandler;

    internal abstract string GetGoalTag();

    void OnTriggerStay2D(Collider2D collision)
    {
        TriggerHandler.MovingTowardsGoal(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CollisionHandler.Attack(collision);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionHandler.Deceleration(collision);
    }

    public virtual void OnCollisionExit2D(Collision2D collision)
    {
        CollisionHandler.SpeedRecovery(collision);
    }

    protected virtual void Awake()
    {
        Pause = true;
        DamageSettings = new UnitDamage();
        HealthSettings = new UnitHealth();
        MovementSettings = new UnitMovement();
        CollisionHandler = new UnitCollisionHandler(this);
        TriggerHandler = new UnitTriggerHandler(this);
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject Slider = transform.GetChild(0).gameObject;
        if (Slider.GetComponentInChildren<Slider>())
        {
            HealthBar = Slider.GetComponentInChildren<Slider>();
            HealthBar.maxValue = HealthSettings.MaxHealth;
            HealthBar.value = HealthSettings.CurrentHealth;
        }
    }

    protected abstract int GetSpeedModifier();

    private bool Pause;

    public void Move(bool isPaused)
    {
        int Mod = GetSpeedModifier();
        if (!isPaused)
        {
           transform.Translate(Vector3.up * MovementSettings.Speed * Time.deltaTime * Mod);
        }
    }

    public void UpDateHealthBar()
    {
        if (HealthBar != null)
            HealthBar.value = HealthSettings.CurrentHealth;
    }

    public void TakeDamage(float damage)
    {
        if (GetComponent<AllyEntity>())
        {
            GetComponent<AllyEntity>().Experience += (int)damage;
        }

        HealthSettings.TakeDamage(damage);

        UpDateHealthBar();

        if (HealthSettings.CurrentHealth <= 0)
            DeathUnit();
    }

    private void RotateSlader()
    {
        transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, -gameObject.transform.rotation.z));
    }

    public virtual void Update()
    {
        RotateSlader();
        if (Time.timeScale == 0)
        {
            Pause = true;
            HealthSettings.Reset();
            UpDateHealthBar();
        }
        else
            Pause = false;
        Move(Pause);
    }

    protected virtual void DeathUnit()
    {
        var colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
            col.enabled = false;

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z - 5f
        );

        Destroy(gameObject, 1.54f);
    }

    protected abstract Vector2 GetHeadPosition();

    public void MoveOn(Vector2 Direction)
    {
        var Orientation = GetHeadPosition();
        float angle = Vector2.SignedAngle(Orientation, Direction);
        Vector3 targetRotation = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), MovementSettings.RotationSpeed * Time.deltaTime);
    }
}