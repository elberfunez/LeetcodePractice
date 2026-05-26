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
    PrintProblemHeader(toRun);
    var instance = (IProblem)Activator.CreateInstance(toRun.Type)!;
    instance.Run();
    Console.WriteLine("\n" + new string('─', 60));
    Console.WriteLine("Press any key to return to menu...");
    Console.ReadKey();
}

static void PrintProblemHeader(ProblemEntry problem)
{
    var diffIcon = problem.Difficulty switch
    {
        Difficulty.Easy   => "🟢",
        Difficulty.Medium => "🟡",
        Difficulty.Hard   => "🔴",
        _                 => "⚪"
    };

    Console.WriteLine();
    Console.WriteLine("┏" + new string('━', 68) + "┓");
    Console.WriteLine($"┃  #{problem.Number:D4} ✦ {problem.Title}".PadRight(69) + "┃");
    Console.WriteLine($"┃  {diffIcon} {problem.Difficulty,-8} │ 🏷️  {problem.Category}".PadRight(69) + "┃");
    Console.WriteLine("┗" + new string('━', 68) + "┛");
    Console.WriteLine();
}

static ProblemEntry? RunUI(List<ProblemEntry> allProblems)
{
    using IApplication app = Application.Create();
    app.Init();

    ProblemEntry?  selected        = null;
    Category?      currentCategory = null;
    List<ProblemEntry> filtered    = [.. allProblems];
    bool focusOnProblems = false;

    var categoryGroups = allProblems
        .GroupBy(p => p.Category)
        .OrderBy(g => g.Key.ToString())
        .ToList();

    var leftPanel = new FrameView
    {
        Title  = "📚 Topics",
        X = 0, Y = 0, Width = 32, Height = Dim.Fill()
    };

    var topicSource = new ObservableCollection<string>(BuildTopicItems(allProblems, categoryGroups));
    var topicList   = new ListView { X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };
    topicList.SetSource(topicSource);
    leftPanel.Add(topicList);

    var rightPanel = new FrameView
    {
        Title  = "⚡ Problems",
        X = 32, Y = 0, Width = Dim.Fill(), Height = Dim.Fill()
    };

    var searchLabel = new Label { Text = "🔍 ", X = 1, Y = 0 };
    var searchField = new TextField { X = 4, Y = 0, Width = Dim.Fill(2) };

    var problemSource = new ObservableCollection<string>(BuildProblemItems(filtered));
    var problemList   = new ListView { X = 0, Y = 2, Width = Dim.Fill(), Height = Dim.Fill() };
    problemList.SetSource(problemSource);

    rightPanel.Add(searchLabel, searchField, problemList);

    using var window = new Window
    {
        Title = "🧮 LeetCode Practice — NeetCode 150",
        X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill()
    };
    window.Add(leftPanel, rightPanel);

    void UpdateTitle()
    {
        var topic = currentCategory?.ToString() ?? "All";
        var focus = focusOnProblems ? "⚡" : "📚";
        var hint = focusOnProblems ? "[Tab] switch  [↑↓] navigate  [Enter] run  [Q] quit" : "[Tab] switch  [↑↓] navigate  [Enter] filter  [Q] quit";
        rightPanel.Title = $"⚡ Problems — {topic} ({filtered.Count})  {hint}";
        leftPanel.Title = $"{focus} Topics";
    }

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

        problemSource.Clear();
        foreach (var item in BuildProblemItems(filtered))
            problemSource.Add(item);

        UpdateTitle();
    }

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

    topicList.KeyDown += (_, key) =>
    {
        if (key == Key.Tab)
        {
            key.Handled = true;
            focusOnProblems = true;
            problemList.SetFocus();
            UpdateTitle();
        }
    };

    problemList.KeyDown += (_, key) =>
    {
        if (key == Key.Tab)
        {
            key.Handled = true;
            focusOnProblems = false;
            topicList.SetFocus();
            UpdateTitle();
        }
        else if (key == Key.Enter)
        {
            key.Handled = true;
            var idx = problemList.SelectedItem;
            if (idx.HasValue && idx.Value < filtered.Count)
            {
                selected = filtered[idx.Value];
                app.RequestStop();
            }
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
    var items = new List<string> { $" ✦ All Problems ({all.Count,3})" };
    items.AddRange(groups.Select(g => $" • {g.Key,-20} ({g.Count(),2})"));
    return items;
}

static List<string> BuildProblemItems(List<ProblemEntry> problems) =>
    problems.Select(p =>
    {
        var diff = p.Difficulty switch
        {
            Difficulty.Easy   => "🟢",
            Difficulty.Medium => "🟡",
            Difficulty.Hard   => "🔴",
            _                 => "⚪"
        };
        var maxLen = Math.Max(30, Console.BufferWidth - 50);
        var title = p.Title.Length > maxLen ? p.Title[..(maxLen - 1)] + "…" : p.Title;
        return $"{diff} #{p.Number:D4}  {title}";
    }).ToList();

record ProblemEntry(int Number, string Title, Category Category, Difficulty Difficulty, Type Type);
