namespace LeetcodePractice.Problems.Trees;

public static class TreeHelper
{
    public static TreeNode CreateTree(int?[] values)
    {
        if (values.Length == 0 || values[0] == null) return null;

        TreeNode root = new TreeNode(values[0].Value);
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        int i = 1;
        while (queue.Count > 0 && i < values.Length)
        {
            TreeNode current = queue.Dequeue();

            if (values[i] != null)
            {
                current.left = new TreeNode(values[i].Value);
                queue.Enqueue(current.left);
            }
            i++;

            if (i < values.Length && values[i] != null)
            {
                current.right = new TreeNode(values[i].Value);
                queue.Enqueue(current.right);
            }
            i++;
        }

        return root;
    }

    public static string TreeToString(TreeNode root)
    {
        if (root == null) return "[]";

        List<string> result = new List<string>();
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            TreeNode current = queue.Dequeue();
            if (current == null)
            {
                result.Add("null");
            }
            else
            {
                result.Add(current.val.ToString());
                queue.Enqueue(current.left);
                queue.Enqueue(current.right);
            }
        }

        // Remove trailing nulls
        while (result.Count > 0 && result[result.Count - 1] == "null")
        {
            result.RemoveAt(result.Count - 1);
        }

        return "[" + string.Join(",", result) + "]";
    }
}
