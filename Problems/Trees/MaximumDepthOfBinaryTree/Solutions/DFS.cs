namespace LeetcodePractice.Problems.Trees;

public class DFS
{
    public int MaxDepth(TreeNode root)
    {
        // base case
        if (root == null) return 0;
        // use recursion with DFS
        int maxDepthLeftSide = MaxDepth(root.left);
        int maxDepthRightSide = MaxDepth(root.right);
        
        return 1 + Math.Max(maxDepthLeftSide, maxDepthRightSide);
    }
}
