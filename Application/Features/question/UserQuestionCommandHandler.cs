using Application.Interfaces;
using Application.Interfaces.AnswerGemini;
using Application.Interfaces.Embedding;
using Application.Interfaces.File;
using Application.Validation;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.question
{
    public class UserQuestionCommandHandler(IGeminiService geminiService, IVectorSearchService vectorSearch,IGetDocumentId getDocument,ICurrentUserService currentUser,IEmbeddingService embeddingService,IvalidationService ivalidation,IValidator<UserQuestionCommand>validator) : IRequestHandler<UserQuestionCommand, string>
    {
        public async Task<string> Handle(UserQuestionCommand request, CancellationToken cancellationToken)
        {
            var valid = await ivalidation.validationservice(request, validator);
            if(!valid.Success)
            {
                return valid.Message;
            }
            var emb = await embeddingService.GenerateEmbeddingAsync(request.question);
            var userid =  currentUser.GetUserId();
            if(userid==null)
            {
                throw new InvalidOperationException("Document not found");
            }
            var documentid = getDocument.getdocumentid(userid);
            if(documentid==null)
            {
                throw new InvalidOperationException("Document not found");

            }
            var documentserch = await  vectorSearch.SearchAsync(emb, 2,.3f, documentid);

           var context=string.Join("\n",documentserch.Select(x=>x.Content));
            var prompt = $"""
Answer the user's question using only the provided context.

Context:
{context}

Question:
{request.question}
""";

            var result = await geminiService.GenerateAnswerAsync(prompt);
            return result;

        }
    }
}
