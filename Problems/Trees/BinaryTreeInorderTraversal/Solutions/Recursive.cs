namespace LeetcodePractice.Problems.Trees;

public class Recursive
{
    public List<int> Inorder(TreeNode root)
    {
        List<int> result = new();
        Traverse(root, result);
        return result;
    }

    private void Traverse(TreeNode node, List<int> res)
    {
        if (node == null) return;

        Traverse(node.left, res);
        res.Add(node.val);
        Traverse(node.right, res);
    }
}
