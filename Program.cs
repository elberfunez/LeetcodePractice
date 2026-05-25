using System.Collections.ObjectModel;
using System.Reflection;
using LeetcodePractice;
using Terminal.Gui.App;
using Terminal.Gui.Input;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

var allProblems = Assembly.GetExecutingAssembly()
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
    var toRun = RunUI(allProblems);
    if (toRun == null) break;

    Console.Clear();
    Console.WriteLine($"--- #{toRun.Number:D4} {toRun.Title} [{toRun.Difficulty}] ---\n");
    var instance = (IProblem)Activator.CreateInstance(toRun.Type)!;
    instance.Run();
    Console.WriteLine("\nPress any key to return to menu...");
    Console.ReadKey();
}

static ProblemEntry? RunUI(List<ProblemEntry> allProblems)
{
    using IApplication app = Application.Create();
    app.Init();

    ProblemEntry?  selected        = null;
    Category?      currentCategory = null;
    List<ProblemEntry> filtered    = [.. allProblems];

    var categoryGroups = allProblems
        .GroupBy(p => p.Category)
        .OrderBy(g => g.Key.ToString())
        .ToList();

    // ── Left panel: topic list ─────────────────────────────────────
    var leftPanel = new FrameView
    {
        Title  = "Topics",
        X = 0, Y = 0, Width = 34, Height = Dim.Fill()
    };

    var topicSource = new ObservableCollection<string>(BuildTopicItems(allProblems, categoryGroups));
    var topicList   = new ListView { X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };
    topicList.SetSource(topicSource);
    leftPanel.Add(topicList);

    // ── Right panel: filter + problem list ────────────────────────
    var rightPanel = new FrameView
    {
        Title  = $"Problems — All ({allProblems.Count})   [Tab] switch  [Enter] run  [Q] quit",
        X = 34, Y = 0, Width = Dim.Fill(), Height = Dim.Fill()
    };

    var searchLabel = new Label { Text = "Filter: ", X = 1, Y = 0 };
    var searchField = new TextField { X = 9, Y = 0, Width = Dim.Fill(1) };

    var problemSource = new ObservableCollection<string>(BuildProblemItems(filtered));
    var problemList   = new ListView { X = 0, Y = 2, Width = Dim.Fill(), Height = Dim.Fill() };
    problemList.SetSource(problemSource);

    rightPanel.Add(searchLabel, searchField, problemList);

    // ── Main window ───────────────────────────────────────────────
    using var window = new Window
    {
        Title = "LeetCode Practice — NeetCode 150",
        X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill()
    };
    window.Add(leftPanel, rightPanel);

    // ── Helper: rebuild problem list ──────────────────────────────
    void Refresh()
    {
        var search = (searchField.Text ?? "").ToLower();
        filtered = allProblems
            .Where(p => currentCategory == null || p.Category == currentCategory)
            .Where(p => string.IsNullOrEmpty(search) ||
                        p.Title.ToLower().Contains(search) ||
                        p.Number.ToString().Contains(search))
            .OrderBy(p => p.Number)
            .ToList();

        var topic = currentCategory?.ToString() ?? "All Problems";
        rightPanel.Title = $"Problems — {topic} ({filtered.Count})   [Tab] switch  [Enter] run  [Q] quit";
        problemSource.Clear();
        foreach (var item in BuildProblemItems(filtered))
            problemSource.Add(item);
    }

    // ── Events ────────────────────────────────────────────────────
    topicList.ValueChanged += (_, args) =>
    {
        var idx = args.NewValue;
        currentCategory = idx == null || idx == 0 ? null : categoryGroups[idx.Value - 1].Key;
        searchField.Text = "";
        Refresh();
    };

    searchField.ValueChanged += (_, _) => Refresh();

    problemList.Accepted += (_, _) =>
    {
        var idx = problemList.SelectedItem;
        if (idx.HasValue && idx.Value < filtered.Count)
        {
            selected = filtered[idx.Value];
            app.RequestStop();
        }
    };

    window.KeyDown += (_, key) =>
    {
        if (key == Key.Q || key == Key.Esc)
        {
            key.Handled = true;
            app.RequestStop();
        }
    };

    app.Run(window);

    return selected;
}

static List<string> BuildTopicItems(
    List<ProblemEntry>                      all,
    List<IGrouping<Category, ProblemEntry>> groups)
{
    var items = new List<string> { $" All Problems          ({all.Count,3})" };
    items.AddRange(groups.Select(g => $" {g.Key,-22} ({g.Count(),3})"));
    return items;
}

static List<string> BuildProblemItems(List<ProblemEntry> problems) =>
    problems.Select(p =>
    {
        var diff = p.Difficulty switch
        {
            Difficulty.Easy   => "[E]",
            Difficulty.Medium => "[M]",
            Difficulty.Hard   => "[H]",
            _                 => "   "
        };
        return $" {diff} #{p.Number:D4}  {p.Title}";
    }).ToList();

record ProblemEntry(int Number, string Title, Category Category, Difficulty Difficulty, Type Type);
