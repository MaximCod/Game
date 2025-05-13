using UnityEngine;

public enum TypeEnemy { Unit, Boss }

public abstract class EnemyEntity : UnitEntity
{

    protected override Vector2 GetHeadPosition()
    {
        return Vector2.down;
    }

    internal override string GetGoalTag()
    {
        return "Ally";
    }

    protected override int GetSpeedModifier()
    {
        return -1;
    }

    public int RewardEnemyLvl1 = 20;
    public int RewardEnemyLvl2 = 50;
    public TypeEnemy TypeEnemy = TypeEnemy.Unit;
    public EnemyLvlUp EnemyLvlUpSettings;

    protected override void DeathUnit()
    {
        base.DeathUnit();
        if (HealthSettings.MaxHealth >= 50)
        {
            GameObject.Find("Main Camera").GetComponent<MainMenu>().Money += RewardEnemyLvl2;
        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<MainMenu>().Money += RewardEnemyLvl1;
        }
    }

    public void Level(int LevelUp)
    {
        HealthSettings.MaxHealth += EnemyLvlUpSettings.UpHealth * LevelUp;
        HealthSettings.CurrentHealth += HealthSettings.MaxHealth;
        DamageSettings.Damage += EnemyLvlUpSettings.UpDamage * LevelUp;

    }

    protected override void Awake()
    {
        base.Awake();
        EnemyLvlUpSettings = new EnemyLvlUp();
    }
}
//if (gameObject.name.Contains("boss")) Destroy(gameObject, 1.1f);