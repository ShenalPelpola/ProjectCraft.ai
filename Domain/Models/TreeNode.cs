namespace Domain.Models;

public class TreeNode
{
    public string Id { get; set; }
    public string Label { get; set; }
    public string FileType {  get; set; }
    public List<TreeNode> Children { get; set; } = new List<TreeNode>();
}
