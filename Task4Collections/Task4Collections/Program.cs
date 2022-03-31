// See https://aka.ms/new-console-template for more information

var list = new List<Entity>
{
    new() {Id = 1, ParentId = 0, Name = "Root entity"},
    new() {Id = 2, ParentId = 1, Name = "Child of 1 entity"},
    new() {Id = 3, ParentId = 1, Name = "Child of 1 entity"},
    new() {Id = 4, ParentId = 2, Name = "Child of 2 entity"},
    new() {Id = 5, ParentId = 4, Name = "Child of 4 entity"}
};

var dictionary = MakeDictionary(list);

foreach (var keys in dictionary)
{
    Console.Write($"Key = {keys.Key}, Value = List{{");
    foreach (var entity in keys.Value) 
        Console.Write($" Entity{{Id = {entity.Id}}} ");
    
    Console.WriteLine("}");
}

//
static Dictionary<int, List<Entity>> MakeDictionary(List<Entity> entitiesList)
{
    var newDictionary = new Dictionary<int, List<Entity>>();

    foreach (var entity in entitiesList)
    {
        newDictionary.TryAdd(entity.ParentId, new List<Entity>());
        newDictionary[entity.ParentId].Add(entity);
    }

    return newDictionary;
}

internal class Entity
{
    public int Id;
    public int ParentId;
    public string? Name;
}