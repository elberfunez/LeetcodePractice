namespace LeetcodePractice.Problems.Tries;

// Problem statement: ImplementTrie.md
[Problem(208, "Implement Trie (Prefix Tree)", Category.Tries, Difficulty.Medium)]
public class ImplementTrie : ProblemBase
{
    protected override void Solve()
    {
        Test("Example 1", () =>
        {
            var prefixTree = new PrefixTree();
            prefixTree.Insert("dog");
            Assert(prefixTree.Search("dog"), true, "search 'dog' after insert");
            Assert(prefixTree.Search("do"), false, "search 'do' (not inserted)");
            Assert(prefixTree.StartsWith("do"), true, "startsWith 'do'");
            prefixTree.Insert("do");
            Assert(prefixTree.Search("do"), true, "search 'do' after second insert");
        });

        Test("Overlapping prefixes", () =>
        {
            var prefixTree = new PrefixTree();
            prefixTree.Insert("apple");
            Assert(prefixTree.Search("app"), false, "search 'app' (not inserted)");
            Assert(prefixTree.StartsWith("app"), true, "startsWith 'app'");
            prefixTree.Insert("app");
            Assert(prefixTree.Search("app"), true, "search 'app' after insert");
        });

        Test("Multiple words", () =>
        {
            var prefixTree = new PrefixTree();
            prefixTree.Insert("cat");
            prefixTree.Insert("car");
            prefixTree.Insert("card");
            Assert(prefixTree.Search("cat"), true, "search 'cat'");
            Assert(prefixTree.Search("car"), true, "search 'car'");
            Assert(prefixTree.Search("ca"), false, "search 'ca' (prefix only)");
            Assert(prefixTree.StartsWith("ca"), true, "startsWith 'ca'");
        });
    }

    private void Test(string name, Action testLogic)
    {
        try
        {
            testLogic();
            Console.WriteLine($"{name}: ✓");
        }
        catch (AssertionException ex)
        {
            Console.WriteLine($"{name}: ✗ - {ex.Message}");
        }
    }

    private void Assert(bool actual, bool expected, string message)
    {
        if (actual != expected)
            throw new AssertionException($"{message} (expected {expected}, got {actual})");
    }

    private class AssertionException : Exception
    {
        public AssertionException(string message) : base(message) { }
    }

    public class PrefixTree
    {
        private class TrieNode
        {
            public Dictionary<char, TrieNode> Children = new();
            public bool IsEndOfWord = false;
        }

        private readonly TrieNode _root;

        public PrefixTree()
        {
            _root = new TrieNode();
        }

        public void Insert(string word)
        {
            var node = _root;
            foreach (var ch in word)
            {
                if (!node.Children.ContainsKey(ch))
                {
                    node.Children[ch] = new TrieNode();
                }
                node = node.Children[ch];
            }
            node.IsEndOfWord = true;
        }

        public bool Search(string word)
        {
            var node = _root;
            foreach (var ch in word)
            {
                if (!node.Children.ContainsKey(ch))
                {
                    return false;
                }
                node = node.Children[ch];
            }
            return node.IsEndOfWord;
        }

        public bool StartsWith(string prefix)
        {
            var node = _root;
            foreach (var ch in prefix)
            {
                if (!node.Children.ContainsKey(ch))
                {
                    return false;
                }
                node = node.Children[ch];
            }
            return true;
        }
    }
}
