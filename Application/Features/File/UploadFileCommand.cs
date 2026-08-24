using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.File
{
    public record UploadFileCommand
   (
       IFormFile FormFile
        ) : IRequest<ServiceResponse>;
}
