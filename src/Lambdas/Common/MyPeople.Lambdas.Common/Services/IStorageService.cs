using MyPeople.Lambdas.Common.Dtos;

namespace MyPeople.Lambdas.Common.Services;

public interface IStorageService
{
    Task<S3ResponseDto> UploadFileAsync(S3ObjectDto s3ObjectDto);
}
