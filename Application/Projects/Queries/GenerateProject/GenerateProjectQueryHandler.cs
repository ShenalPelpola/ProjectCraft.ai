using System.IO.Compression;
using MediatR;
using CliWrap;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
namespace Application.Projects.Queries.GenerateProject;

public class GenerateProjectQueryHandler : IRequestHandler<GenerateProjectQuery>
{
    private readonly string _tempDirectoryName = "projectcraft";
    private readonly IConfiguration _configuration;
    private readonly IBlobStorageRepository _blobStorageRepository;

    public GenerateProjectQueryHandler(IConfiguration configuration, IBlobStorageRepository blobStorageRepository)
    {
        _configuration = configuration;
        _blobStorageRepository = blobStorageRepository;
    }

    public async Task Handle(GenerateProjectQuery request, CancellationToken cancellationToken)
    {
        string projectId = Guid.NewGuid().ToString();
        string tempDirectory = Path.Combine(Path.GetTempPath(), _tempDirectoryName);

        if (!Directory.Exists(tempDirectory))
        {
            Directory.CreateDirectory(tempDirectory);
        }

        string projectDirectory = Path.Combine(tempDirectory, projectId);

        if (!Directory.Exists(projectDirectory))
        {
            Directory.CreateDirectory(projectDirectory);
        }

        string[] commands = {
             "new sln -n TestApplication",
             "new webapi -n TestApplication",
             "sln add ./TestApplication/TestApplication.csproj"
         };

        bool isProjectGenerated = true;

        foreach (var command in commands)
        {
            var result = await Cli.Wrap("dotnet")
                .WithArguments(command)
                .WithWorkingDirectory($@"{projectDirectory}")
                .ExecuteAsync();

            if (!result.IsSuccess)
            {
                isProjectGenerated = false;
            }
        }

        if (isProjectGenerated)
        {
            string zipFilePath = Path.Combine(tempDirectory, $"{projectId}.zip");
            ZipFolder(projectDirectory, zipFilePath);

            try
            {
                await using (Stream uploadFileStream = File.OpenRead(zipFilePath))
                {
                    await _blobStorageRepository.UploadAsync(projectId, uploadFileStream);
                }

                File.Delete(zipFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                if (Directory.Exists(projectDirectory))
                {
                    try
                    {
                        Directory.Delete(projectDirectory, true);
                        Console.WriteLine("Directory deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to delete directory: " + ex.Message);
                    }
                }
            }
        }
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
}