namespace LeetcodePractice.Problems.Trees;

public class PreorderIterative
{
    public List<int> Preorder(TreeNode root)
    {
        Stack<TreeNode> s = new();
        TreeNode cur = root;
        List<int> result = new();

        while (cur != null || s.Count > 0)
        {
            while (cur != null) // go left
            {
                s.Push(cur);
            }
            result.Add(cur.val);
            // pop from the stack
            TreeNode popped = s.Pop();
            
            // Go right
            cur = popped.right;
        }

        return result;
    }
}
