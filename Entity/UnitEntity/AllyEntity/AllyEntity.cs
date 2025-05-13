using UnityEngine;

public abstract class AllyEntity : UnitEntity
{
    public AllyLvlUp AllyLvlUpSettings;
    private int cost = 50;

    private Color[] levelColors; // Массив цветов для каждого уровня
    private int currentLevel = 0;
    public int StepColor = 5;
    private int stepColor = 0;
    public int Experience = 0;

    public int Cost => cost; // Свойство только для чтения

    protected override void Awake()
    {
        base.Awake(); // Инициализация Unit
        AllyLvlUpSettings = new AllyLvlUp();
        if (levelColors == null || levelColors.Length == 0)
        {
            levelColors = new Color[] { Color.white, Color.blue, Color.green, Color.red, Color.black}; // Цвет по умолчанию
        }
    }

    public override void Update()
    {
        base.Update();
        if (Experience >= DamageSettings.Damage * 5)
        {
            LevelUp();
            Experience = 0;
        }
    }

    protected override Vector2 GetHeadPosition()
    {
        return Vector2.up;
    }

    internal override string GetGoalTag()
    {
        return "Enemy";
    }

    protected override int GetSpeedModifier()
    {
        return 1;
    }

    public void LevelUp()
    {
        // Улучшение характеристик
        HealthSettings.MaxHealth += AllyLvlUpSettings.UpHealth;
        HealthSettings.CurrentHealth += AllyLvlUpSettings.UpHealth;
        DamageSettings.Damage += AllyLvlUpSettings.UpDamage;

        ColorChangeLevel();
    }

    private void ColorChangeLevel()
    {
        if (stepColor >= StepColor)
        {
            currentLevel = Mathf.Min(currentLevel + 1, levelColors.Length - 1);
            stepColor = 0;
        }
        else
        {
            stepColor++;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(
                spriteRenderer.color.r + Mathf.Pow(-1, currentLevel) * levelColors[currentLevel].r / StepColor,
                spriteRenderer.color.g + Mathf.Pow(-1, currentLevel) * levelColors[currentLevel].g / StepColor,
                spriteRenderer.color.b + Mathf.Pow(-1, currentLevel) * levelColors[currentLevel].b / StepColor
            );
        }
    }

    // Упрощенный способ установки позиции
    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}