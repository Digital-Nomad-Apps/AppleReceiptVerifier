﻿using System;
using System.Threading.Tasks;

namespace AppleReceiptVerifier.Interfaces
{
    /// <summary>
    /// Apple Http Request interface
    /// </summary>
    internal interface IAppleHttpRequest
    {
        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="postData">The data to be posted.</param>
        /// <returns>
        /// response as string
        /// </returns>
        string GetResponse(Uri url, string postData);

        /// <summary>
        /// Gets the response asyncronously
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="postData">The data to be posted.</param>
        /// <returns>
        /// response as string
        /// </returns>
        Task<string> GetResponseAsync(Uri url, string postData);
    }
}
