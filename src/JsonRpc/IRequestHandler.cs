﻿using System.Threading.Tasks;

namespace JsonRPC
{
    /// <summary>
    /// 
    /// Client --> -->
    ///               |
    /// Server <-- <--
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IRequestHandler<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }

    /// <summary>
    /// 
    /// Client --> -->
    ///               |
    /// Server <-- <--
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IRequestHandler<TRequest>
    {
        Task Handle(TRequest request);
    }
}