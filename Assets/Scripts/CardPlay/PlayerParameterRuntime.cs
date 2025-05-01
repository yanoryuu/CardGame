using R3;

public class PlayerParameterRuntime
{
    public ReactiveProperty<int> MaxMana = new();
    public ReactiveProperty<int> CurrentMana = new();
    public ReactiveProperty<int> Brave = new();
    public ReactiveProperty<int> Charm = new();
    public ReactiveProperty<int> Strength = new();

    public ReactiveProperty<int> ActionPoint = new(); // ← 追加

    public PlayerParameterRuntime() {}

    public PlayerParameterRuntime(PlayerParameter source)
    {
        LoadFrom(source);
    }

    public void LoadFrom(PlayerParameter source)
    {
        MaxMana.Value = source.MaxMana;
        CurrentMana.Value = source.CurrentMana;
        Brave.Value = source.Brave;
        Charm.Value = source.Charm;
        Strength.Value = source.Strength;
        ActionPoint.Value = source.ActionPoint; // ← 追加
    }

    public PlayerParameter ToParameter()
    {
        return new PlayerParameter
        {
            MaxMana = MaxMana.Value,
            CurrentMana = CurrentMana.Value,
            Brave = Brave.Value,
            Charm = Charm.Value,
            Strength = Strength.Value,
            ActionPoint = ActionPoint.Value // ← 追加
        };
    }
}