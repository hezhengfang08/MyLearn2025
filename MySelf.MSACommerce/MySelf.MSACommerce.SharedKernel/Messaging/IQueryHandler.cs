﻿

using MediatR;

namespace MySelf.MSACommerce.SharedKernel.Messaging
{
    public interface IQueryHandler<in TQuery, TResponse> :IRequestHandler<TQuery, TResponse>
        where  TQuery :IQuery<TResponse>
    {
    }
}
