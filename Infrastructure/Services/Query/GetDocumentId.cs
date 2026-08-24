using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Query
{
    public class GetDocumentId(AppDbContext context) : IGetDocumentId
    {
        public Guid getdocumentid(string userid)
        {
          var document= context.Documents.FirstOrDefault(x => x.UserId == userid);
            return document.Id;
        }
    }
}
