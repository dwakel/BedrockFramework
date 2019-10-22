﻿using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;

namespace Bedrock.Framework
{
    public static partial class ServerBuilderExtensions
    {
        public static ServerBuilder UseSockets(this ServerBuilder serverBuilder, Action<SocketsServerBuilder> configure)
        {
            var socketsBuilder = new SocketsServerBuilder();
            configure(socketsBuilder);
            socketsBuilder.Apply(serverBuilder);
            return serverBuilder;
        }

        public static ClientBuilder UseSockets(this ClientBuilder clientBuilder)
        {
            return clientBuilder.UseMiddleware(previous => new SocketConnectionFactory());
        }

        public static ClientBuilder UseMiddleware(this ClientBuilder clientBuilder, Func<IConnectionFactory, IConnectionFactory> middleware)
        {
            clientBuilder.ConnectionFactory = middleware(clientBuilder.ConnectionFactory);
            return clientBuilder;
        }
    }
}