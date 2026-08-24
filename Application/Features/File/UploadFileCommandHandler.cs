using Application.Dto;
using Application.Interfaces;
using Application.Interfaces.Embedding;
using Application.Interfaces.File;
using Application.Validation;
using Domain.Entity.File;
using Domain.Repostry.file;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.File
{
    public class UploadFileCommandHandler(IEmbeddingService embedding,ITextChunker textChunker, IPdfTextExtractor pdfTextExtractor, IDocumentRepository documentRepository,ICurrentUserService currentUserService,IFileService fileService,IvalidationService ivalidation,IValidator<UploadFileCommand>validator) : IRequestHandler<UploadFileCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
             var valid=await  ivalidation.validationservice(request, validator);
            if(!valid.Success)
            {
                return valid;
            }
            var userid = currentUserService.GetUserId();
            if(userid == null)
            {
                return new ServiceResponse(false, "user not found");
            }
            var file = await fileService.UploadAsync(request.FormFile);
            if(file == null)
            {
                return new ServiceResponse(false, "FAILED TO UPLOAD FILE");
            }
            var text = await pdfTextExtractor.ExtractText(file.FilePath);
            var chunks = textChunker.ChunkText(text);
           
            var document = new Document()
            {
                FileUrl = file.FileUrl,
                UserId=userid,
                Name=request.FormFile.FileName,
                CreatedAt=DateTime.UtcNow,
            };
            Console.WriteLine($"Total Chunks: {chunks.Count}");
            foreach (var chunk in chunks)
            {
              var emb=await embedding.GenerateEmbeddingAsync(chunk.Content);

                document.Chunks.Add(new DocumentChunk
                {
                   
                    ChunkIndex = chunk.Index,
                    Content = chunk.Content,
                    Embedding = emb

                });
                Console.WriteLine(
        $"Chunk {chunk.Index} - Words: {chunk.WordCount}");
            }
            var result = await documentRepository.AddAsync(document);
            if(!result)
            {
                return new ServiceResponse(false, "\"Failed to save document\"");
            }
            
            
            return new ServiceResponse(true, $"{document.Id}");
        }
    }
}
