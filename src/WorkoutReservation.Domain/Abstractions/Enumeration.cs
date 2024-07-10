using System.Reflection;

namespace WorkoutReservation.Domain.Abstractions;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumerations();

    public int Id { get; set; }
    public string Name { get; protected set; } = string.Empty;
    
    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    protected Enumeration()
    {
        // for EF core
    }
    
    public static TEnum FromValue(int value) => 
        Enumerations.TryGetValue(value, out TEnum enumeration) ? enumeration : default;
    
    public static TEnum FromName(string name) => 
        Enumerations.Values.SingleOrDefault(e => e.Name == name);

    public bool Equals(Enumeration<TEnum> other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType() && Id == other.Id;
    }

    public override bool Equals(object obj) =>
        obj is Enumeration<TEnum> other && Equals(other);

    public override int GetHashCode() => base.GetHashCode();

    public static IReadOnlyCollection<TEnum> GetEnumerations() => Enumerations.Values.ToList();

    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fieldsForType = enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Id);
    }
}