namespace LeetcodePractice.Problems.Trees;

public class PreorderRecursive
{
    public List<int> Preorder(TreeNode root)
    {
        List<int> result = new();
        Traverse(root, result);
        return result;
    }

    private void Traverse(TreeNode node, List<int> res)
    {
        if (node == null) return;
        
        res.Add(node.val); // Add goes before others (pre)
        Traverse(node.left, res);
        Traverse(node.right, res);
    }
}
