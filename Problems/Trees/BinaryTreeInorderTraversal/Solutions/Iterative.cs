namespace LeetcodePractice.Problems.Trees;

public class Iterative
{
    public List<int> Inorder(TreeNode root)
    {
        List<int> result = new();
        Stack<TreeNode> stack = new Stack<TreeNode>();
        TreeNode cur = root;

        while (cur != null || stack.Count > 0)
        {
            // Go to the leftmost node
            while (cur != null)
            {
                stack.Push(cur);
                cur = cur.left;
            }

            // Current is null, pop from stack
            cur = stack.Pop();
            result.Add(cur.val);

            // Visit the right subtree
            cur = cur.right;
        }

        return result;
    }
}
