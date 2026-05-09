using System.Reflection;
using LeetcodePractice;

var problems = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsAssignableTo(typeof(IProblem)))
    .Select(t =>
    {
        var attr = t.GetCustomAttribute<ProblemAttribute>();
        return new ProblemEntry(
            Number:     attr?.Number ?? 0,
            Title:      attr?.Title ?? t.Name,
            Category:   attr?.Category ?? Category.ArraysAndHashing,
            Difficulty: attr?.Difficulty ?? Difficulty.Easy,
            Type:       t
        );
    })
    .OrderBy(p => p.Number)
    .ToList();

while (true)
{
    Console.Clear();
    Console.WriteLine("=== LeetCode Practice ===\n");

    if (problems.Count == 0)
    {
        Console.WriteLine("  No problems yet. Add some to Problems/ folder!\n");
    }
    else
    {
        for (int i = 0; i < problems.Count; i++)
        {
            var p = problems[i];
            var diffTag = p.Difficulty switch
            {
                Difficulty.Easy   => "[E]",
                Difficulty.Medium => "[M]",
                Difficulty.Hard   => "[H]",
                _                 => "   "
            };
            var categoryName = p.Category.ToString();
            Console.WriteLine($"  {i + 1,3}. {diffTag} #{p.Number:D4} - {p.Title} ({categoryName})");
        }
    }

    Console.WriteLine("\n  0. Exit");
    Console.Write("\nSelect: ");

    if (!int.TryParse(Console.ReadLine(), out int choice)) continue;
    if (choice == 0) break;
    if (choice < 1 || choice > problems.Count) continue;

    var selected = problems[choice - 1];
    var instance = (IProblem)Activator.CreateInstance(selected.Type)!;

    Console.Clear();
    Console.WriteLine($"--- #{selected.Number:D4} {selected.Title} ---\n");
    instance.Run();

    Console.WriteLine("\nPress any key to return...");
    Console.ReadKey();
}

record ProblemEntry(int Number, string Title, Category Category, Difficulty Difficulty, Type Type);
