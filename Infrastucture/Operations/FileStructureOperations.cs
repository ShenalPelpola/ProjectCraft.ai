using Domain.Models;

namespace Infrastucture.Operations;

public static class FileStructureOperations
{
    public static List<TreeNode> GetDirectories(DirectoryInfo directory)
    {
        List<TreeNode> subFolders = new List<TreeNode>();

        foreach (var dir in directory.GetDirectories())
        {
            subFolders.Add(new TreeNode
            {
                Id = dir.FullName,
                Label = dir.Name,
                Children = GetDirectories(dir)
            });
        }

        foreach (var file in directory.GetFiles())
        {
            subFolders.Add(new TreeNode
            {
                Id = file.FullName,
                Label = file.Name,
                FileType = file.Extension
            });
        }
        return subFolders;
    }
}
