namespace LeetcodePractice.Problems.Tries;

// Template - Do not inherit from ProblemBase. To use: rename class and add : ProblemBase
public class ImplementTrieTemplate
{
    public void Solve()
    {
        var prefixTree = new PrefixTree();

        // Test case 1: Basic insert and search
        prefixTree.Insert("dog");
        Console.WriteLine($"search('dog'): {prefixTree.Search("dog")}"); // true
        Console.WriteLine($"search('do'): {prefixTree.Search("do")}"); // false
        Console.WriteLine($"startsWith('do'): {prefixTree.StartsWith("do")}"); // true

        // Test case 2: Insert another word
        prefixTree.Insert("do");
        Console.WriteLine($"search('do'): {prefixTree.Search("do")}"); // true
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
