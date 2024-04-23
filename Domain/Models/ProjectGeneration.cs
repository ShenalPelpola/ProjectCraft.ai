namespace Domain.Models;

public class ProjectGeneration
{
    public string ProjectId { get; set; }
    public string ConversationId {  get; set; }
    public List<TreeNode> FileStructure { get; set; }
}