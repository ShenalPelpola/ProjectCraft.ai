using System.IO.Compression;
using MediatR;
using CliWrap;
using Domain.Repositories;
using Domain.Models.Responses;
using Infrastucture.Services;
using Domain.Models;
using Application.Projects.Queries.GenerateProject;
using Infrastucture.Operations;

public class GenerateProjectQueryHandler : IRequestHandler<GenerateProjectQuery, ProjectGeneration>
{
    private readonly string _tempDirectoryName = "projectcraft";
    private readonly IBlobStorageRepository _blobStorageRepository;
    private readonly ChatService _chatService;

    public GenerateProjectQueryHandler(IBlobStorageRepository blobStorageRepository, ChatService chatService)
    {
        _blobStorageRepository = blobStorageRepository;
        _chatService = chatService;
    }

    public async Task<ProjectGeneration> Handle(GenerateProjectQuery request, CancellationToken cancellationToken)
    {
        string projectId = !string.IsNullOrWhiteSpace(request.ProjectId) ? request.ProjectId : Guid.NewGuid().ToString();

/*        ChatServiceResponse response = await _chatService.GetCommands(projectId, request.Prompt);
*/        
        string tempDirectory = Path.Combine(Path.GetTempPath(), _tempDirectoryName);


        if (!Directory.Exists(tempDirectory))
        {
            Directory.CreateDirectory(tempDirectory);
        }

        string projectDirectory = Path.Combine(tempDirectory, "1b0b15d0-710a-47d0-ba96-1c7191e6e2b1");

        if (!Directory.Exists(projectDirectory))
        {
            Directory.CreateDirectory(projectDirectory);
        }

        DirectoryInfo projectDirectoryInfo = new DirectoryInfo(projectDirectory);
        List<TreeNode> nodes = FileStructureOperations.GetDirectories(projectDirectoryInfo);

        return new ProjectGeneration
        {
            ProjectId = projectId,
            FileStructure = nodes
        };

/*        bool isProjectGenerated = true;

        foreach (string command in response.Commands)
        {
            (string executable, string arguments) = SeparateExecutableFromArguments(command);

            CommandResult result = await Cli.Wrap(executable)
                .WithArguments(arguments)
                .WithWorkingDirectory($@"{projectDirectory}")
                .ExecuteAsync();

            if (!result.IsSuccess)
            {
                isProjectGenerated = false;
            }
        }

        if (isProjectGenerated)
        {
            string fileName = $"{projectId}.zip";
            string zipFilePath = Path.Combine(tempDirectory, $"{projectId}.zip");
            ZipFolder(projectDirectory, zipFilePath);

            try
            {
                await using (Stream uploadFileStream = File.OpenRead(zipFilePath))
                {
                    await _blobStorageRepository.UploadAsync(fileName, uploadFileStream);
                }

                File.Delete(zipFilePath);

                return new ProjectGeneration
                {
                    ProjectId = projectId,
                    FileStructure = null
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);

                return new ProjectGeneration
                {
                    ProjectId = null,
                    FileStructure = null
                };
            }
        }
        else
        {
            return new ProjectGeneration
            {
                ProjectId = null,
                FileStructure = null
            };
        }*/
    }

    private static void ZipFolder(string sourceDirectory, string destinationZipFilePath)
    {
        string destDir = Path.GetDirectoryName(destinationZipFilePath);

        if (!Directory.Exists(destDir))
        {
            Directory.CreateDirectory(destDir);
        }

        ZipFile.CreateFromDirectory(sourceDirectory, destinationZipFilePath);
    }

    public static (string executable, string arguments) SeparateExecutableFromArguments(string command)
    {
        command = command.Trim();

        int firstSpaceIndex = command.IndexOf(' ');

        if (firstSpaceIndex != -1)
        {
            string executable = command.Substring(0, firstSpaceIndex);
            string arguments = command.Substring(firstSpaceIndex + 1).Trim();
            return (executable, arguments);
        }
        else
        {
            return (command, string.Empty);
        }
    }
}