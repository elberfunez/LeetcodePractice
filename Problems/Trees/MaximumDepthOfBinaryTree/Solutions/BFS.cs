namespace LeetcodePractice.Problems.Trees;

public class BFS
{
    public int MaxDepth(TreeNode root)
    {
        // base case
        if (root == null) return 0;

        Queue<TreeNode> q = new();
        q.Enqueue(root);
        int depth = 0;

        while (q.Count > 0)
        {
            depth++;
            int qLength = q.Count;

            for (int i = 0; i < qLength; i++)
            {
                TreeNode current = q.Dequeue();
                if (current.left != null) q.Enqueue(current.left);
                if (current.right != null) q.Enqueue(current.right);
            }
        }
        return depth;
    }
}
