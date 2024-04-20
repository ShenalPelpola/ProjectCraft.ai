namespace Domain.Repositories;

public interface IBlobStorageRepository
{
    Task<bool> UploadAsync(string blobName, Stream content);

    Task<Stream> DownloadFileAsync(string blobName);

    Task DeleteFileAsync(string blobName);
}
