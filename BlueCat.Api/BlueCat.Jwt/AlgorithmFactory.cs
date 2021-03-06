﻿using System;
using BlueCat.Jwt.Algorithms;

namespace BlueCat.Jwt
{
    /// <summary>
    /// Provides IJwtAlgorithms.
    /// </summary>
    public sealed class AlgorithmFactory
    {
        /// <summary>
        /// Creates an AlgorithmFactory using the provided
        /// algorithm name.
        /// </summary>
        /// <param name="algorithmName">The name of the algorithm.</param>
        /// <returns></returns>
        public IJwtAlgorithm Create(string algorithmName)
        {
            return Create((JwtHashAlgorithm)Enum.Parse(typeof(JwtHashAlgorithm), algorithmName));
        }

        /// <summary>
        /// Creates an AlgorithmFactory using the provided
        /// algorithm name.
        /// </summary>
        /// <param name="algorithm">The name of the algorithm.</param>
        public IJwtAlgorithm Create(JwtHashAlgorithm algorithm)
        {
            switch (algorithm)
            {
                case JwtHashAlgorithm.HS256:
                    return new HMACSHA256Algorithm();
                case JwtHashAlgorithm.HS384:
                    return new HMACSHA384Algorithm();
                case JwtHashAlgorithm.HS512:
                    return new HMACSHA512Algorithm();
                default:
                    throw new InvalidOperationException("Algorithm {algorithm} is not supported.");
            }
        }
    }
}